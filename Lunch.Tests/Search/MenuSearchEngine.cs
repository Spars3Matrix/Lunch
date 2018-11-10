using Lunch.Menu;
using Lunch.Search;
using Xunit;
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace Lunch.Tests.Search
{
    [Collection("Search")]
    public class MenuSearchEngine : BaseTest
    {
        protected override void Initialize()
        {
            base.Initialize();
            new MenuService().SetSearchEngine(new Lunch.Search.MenuSearchEngine());
        }

        [Fact]
        public void Search()
        {
            MenuService service = new MenuService();

            Assert.NotNull(service.GetItem("fries"));
            Assert.NotNull(service.GetItem("fri"));
            Assert.NotNull(service.GetItem("FrIEs"));
            Assert.NotNull(service.GetItem("fris"));
            Assert.Null(service.GetItem("   "));
            Assert.Null(service.GetItem(null));
        }
    }
}