namespace Lunch.Menu
{
    public class MenuService
    {
        private static IMenuProvider MenuProvider;

        public Menu Menu => MenuProvider.Menu;

        public void SetMenuProvider(IMenuProvider menuProvider)
        {
            MenuProvider = menuProvider;
        }
    }
}