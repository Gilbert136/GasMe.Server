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


namespace GasMe.Api.Controllers
{
    [ApiController]
    [Route(ApiRoutesBase.Base + "[controller]")]
    public class CapacityController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<OrderController> _logger;
        private readonly IHubContext<OrderHub> _orderHub; 
        private readonly ICapacityService _capacityService;

        public CapacityController(ApplicationDbContext db, ILogger<OrderController> logger, IHubContext<OrderHub> orderHub, ICapacityService capacityService)
        {
            _db = db;
            _logger = logger;
            _orderHub = orderHub;
            _capacityService = capacityService;
        }

        [HttpGet]
        public async Task<object> getAll(){
            return new { state = true, data = await _capacityService.Gets() };
        }

        [HttpPost]
        public async Task<object> save([FromBody] List<Capacity> data){
            try{
                return new { state = true, data = await _capacityService.Save(data) };
            }
            catch(Exception e){
                return new { state = false, message = e.Message };
            }
        }
    }
}
