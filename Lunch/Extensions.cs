using System;
using System.Collections.Generic;
using System.Linq;
using Lunch.Data;
using Lunch.Order;

namespace Lunch
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (T entry in collection)
            {
                action(entry);
            }

            return collection;
        }

        public static IEnumerable<T> Filter<T>(this IEnumerable<T> collection, ResultFilter filter)
        {
            return (filter ?? ResultFilter.Default).Filter(collection);
        }
    }

    public static class IQueryableExtensions
    {
        public static IQueryable<T> Filter<T>(this IQueryable<T> collection, ResultFilter filter)
        {
            return (filter ?? ResultFilter.Default).Filter(collection);
        }
    }

    public static class OrderIEnumerableExtensions
    {
        public static IEnumerable<OrderItem> Coalesce(this IEnumerable<OrderItem> collection)
        {
            return collection
                .GroupBy(o => o.Description)
                .Select(c => 
                {
                    OrderItem item = c.First();
                    item.Amount = c.Sum(o => o.Amount);
                    item.Person = null;
                    return item;
                });
        }
    }
}