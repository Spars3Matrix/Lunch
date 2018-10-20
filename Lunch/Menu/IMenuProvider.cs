namespace Lunch.Menu
{
    public interface IMenuProvider
    {
        Menu Menu { get; }
        void InvalidateMenu();
    }
}