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


namespace GasMe.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CapacityController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<OrderController> _logger;
        private readonly IHubContext<OrderHub> _orderHub;

        public CapacityController(ApplicationDbContext db, ILogger<OrderController> logger, IHubContext<OrderHub> orderHub)
        {
            _db = db;
            _logger = logger;
            _orderHub = orderHub;
        }

        [HttpGet]
        public async Task<object> getAll(){
            return new { state = true, data = await (_db.Capacity.Where(x => x.status == EntityStatus.Active).ToListAsync()) };
        }

        [HttpPost]
        public async Task<object> save([FromBody] List<Capacity> data){
            try{
                data.ForEach(x => {
                    switch(x.status){
                        case EntityStatus.New:
                        {
                            x.status = EntityStatus.Active;
                            x.createdDate = DateTime.Now;
                            _db.Capacity.Add(x);
                        }
                        break;
                        case EntityStatus.Delete:
                        case EntityStatus.Active:
                        {
                            x.modifiedDate = DateTime.Now;
                            _db.Capacity.Update(x);
                        }
                        break;
                    }
                });
                await _db.SaveChangesAsync();
                return new { state = true, data = data.ToList() };
            }
            catch(Exception e){
                return new { state = false, message = e.Message };
            }
        }
    }
}
