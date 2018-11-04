using System;
using System.Collections.Generic;
using Lunch.Menu;
using Lunch.Order;

namespace Lunch.Commands
{
    public class CommandHandler : ICommandHandler
    {
        public virtual IEnumerable<MenuItem> ListMenu(string initiator, string command)
        {
            return new MenuService().GetItems();
        }

        public virtual IEnumerable<OrderItem> ListOrder(string initiator, string command)
        {
            OrderService service = new OrderService();
            return command != null && command.Trim().ToLower().StartsWith("all") ?
                service.GetItems(DateTime.MinValue, DateTime.MaxValue) :
                service.GetItems(initiator);
        }

        public virtual OrderResult Order(string initiator, string command)
        {
            command = command.Trim();

            string amount = "";
            foreach (char c in command)
            {
                if (char.IsDigit(c)) amount += c;
                else break;
            }
            string description = command.Substring(amount.Length).Trim();

            OrderService service = new OrderService();
            return amount.Length > 0 ?
                service.Order(description, initiator, int.Parse(amount)) :
                service.IncrementOrder(description, initiator);
        }
    }
}