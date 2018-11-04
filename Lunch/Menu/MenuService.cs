using System.Collections.Generic;
using System.Linq;
using Lunch.Data;
using Lunch.Search;

namespace Lunch.Menu
{
    public class MenuService
    {
        private static IMenuProvider MenuProvider;

        private static ISearchEngine<MenuItem> SearchEngine;

        public Menu Menu => MenuProvider.Menu;

        public MenuService SetMenuProvider(IMenuProvider menuProvider)
        {
            MenuProvider = menuProvider;
            MenuProvider.MenuUpdated += (Menu menu) => SearchEngine.SetDataset(menu.GetItemsRecursively());
            return this;
        }

        public MenuService SetSearchEngine(ISearchEngine<MenuItem> engine)
        {
            SearchEngine = engine;
            SearchEngine.SetDataset(GetItems());
            return this;
        }

        public MenuItem GetItem(string description)
        {
            return SearchEngine.Search(description).FirstOrDefault();
        }

        public IEnumerable<MenuItem> GetItems(ResultFilter filter = null)
        {
            return Menu.GetItemsRecursively().Filter(filter);
        }
    }
}