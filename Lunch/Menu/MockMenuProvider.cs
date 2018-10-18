namespace Lunch.Menu
{
    public class MockMenuProvider : IMenuProvider
    {
        public Menu GetMenu()
        {
            Menu menu = new Menu();
            menu.Add("fries", 1.60m);
            menu.Add("soup", 2m);
            menu.Add("coffee", 2.50m);
            return menu;
        }
    }
}