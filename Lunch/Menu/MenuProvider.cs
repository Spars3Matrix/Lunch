using System;

namespace Lunch.Menu
{
    public abstract class MenuProvider : IMenuProvider
    {
        protected readonly object lck = new object();

        protected Menu menu;
        public Menu Menu => GetMenu();

        public event MenuUpdated MenuUpdated;

        protected abstract Menu CreateMenu();

        public virtual void InvalidateMenu()
        {
            lock (lck)
            {
                menu = null;
            }
        }

        protected virtual Menu GetMenu()
        {
            if (menu == null)
            {
                lock (lck)
                {
                    if (menu == null) 
                    {
                        menu = CreateMenu();

                        if (MenuUpdated != null) MenuUpdated(menu);
                    }
                }
            }

            return menu;
        }
    }
}