using System;
using System.Collections.Generic;
using Lunch.Data;
using Lunch.Schedule;

namespace Lunch.Order
{
    public class OrderRepository : IRepository
    {
        // TODO implement cache
        
        private OrderDao Dao = new OrderDao();

        public IEnumerable<OrderItem> GetItems(string person, DateTime start, DateTime end, ResultFilter filter) => Dao.Get(person, start, end, filter);

        public OrderItem Save(OrderItem item) => Dao.Save(item);

        public void Delete(int id) => Dao.Delete(id);
    }
}