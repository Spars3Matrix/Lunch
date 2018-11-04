using System;
using Newtonsoft.Json;

namespace Lunch.Menu
{
    public class MenuItem
    {
        [JsonIgnore]
        public string Id { get; } = Guid.NewGuid().ToString();
        public string Description { get; set; }
        public decimal Price { get; set; }

        public MenuItem(string description, decimal price)
        {
            Description = description;
            Price = price;
        }

        public override string ToString() => Description;
    }
}