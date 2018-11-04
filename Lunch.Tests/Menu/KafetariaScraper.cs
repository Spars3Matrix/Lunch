using Xunit;

namespace Lunch.Tests.Menu
{
    public class KafetariaScraper : BaseTest
    {
        [Fact]
        public void Scrape()
        {
            var scaper = new Lunch.Menu.Kafetaria.KafetariaScraper();
            var menu = scaper.Scrape();
        }
    }
}