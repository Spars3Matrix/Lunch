using System.Collections.Generic;
using System.Linq;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lunch.Data;

namespace Lunch.Menu
{
    public class Menu
    {
        public static readonly Menu Empty = new Menu();
        
        public string Title { get; set; }

        public IList<Menu> SubMenus { get; set; }

        public IList<MenuItem> MenuItems { get; set; }

        public Menu(string title = null)
        {
            Title = title;
            SubMenus = new List<Menu>();
            MenuItems = new List<MenuItem>();
        }

        public bool AddItem(string description, decimal price)
        {
            if (string.IsNullOrEmpty(description) || 
                price < 0 ||
                ItemExists(description)) return false;

            MenuItems.Add(new MenuItem(description, price));
            return true;
        }

        public bool AddMenu(Menu menu)
        {
            if (menu == null || MenuExists(menu)) return false;

            SubMenus.Add(menu);
            return true;
        }

        public bool ItemExists(string description)
        {
            return !string.IsNullOrEmpty(description) &&
                MenuItems.Any(item => item.Description == description);
        }

        public bool MenuExists(Menu menu)
        {
            if (this == menu) return true;

            foreach (Menu submenu in SubMenus)
            {
                if (menu.MenuExists(menu)) return true;
            }

            return false;
        }

        public IEnumerable<MenuItem> GetItemsRecursively()
        {
            List<MenuItem> items = MenuItems.ToList();

            foreach (Menu submenu in SubMenus)
            {
                items.AddRange(submenu.GetItemsRecursively());
            }

            return items;
        }

        public IEnumerable<Menu> GetSubMenus()
        {
            return SubMenus;
        }
    }
}