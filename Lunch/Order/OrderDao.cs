using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lunch.Data;

namespace Lunch.Order
{
    public class OrderDao : Dao
    {

        public IEnumerable<OrderItem> Get(string person, DateTime start, DateTime end, ResultFilter filter)
        {
            return Execute(database => FindEntities(database.OrderItems, o =>
                (string.IsNullOrEmpty(person) || o.Person == person) && 
                (start == DateTime.MinValue || o.Modified >= start) && 
                (end == DateTime.MaxValue || o.Modified <= end), filter));
        }

        public OrderItem Save(OrderItem item)
        {
            return Execute(database =>
            {
                AddOrUpdateEntity(item, database.OrderItems, i => i.Id == item.Id);
                database.SaveChanges();
                return item;
            });
        }

        protected async Task<OrderItem> SaveAsync(OrderItem item)
        {
            return await Execute(async database =>
            {
                AddOrUpdateEntity(item, database.OrderItems, i => i.Id == item.Id);
                await database.SaveChangesAsync();
                return item;
            });
        }

        public void Delete(int id)
        {
            Execute(database =>
            {
                DeleteEntity(id, database.OrderItems, s => s.Id == id);
                database.SaveChanges();
            });
        }

        protected async void DeleteAsync(int id)
        {
            await Execute(async database =>
            {
                DeleteEntity(id, database.OrderItems, s => s.Id == id);
                await database.SaveChangesAsync();
            });
        }
    }
}