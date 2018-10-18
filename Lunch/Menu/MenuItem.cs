namespace Lunch.Menu
{
    public class MenuItem
    {
        public string Description { get; set; }
        public decimal Price { get; set; }

        public MenuItem(string description, decimal price)
        {
            Description = description;
            Price = price;
        }
    }
}