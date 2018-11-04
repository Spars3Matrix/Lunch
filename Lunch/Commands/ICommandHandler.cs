using System.Collections.Generic;
using Lunch.Menu;
using Lunch.Order;

namespace Lunch.Commands
{
    public interface ICommandHandler
    {
        OrderResult Order(string initiator, string command);

        IEnumerable<OrderItem> ListOrder(string initiator, string command);

        IEnumerable<MenuItem> ListMenu(string initiator, string command);
    }
}