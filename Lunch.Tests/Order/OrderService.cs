using System;
using Lunch.Order;
using Xunit;

namespace Lunch.Tests.Order
{
    public class OrderService : BaseTest
    {
        [Fact]
        public void GetItemsByPerson()
        {
            var service = new Lunch.Order.OrderService();
            string person = "Mini";

            Assert.Empty(service.GetItems(person));

            service.Order("fries", person, 1);
            Assert.NotEmpty(service.GetItems(person));
        }

        [Fact]
        public void Order()
        {
            var service = new Lunch.Order.OrderService();
            string person = "Mini";
            string description = "fries";

            {
                OrderResult result = service.Order(description, person, 1);
                Assert.True(result.Successful);
            }

            {
                OrderResult result = service.Order(description, person, 2);
                Assert.True(result.Successful);
                Assert.Equal(2, result.OrderItem.Amount);
            }

            {
                OrderResult result = service.Order(description, person, -1);
                Assert.False(result.Successful);
                Assert.Equal(OrderException.NewAmountLowerThanZero, result.Exception);
            }

            {
                OrderResult result = service.Order("aint got this item", person, 1);
                Assert.False(result.Successful);
                Assert.Equal(OrderException.MenuItemDoesNotExist, result.Exception);
            }

            {
                OrderResult result = service.Order(null, person, 1);
                Assert.False(result.Successful);
                Assert.Equal(OrderException.MenuItemDoesNotExist, result.Exception);
            }

            {
                OrderResult result = service.Order(description, null, 1);
                Assert.False(result.Successful);
                Assert.Equal(OrderException.PersonNotProvided, result.Exception);
            }
        }

        [Fact]
        public void IncrementOrder()
        {
            var service = new Lunch.Order.OrderService();
            string person = "Mini";
            string description = "fries";

            Action restoreAmount = () => service.Order(description, person, 0);

            {
                OrderResult result = service.IncrementOrder(description, person);
                Assert.True(result.Successful);
                Assert.Equal(1, result.OrderItem.Amount);
                restoreAmount();
            }

            {
                OrderResult result = service.IncrementOrder(description, person, 2);
                Assert.True(result.Successful);
                Assert.Equal(2, result.OrderItem.Amount);
                restoreAmount();
            }

            {
                service.IncrementOrder(description, person);
                OrderResult result = service.IncrementOrder(description, person, 2);
                Assert.True(result.Successful);
                Assert.Equal(3, result.OrderItem.Amount);
                restoreAmount();
            }

            // the rest of the tests are int Order()
        }

        [Fact]
        public void DecrementOrder()
        {
            var service = new Lunch.Order.OrderService();
            string person = "Mini";
            string description = "fries";

            Action restoreAmount = () => service.Order(description, person, 5);

            {
                restoreAmount();
                OrderResult result = service.DecrementOrder(description, person);
                Assert.True(result.Successful);
                Assert.Equal(4, result.OrderItem.Amount);
            }

            {
                OrderResult result = service.DecrementOrder(description, person, 2);
                Assert.True(result.Successful);
                Assert.Equal(3, result.OrderItem.Amount);
                restoreAmount();
            }

            {
                service.DecrementOrder(description, person);
                OrderResult result = service.DecrementOrder(description, person, 2);
                Assert.True(result.Successful);
                Assert.Equal(2, result.OrderItem.Amount);
                restoreAmount();
            }

            // the rest of the tests are int Order()
        }
    }
}