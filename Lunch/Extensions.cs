using System;
using System.Collections.Generic;
using System.Linq;
using Lunch.Data;

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
}