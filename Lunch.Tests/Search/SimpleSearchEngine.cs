using Lunch.Menu;
using Lunch.Search;
using Xunit;

namespace Lunch.Tests.Search
{
    [Collection("Search")]
    public class SimpleSearchEngine : BaseTest
    {
        protected override void Initialize()
        {
            base.Initialize();
            new MenuService().SetSearchEngine(new SimpleSearchEngine<MenuItem>());
        }

        [Fact]
        public void Search()
        {
            MenuService service = new MenuService();

            Assert.NotNull(service.GetItem("fries"));
            Assert.NotNull(service.GetItem("fri"));
            Assert.NotNull(service.GetItem("FrIEs"));
            Assert.Null(service.GetItem("pumpkin"));
            Assert.Null(service.GetItem("   "));
            Assert.Null(service.GetItem(null));
        }
    }
}