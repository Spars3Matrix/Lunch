using System.Collections.Generic;
using System.IO;
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
    }
}