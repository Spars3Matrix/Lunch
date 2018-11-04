using System.Collections.Generic;
using Lunch.Data;

namespace Lunch.Search
{
    public interface ISearchEngine<T>
    {
        void SetDataset(IEnumerable<T> dataset);
        
        IEnumerable<T> Search(string query, ResultFilter filter = null);
    }
}