using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using GasMe.Data.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;
using System.Linq;
using Newtonsoft.Json;

namespace GasMe.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        private readonly DbContextOptions _options;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            _options = options;
        }

        public DbSet<Order> Order { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<CylinderType> CylinderType { get; set; }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<Quantity> Quantity { get; set; }
        public DbSet<Capacity> Capacity { get; set; }
        public DbSet<Inbox> Inbox { get; set; }
        public DbSet<Unit> Unit { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Schedule>()
                .Property(t => t.PeridDays)
                .HasConversion(new EnumCollectionJsonValueConverter<Day>())
                .Metadata.SetValueComparer(new CollectionValueComparer<Day>());
            base.OnModelCreating(modelBuilder);
        }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     base.OnModelCreating(modelBuilder);
        //     //modelBuilder.Entity<Part>().Property(p => p.Size).HasColumnType("decimal(18,4)");
        //     // Configure Decimal to always have a precision of 18 and a scale of 4
        //     modelBuilder.Conventions.Remove<DecimalPropertyConvention>();
        //     modelBuilder.Conventions.Add(new DecimalPropertyConvention(18, 4));
        //     modelBuilder.Properties<decimal>().Configure(config => config.HasPrecision(18, 4));
        // }
    }
}
