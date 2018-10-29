using System;
using System.Collections.Generic;
using Lunch.Data;

namespace Lunch.Order
{
    public class OrderRepository : IRepository
    {
        // TODO implement cache
        
        private OrderDao Dao = new OrderDao();

        public IEnumerable<OrderItem> GetByPerson(string person) => Dao.GetByPerson(person);

        public IEnumerable<OrderItem> GetByDate(DateTime start, DateTime end) => Dao.GetByDate(start, end);

        public OrderItem Save(OrderItem item) => Dao.Save(item);

        public void Delete(int id) => Dao.Delete(id);
    }
}