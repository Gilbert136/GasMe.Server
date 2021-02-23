using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using GasMe.Data.Models;
using GasMe.Service.Options;
using GasMe.Data;
using GasMe.Data.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using GasMe.Data.Models.EntityBase;


namespace GasMe.Service
{
    public interface IIdentityService
    {
        Task<ResultBase<User>> LoginInAsync(User data);
        Task<ResultBase<User>> RegisterAsync(User data);
        Task<ResultBase<User>> AuthAsync(User data);
        Task<ResultBase<User>> RefreshTokenAsync(User data);
        Task<IdentityUser> GetAsync(string id);
    }
    public class IdentityService : IIdentityService
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly IUserService _userService;
        private readonly TokenValidationParameters _tokenValidationParameters;
        public IdentityService(ApplicationDbContext db, UserManager<IdentityUser> userManager, JwtSettings jwtSettings, IUserService userService, TokenValidationParameters tokenValidationParameters)
        {
            _db = db;
            _userManager = userManager;
            _jwtSettings = jwtSettings;
            _userService = userService;
            _tokenValidationParameters = tokenValidationParameters;
        }

        public Task<IdentityUser> GetAsync(string id){
            return _userManager.FindByIdAsync(id);
        }

        public async Task<ResultBase<User>> RegisterAsync(User data){
            var user = await _userManager.FindByEmailAsync(data.identityUser.Email);
            if(user != null){
                data = new User{
                    errors = new List<string> {"User exist"},
                    identityUser = null,
                };
                return new ResultBase<User> { state = false, data = data };
            }

            user = data.identityUser;
            var createUserResult = await _userManager.CreateAsync(user, data.identityUser.PasswordHash);
            if(!createUserResult.Succeeded){
                data = new User{
                    errors = createUserResult.Errors.Select(x => x.Description).ToList(),
                    identityUser = null,
                };
                return new ResultBase<User> { state = false, data = data };
            }
            data.status = EntityStatus.New;
            data.identityUserId = user?.Id;
            data.userName = user?.UserName ?? data.userName ;
            data.phoneNumber = user?.PhoneNumber ?? data.phoneNumber;
            data.email = user?.Email ?? data.email;
            data.createdBy = user?.Id;
            data = await _userService.SaveAsync(data);
            data.identityUser = user;
            return await GenerateAuthenticationAsync(data);
        }

        public async Task<ResultBase<User>> LoginInAsync(User data){
            var user = await _userManager.FindByEmailAsync(data.identityUser.Email);
            if(user == null){
                data = new User{
                    errors = new List<string> {"User does not exist"},
                    identityUser = null,
                };
                return new ResultBase<User> { state = false, data = data };
            }

            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, data.identityUser.PasswordHash);
            if(!userHasValidPassword){
                data = new User{
                    errors = new List<string> {"User or Password is invalid"},
                    identityUser = null,
                };
                return new ResultBase<User> { state = false, data = data };
            }
            data = await _userService.GetByIdentityIdAsync(user.Id);
            data.identityUser = user;
            return await GenerateAuthenticationAsync(data);
        }
        public async Task<ResultBase<User>> AuthAsync(User data){
            switch(data.status){
                case EntityStatus.New: 
                    return await RegisterAsync(data); 
                case EntityStatus.Active:
                    return await LoginInAsync(data);
                default:
                    return await LoginInAsync(data); 
            }
        }

        public async Task<ResultBase<User>> RefreshTokenAsync(User data){
            var validatedToken = GetPrincipalFromToken(data.token);
            if(validatedToken == null){
                data = new User{ errors = new List<string> {"Invalid token"}, };
                return new ResultBase<User> { state = false, data = data };
            }

            var expiryDateUnix = long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
            var expiryDateTimeUtc = default(DateTime).AddYears(1969).AddSeconds(expiryDateUnix);
            
            if(expiryDateTimeUtc > DateTime.UtcNow){
                data = new User{ errors = new List<string> {"Token not yet expired", }, };
                return new ResultBase<User> { state = false, data = data };
            }

            var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            var storedRefreshToken = await _db.RefreshToken.SingleOrDefaultAsync(x => x.token == data.refreshToken);
            if(storedRefreshToken == null){
                data = new User{ errors = new List<string> {"Refresh token does not exist"}, };
                return new ResultBase<User> { state = false, data = data };
            }

            if(storedRefreshToken.jwtId != jti){
                data = new User{ errors = new List<string> {"Refresh token does not match"}, };
                return new ResultBase<User> { state = false, data = data };
            }
            
            if(storedRefreshToken.expiryDate < DateTime.UtcNow){
                data = new User{ errors = new List<string> {"Refresh token has expired"}, };
                return new ResultBase<User> { state = false, data = data };
            }

            if(storedRefreshToken.invalidated ?? true){
                data = new User{ errors = new List<string> {"Refresh token has been invalidated"}, };
                return new ResultBase<User> { state = false, data = data };
            }

            if(storedRefreshToken.used ?? true){
                data = new User{ errors = new List<string> {"Refresh token has been used", }, };
                return new ResultBase<User> { state = false, data = data };
            }

            storedRefreshToken.used = true;
            _db.RefreshToken.Update(storedRefreshToken);
            await _db.SaveChangesAsync();

            var identityUserId = validatedToken.Claims.Single(x => x.Type == "identityUserId").Value;
            var user = await _userService.GetByIdentityIdAsync(identityUserId);
            return await GenerateAuthenticationAsync(user);
        }

        private ClaimsPrincipal GetPrincipalFromToken(string token){
            var tokenHandler = new JwtSecurityTokenHandler();
            try{
                var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);
                if(!IsJwtWithValidSecurityAlgorithm(validatedToken)) return null;
                return principal;
            }catch{
                return null;
            }
        }

        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken){
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.CurrentCultureIgnoreCase);
        }

        private async Task<ResultBase<User>> GenerateAuthenticationAsync(User data){
            var identityUser = data.identityUser;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity( new []
                {
                    new Claim(JwtRegisteredClaimNames.Sub, identityUser.Email),
                    new Claim(JwtRegisteredClaimNames.Email, identityUser.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("identityUserId", identityUser.Id),
                }),
                Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifeTime),
                SigningCredentials = new SigningCredentials( new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenCreate = tokenHandler.CreateToken(tokenDescriptor);
            
            var refreshToken = new RefreshToken{
                token = Guid.NewGuid().ToString(),
                jwtId = tokenCreate.Id,
                used = false,
                invalidated = false,
                identityUserId = identityUser.Id,
                expiryDate = DateTime.UtcNow.AddMinutes(2),
                createdDate = DateTime.UtcNow,
                
            };
            await _db.RefreshToken.AddAsync(refreshToken);
            await _db.SaveChangesAsync();

            data.refreshToken = refreshToken.token;
            data.token = tokenHandler.WriteToken(tokenCreate);
            data.identityUser = null;
            data.errors = null;
            return new ResultBase<User> { state = true, data = data };
        }

        public static IdentityUser mapper(IdentityUser data){
            return new IdentityUser{
                Email = data.Email,
                UserName = data.Email,
                PhoneNumber = data.PhoneNumber
            };
        }
    }
}
