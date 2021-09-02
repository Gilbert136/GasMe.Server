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
using GasMe.Data.Models.EntityBase;




namespace GasMe.Api.Controllers
{
    [ApiController]
    [Route(ApiRoutesBase.Base + "[controller]")]
    public class UnitController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<OrderController> _logger;
        private readonly IHubContext<OrderHub> _orderHub;

        public UnitController(ApplicationDbContext db, ILogger<OrderController> logger, IHubContext<OrderHub> orderHub)
        {
            _db = db;
            _logger = logger;
            _orderHub = orderHub;
        }

        private IQueryable<Unit> _get
        {
            get
            {
                return _db.Unit.Where(x => x.status == EntityStatus.Active);
            }
        }

        [HttpGet]
        public async Task<object> getAll()
        {
            return new { state = true, data = await (_db.Unit.Where(x => x.status == EntityStatus.Active).ToListAsync()) };
        }

        [HttpGet("classification/{query}")]
        public async Task<ResultBase<IEnumerable<Unit>>> GetAsync(UnitClassification query)
        {
            return new ResultBase<IEnumerable<Unit>> { state = true, data = await _get.Where(x => x.classification == query).ToListAsync() };
        }

        [HttpPost]
        public async Task<object> save([FromBody] List<Unit> Unit)
        {
            try
            {
                Unit.ForEach(x =>
                {
                    switch (x.status)
                    {
                        case EntityStatus.New:
                            {
                                x.status = EntityStatus.Active;
                                x.createdDate = DateTime.Now;
                                _db.Unit.Add(x);
                            }
                            break;
                        case EntityStatus.Delete:
                        case EntityStatus.Active:
                            {
                                x.modifiedDate = DateTime.Now;
                                _db.Unit.Update(x);
                            }
                            break;
                    }
                });
                await _db.SaveChangesAsync();
                return new { state = true, data = Unit.ToList() };
            }
            catch (Exception e)
            {
                return new { state = false, message = e.Message };
            }
        }
    }
}
