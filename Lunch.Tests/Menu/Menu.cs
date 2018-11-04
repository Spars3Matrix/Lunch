using Xunit;

namespace Lunch.Tests.Menu
{
    public class Menu : BaseTest
    {
        [Fact]
        public void AddItem()
        {
            string fries = "fries";
            string soda = "soda";
            
            var menu = new Lunch.Menu.Menu();
            Assert.True(menu.AddItem(fries, 2m));
            Assert.False(menu.AddItem(fries, 1.5m));
            Assert.False(menu.AddItem(soda, -1m));
            Assert.True(menu.AddItem(soda, 0m));
            Assert.False(menu.AddItem(null, 1m));
            Assert.False(menu.AddItem("", 1m));
        }

        [Fact]
        public void AddMenu()
        {
            var menu = new Lunch.Menu.Menu();
            var submenu = new Lunch.Menu.Menu();
            Assert.True(menu.AddMenu(submenu));
            Assert.False(menu.AddMenu(null));
            Assert.False(menu.AddMenu(submenu));
            Assert.False(menu.AddMenu(menu));
        }

        [Fact]
        public void Exists()
        {
            string description = "fries";
            decimal price = 2m;

            var menu = new Lunch.Menu.Menu();
            Assert.False(menu.ItemExists(null));
            Assert.False(menu.ItemExists("soda"));
            Assert.False(menu.ItemExists(description));

            menu.AddItem(description, price);
            Assert.True(menu.ItemExists(description));
        }
    }
}