using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using GasMe.Data.Models;
using GasMe.Data;
using GasMe.Data.Enums;
using Microsoft.AspNetCore.SignalR;

namespace GasMe.Service
{
    public interface ICapacityService
    {
        Task<List<Capacity>> Gets();
        Task<Capacity> Get(int id);
        Task<List<Capacity>> Save(List<Capacity> data);
    }

    public class CapacityService : ICapacityService
    {
        private readonly ApplicationDbContext _db;

        public CapacityService(ApplicationDbContext db)
        {
            _db = db;
        }

        private IQueryable<Capacity> _get
        {
            get
            {
                return _db.Capacity.Where(x => x.status == EntityStatus.Active);
            }
        }

        public Task<List<Capacity>> Gets()
        {
            return _get.Include(x => x.unit).Include(x => x.currency).ToListAsync();
        }

        public Task<Capacity> Get(int id)
        {
            return (_db.Capacity.FirstOrDefaultAsync(x => (x.id == id) && (x.status == EntityStatus.Active)));
        }

        public async Task<List<Capacity>> Save(List<Capacity> data)
        {
            data.ForEach(x =>
            {
                switch (x.status)
                {
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
            return data.ToList();
        }
    }
}
