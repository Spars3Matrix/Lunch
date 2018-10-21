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
        private readonly string[] ConfigurationKeys = new string[]
        {
            "Host",
            "Port",
            "Username",
            "Password",
            "Database"
        };
        public static IConfiguration Configuration { private get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseNpgsql(GetConnectionString());
            }
        }

        private string GetConnectionString()
        {
            string result = "";

            IEnumerable<IConfigurationSection> items = Configuration.GetSection("Data").GetChildren();

            foreach (IConfigurationSection item in items)
            {
                result += $"{item.Key}={item.Value};";
            }

            return result;
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