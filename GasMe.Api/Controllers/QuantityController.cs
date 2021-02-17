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
    public class QuantityController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<OrderController> _logger;
        private readonly IHubContext<OrderHub> _orderHub;

        public QuantityController(ApplicationDbContext db, ILogger<OrderController> logger, IHubContext<OrderHub> orderHub)
        {
            _db = db;
            _logger = logger;
            _orderHub = orderHub;
        }

        [HttpGet]
        public async Task<object> getAll(){
            return new { state = true, data = await (_db.Quantity.Where(x => x.status == EntityStatus.Active).ToListAsync()) };
        }

        [HttpPost]
        public async Task<object> save([FromBody] List<Quantity> data){
            try{
                data.ForEach(x => {
                    switch(x.status){
                        case EntityStatus.New:
                        {
                            x.status = EntityStatus.Active;
                            x.createdDate = DateTime.Now;
                            _db.Quantity.Add(x);
                        }
                        break;
                        case EntityStatus.Delete:
                        case EntityStatus.Active:
                        {
                            x.modifiedDate = DateTime.Now;
                            _db.Quantity.Update(x);
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
