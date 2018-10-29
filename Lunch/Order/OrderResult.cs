namespace Lunch.Order
{
    public class OrderResult
    {
        public OrderException Exception { get; set; } = OrderException.None;
        public bool Successful => Exception == OrderException.None;
        public OrderItem OrderItem { get; set; }
    }

    public enum OrderException
    {
        None = 0,
        MenuItemDoesNotExist = 1,
        NewAmountLowerThanZero = 2,
        PersonNotProvided = 3,
        Unknown = 99
    }
}