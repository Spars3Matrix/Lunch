using System.Collections.Generic;
using System.Linq;

namespace Lunch.Data
{
    public class ResultFilter
    {
        public static ResultFilter Default = new ResultFilter();

        public int Limit { get; set; } = 0;

        public int Offset { get; set; } = 0;

        public IQueryable<T> Filter<T>(IQueryable<T> query)
        {
            if (Offset > 0) query = query.Skip(Offset);
            if (Limit > 0) query = query.Take(Limit);
            return query;
        }

        public IEnumerable<T> Filter<T>(IEnumerable<T> collection)
        {
            if (Offset > 0) collection = collection.Skip(Offset);
            if (Limit > 0) collection = collection.Take(Limit);
            return collection;
        }
    }
}