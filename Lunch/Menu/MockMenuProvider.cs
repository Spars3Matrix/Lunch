namespace Lunch.Menu
{
    public class MockMenuProvider : MenuProvider
    {
        protected override Menu CreateMenu()
        {
            Menu menu = new Menu();
            menu.Add("fries", 1.60m);
            menu.Add("soup", 2m);
            menu.Add("coffee", 2.50m);
            return menu;
        }

        public override void InvalidateMenu()
        {
            menu = null;
        }
    }
}