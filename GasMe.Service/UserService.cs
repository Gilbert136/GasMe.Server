using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using GasMe.Data.Models;
using GasMe.Service.Extentions;
using Microsoft.AspNetCore.Http;
using GasMe.Data;
using GasMe.Service;
using GasMe.Data.Enums;
using Microsoft.AspNetCore.SignalR;

namespace GasMe.Service
{
    public interface IUserService
    {
        Task<List<User>> GetsAsync();
        Task<User> GetAsync(int id);
        Task<User> GetByIdentityIdAsync(string id);
        Task<User> SaveAsync(User data);
    }

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }
        
        public Task<List<User>> GetsAsync(){
            return (_db.User.Where(x => x.status == EntityStatus.Active).Include(c => c.identityUser).ToListAsync());
        } 

        public Task<User> GetAsync(int id){
            return (_db.User.Include(c => c.identityUser).FirstOrDefaultAsync(x => (x.id == id) && (x.status == EntityStatus.Active)));
        }

         public Task<User> GetByIdentityIdAsync(string id){
            return (_db.User.Include(c => c.identityUser).FirstOrDefaultAsync(x => (x.identityUserId == id) && (x.status == EntityStatus.Active)));
        }

        public async Task<User> SaveAsync(User data){
            switch(data.status)
            {
                case EntityStatus.New:
                {
                    data.status = EntityStatus.Active;
                    data.createdDate = DateTime.Now;
                    _db.User.Add(data);
                }
                break;
                case EntityStatus.Delete:
                case EntityStatus.Active:
                {
                    data.modifiedDate = DateTime.Now;
                    data.modifiedBy = _httpContextAccessor.HttpContext.GetUserId();
                    _db.User.Update(data);
                }
                break;
            }
            await _db.SaveChangesAsync();
            return data;
        }

        public User mapper(User data){
            return new User();
        }
    }
}
