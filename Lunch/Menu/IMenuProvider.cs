using System;

namespace Lunch.Menu
{
    public delegate void MenuUpdated(Menu menu);

    public interface IMenuProvider
    {
        Menu Menu { get; }

        void InvalidateMenu();

        event MenuUpdated MenuUpdated;
    }
}