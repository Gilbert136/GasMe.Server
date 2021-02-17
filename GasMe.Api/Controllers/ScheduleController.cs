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
    public class ScheduleController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<OrderController> _logger;
        private readonly IHubContext<OrderHub> _orderHub;

        public ScheduleController(ApplicationDbContext db, ILogger<OrderController> logger, IHubContext<OrderHub> orderHub)
        {
            _db = db;
            _logger = logger;
            _orderHub = orderHub;
        }

        [HttpGet]
        public async Task<object> getAll(){
            return new { state = true, data = await (_db.Schedule.Where(x => x.status == EntityStatus.Active).ToListAsync()) };
        }

        [HttpPost]
        public async Task<object> save([FromBody] List<Schedule> Schedule){
            try{
                Schedule.ForEach(x => {
                    switch(x.status){
                        case EntityStatus.New:
                        {
                            x.status = EntityStatus.Active;
                            x.createdDate = DateTime.Now;
                            _db.Schedule.Add(x);
                        }
                        break;
                        case EntityStatus.Delete:
                        case EntityStatus.Active:
                        {
                            x.modifiedDate = DateTime.Now;
                            _db.Schedule.Update(x);
                        }
                        break;
                    }
                });
                await _db.SaveChangesAsync();
                //await _orderHub.Clients.AllExcept(orders.FirstOrDefault().connectionId).SendAsync("recievedOrders", orders.ToList());
                return new { state = true, data = Schedule.ToList() };
            }
            catch(Exception e){
                return new { state = false, message = e.Message };
            }
        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        
        // [HttpGet]
        // public IEnumerable<WeatherForecast> Get()
        // {
        //     var rng = new Random();
        //     return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //     {
        //         Date = DateTime.Now.AddDays(index),
        //         TemperatureC = rng.Next(-20, 55),
        //         Summary = Summaries[rng.Next(Summaries.Length)]
        //     })
        //     .ToArray();
        // }
    }
}
