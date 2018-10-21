using System;
using System.Collections.Generic;

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
    }
}