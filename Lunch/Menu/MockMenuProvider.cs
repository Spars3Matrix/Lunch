namespace Lunch.Menu
{
    public class MockMenuProvider : MenuProvider
    {
        protected override Menu CreateMenu()
        {
            Menu menu = new Menu();
            menu.AddItem("fries", 1.60m);
            menu.AddItem("soup", 2m);
            menu.AddItem("coffee", 2.50m);
            return menu;
        }

        public override void InvalidateMenu()
        {
            menu = null;
        }
    }
}