using System;
using System.Collections.Generic;
using System.Linq;
using Lunch.Data;
using Lunch.Menu;

namespace Lunch.Order
{
    public class OrderService
    {
        public IEnumerable<OrderItem> GetItems(string person, ResultFilter filter = null)
        {
            return new OrderRepository().GetByPerson(person, filter);
        }

        public IEnumerable<OrderItem> GetItems(DateTime start, DateTime end, ResultFilter filter = null)
        {
            return new OrderRepository().GetByDate(start, end, filter);
        }

        public IEnumerable<OrderItem> GetItems(string person, DateTime start, DateTime end, ResultFilter filter = null)
        {
            return new OrderRepository().Get(person, start, end, filter);
        }

        public OrderResult IncrementOrder(string description, string person, int amount = 1)
        {
            int currentAmount = GetOrCreate(description, person)?.Amount ?? 0;
            return Order(description, person, currentAmount + amount);
        }

        public OrderResult DecrementOrder(string description, string person, int amount = 1)
        {
            int currentAmount = GetOrCreate(description, person)?.Amount ?? 0;
            return Order(description, person, currentAmount - amount);
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

            MenuItem menuItem = new MenuService().GetItem(description);
            if (menuItem == null)
            {
                result.Exception = OrderException.MenuItemDoesNotExist;
                return result;
            } 

            OrderItem item = GetOrCreate(menuItem.Description, person);
            item.Amount = amount;
            item.Price = menuItem.Price;

            result.OrderItem = new OrderRepository().Save(item);
            
            return result;
        }

        public void Cancel(string description, string person) {
            Order(description, person, 0);
        }

        private OrderItem GetOrCreate(string description, string person)
        {
            string realDescription = new MenuService().GetItem(description)?.Description;
            return new OrderRepository()
                .GetByPerson(person, ResultFilter.Default)
                .FirstOrDefault(o => o.Description == realDescription) ?? new OrderItem(realDescription, person);
        }
    }
}