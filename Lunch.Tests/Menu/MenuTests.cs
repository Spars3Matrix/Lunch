using Xunit;

namespace Lunch.Tests.Menu
{
    public class Menu
    {
        [Fact]
        public void Add()
        {
            string fries = "fries";
            string soda = "soda";
            
            var menu = new Lunch.Menu.Menu();
            Assert.True(menu.Add(fries, 2m));
            Assert.False(menu.Add(fries, 1.5m));
            Assert.False(menu.Add(soda, -1m));
            Assert.False(menu.Add(null, 1m));
            Assert.False(menu.Add("", 1m));
        }

        [Fact]
        public void Exists()
        {
            string description = "fries";
            decimal price = 2m;

            var menu = new Lunch.Menu.Menu();
            Assert.False(menu.Exists(null));
            Assert.False(menu.Exists("soda"));
            Assert.False(menu.Exists(description));

            menu.Add(description, price);
            Assert.True(menu.Exists(description));
        }

        [Fact]
        public void GetItem()
        {
            string description = "fries";
            decimal price = 2m;

            var menu = new Lunch.Menu.Menu();
            Assert.Null(menu.GetItem(null));
            Assert.Null(menu.GetItem(description));

            menu.Add(description, price);
            Assert.True(menu.Exists(description));
        }
    }
}