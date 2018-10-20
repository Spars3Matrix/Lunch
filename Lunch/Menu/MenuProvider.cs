namespace Lunch.Menu
{
    public abstract class MenuProvider : IMenuProvider
    {
        protected readonly object lck = new object();

        protected Menu menu;
        public Menu Menu => GetMenu();

        protected abstract Menu CreateMenu();

        public abstract void InvalidateMenu();

        protected virtual Menu GetMenu()
        {
            if (menu == null)
            {
                lock (lck)
                {
                    if (menu == null) 
                    {
                        menu = CreateMenu();
                    }
                }
            }

            return menu;
        }
    }
}