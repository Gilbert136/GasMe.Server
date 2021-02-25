using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GasMe.Data.Models;
using GasMe.Data;
using GasMe.Data.Enums;
using Microsoft.AspNetCore.SignalR;
using GasMe.Api.Hubs;
using GasMe.Api.Contracts.V1;
using GasMe.Service;
using GasMe.Data.Models.EntityBase;



namespace GasMe.Api.Controllers
{
    [ApiController]
    [Route(ApiRoutesBase.Base + "[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<OrderController> _logger;
        private readonly IHubContext<OrderHub> _orderHub; 
        private readonly IIdentityService _identityService;

        public IdentityController(ApplicationDbContext db, ILogger<OrderController> logger, IHubContext<OrderHub> orderHub, IIdentityService identityService)
        {
            _db = db;
            _logger = logger;
            _orderHub = orderHub;
            _identityService = identityService;
        }

        [HttpPost("auth")]
        public async Task<ResultBase<User>> auth([FromBody] User data){
            try{
                return await _identityService.AuthAsync(data);
            }
            catch(Exception e){
                return new ResultBase<User> { state = false, message = e.Message };
            }
        }

        [HttpPost("register")]
        public async Task<object> register([FromBody] User data){
            try{
                return await _identityService.RegisterAsync(data);
            }
            catch(Exception e){
                return new { state = false, message = e.Message };
            }
        }

        [HttpPost("login")]
        public async Task<object> login([FromBody] User data){
            try{
                return await _identityService.LoginInAsync(data);
            }
            catch(Exception e){
                return new { state = false, message = e.Message };
            }
        }

        [HttpPost("refreshToken")]
        public async Task<object> refreshToken([FromBody] User data){
            try{
                return await _identityService.RefreshTokenAsync(data);
            }
            catch(Exception e){
                return new { state = false, message = e.Message };
            }
        }
    }
}
