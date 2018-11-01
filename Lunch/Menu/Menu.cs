using System.Collections.Generic;
using System.Linq;
using Lunch.Data;

namespace Lunch.Menu
{
    public class Menu
    {
        public static readonly Menu Empty = new Menu();

        private IList<MenuItem> MenuItems;

        public Menu()
        {
            MenuItems = new List<MenuItem>();
        }

        public bool Add(string description, decimal price)
        {
            if (string.IsNullOrEmpty(description) || 
                price < 0 ||
                Exists(description)) return false;

            MenuItems.Add(new MenuItem(description, price));
            return true;
        }

        public bool Exists(string description)
        {
            return !string.IsNullOrEmpty(description) &&
                MenuItems.Any(item => item.Description == description);
        }

        public MenuItem GetItem(string description)
        {
            return MenuItems.FirstOrDefault(item => item.Description == description);
        }

        public IEnumerable<MenuItem> GetItems(ResultFilter filter = null)
        {
            return MenuItems.Filter(filter);
        }
    }
}