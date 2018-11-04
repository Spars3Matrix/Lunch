using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Lunch
{
    public class Settings 
    {
        public static IConfiguration Configuration { private get; set; }

        public static string SearchIndexPath => Configuration["SearchIndexPath"];

        public static string ConnectionString
        {
            get
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
}