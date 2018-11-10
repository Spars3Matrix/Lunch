using System;
using System.Collections.Generic;
using Lunch.Menu;
using Lunch.Order;
using Lunch.Schedule;

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
            Scheduler scheduler = new Scheduler();
            return service.GetItems(command != null && command.Trim() == "all" ? null : initiator, scheduler.Start, scheduler.End).Coalesce();
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