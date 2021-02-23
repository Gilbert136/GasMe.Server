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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;



namespace GasMe.Api.Controllers
{
    [ApiController]
    [Route(ApiRoutesBase.Base + "[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<OrderController> _logger;
        private readonly IUserService _userService;

        public UserController(ApplicationDbContext db, ILogger<OrderController> logger, IUserService userService)
        {
            _db = db;
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        public async Task<object> getAllAsync(){
            return new { state = true, data = await _userService.GetsAsync() };
        }

        [HttpGet("{id}")]
        public async Task<object> getAsync(int id){
            return new { state = true, data = await _userService.GetAsync(id) };
        }
    }
}
