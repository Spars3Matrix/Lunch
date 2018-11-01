using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Lunch.Data
{
    public class ResultFilter
    {
        public static ResultFilter Default = new ResultFilter();

        [FromQuery(Name = "limit")]
        public int Limit { get; set; } = 0;

        [FromQuery(Name = "offset")]
        public int Offset { get; set; } = 0;

        public IQueryable<T> Filter<T>(IQueryable<T> query)
        {
            if (Offset > 0) query = query.Skip(Offset);
            if (Limit > 0) query = query.Take(Limit);
            return query;
        }
    }
}