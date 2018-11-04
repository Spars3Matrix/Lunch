using System.Collections.Generic;
using System.Linq;
using Lunch.Data;

namespace Lunch.Search
{
    public class SimpleSearchEngine<T> : ISearchEngine<T>
    {
        private IEnumerable<T> Dataset { get; set; } = new List<T>();

        public void SetDataset(IEnumerable<T> dataset)
        {
            Dataset = dataset;
        }

        public IEnumerable<T> Search(string query, ResultFilter filter = null)
        {
            if (query != null) query = query.Trim().ToLower();
            
            return Dataset.Where(t =>
                t != null &&
                !string.IsNullOrEmpty(query) &&
                t.ToString().ToLower().Contains(query));
        }
    }
}