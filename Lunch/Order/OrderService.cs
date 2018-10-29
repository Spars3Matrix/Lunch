using System;
using System.Collections.Generic;
using System.Linq;
using Lunch.Menu;

namespace Lunch.Order
{
    public class OrderService
    {
        public IEnumerable<OrderItem> GetItems(string person)
        {
            return new OrderRepository().GetByPerson(person);
        }

        public OrderResult IncrementOrder(string description, string person, int amount = 1)
        {
            int currentAmount = GetOrCreate(description, person)?.Amount ?? 0;
            return Order(description, person, currentAmount + 1);
        }

        public OrderResult DecrementOrder(string description, string person, int amount = 1)
        {
            int currentAmount = GetOrCreate(description, person)?.Amount ?? 0;
            return Order(description, person, currentAmount - 1);
        }

        public OrderResult Order(string description, string person, int amount) {
            OrderResult result = new OrderResult();

            // TODO clean this mess of if statements

            if (amount < 0)
            {
                result.Exception = OrderException.NewAmountLowerThanZero;
                return result;
            }

            if (string.IsNullOrEmpty(person))
            {
                result.Exception = OrderException.PersonNotProvided;
                return result;
            }

            MenuItem menuItem = new MenuService().Menu.GetItem(description);
            if (menuItem == null)
            {
                result.Exception = OrderException.MenuItemDoesNotExist;
                return result;
            } 

            OrderItem item = GetOrCreate(description, person);
            item.Amount = amount;

            result.OrderItem = new OrderRepository().Save(item);
            
            return result;
        }

        private OrderItem GetOrCreate(string description, string person)
        {
            return new OrderRepository()
                .GetByPerson(person)
                .FirstOrDefault(o => o.Description == description) ?? new OrderItem(description, person);
        }
    }
}