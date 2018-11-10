using System;
using System.Collections.Generic;
using Lunch.Data;

namespace Lunch.Order
{
    public class OrderRepository : IRepository
    {
        // TODO implement cache
        
        private OrderDao Dao = new OrderDao();

        public IEnumerable<OrderItem> GetByPerson(string person, ResultFilter filter) => Get(person, DateTime.MinValue, DateTime.MaxValue, filter);

        public IEnumerable<OrderItem> GetByDate(DateTime start, DateTime end, ResultFilter filter) => Get(null, start, end, filter);

        public IEnumerable<OrderItem> Get(string person, DateTime start, DateTime end, ResultFilter filter) => Dao.Get(person, start, end, filter);

        public OrderItem Save(OrderItem item) => Dao.Save(item);

        public void Delete(int id) => Dao.Delete(id);
    }
}