using System.Collections.Generic;

namespace PlatformFramework.Models
{
    public class PagedModel<T> where T : class
    {
        public PagedModel(IEnumerable<T> collection, long total)
        {
            Collection = collection;
            Total = total;
        }

        public IEnumerable<T> Collection { get; }

        public long Total { get; }
    }
}