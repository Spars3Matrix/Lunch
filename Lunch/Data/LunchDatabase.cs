using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Lunch.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace Lunch.Data
{
    public class LunchDatabase : DbContext
    {
        public static bool IsTesting { get; set; } = false;

        public DbSet<OrderItem> OrderItems { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                if (!IsTesting) builder.UseNpgsql(Settings.ConnectionString);
                else builder.UseInMemoryDatabase("lunch");
            }
        }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            AddTimestamps();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void AddTimestamps()
        {
            ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified))
                .ForEach(entry => {
                    if (entry.State == EntityState.Added) 
                    {
                        (entry.Entity as BaseEntity).Created = DateTime.UtcNow;
                    }

                    (entry.Entity as BaseEntity).Modified = DateTime.UtcNow;
                });
        }
    }
}