using System.Collections.Generic;
using System.Linq;

namespace PlatformFramework.Models
{
    public class PagedCollection<T>
        where T : class
    {
        public PagedCollection(IEnumerable<T> collection, int total)
        {
            Collection = collection.ToList();
            Total = total;
        }

        public IReadOnlyCollection<T> Collection { get; }

        public int Total { get; }
    }
}