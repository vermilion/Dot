using System.Collections.Generic;

namespace PlatformFramework.Models
{
    public class ListModel<T> where T : class
    {
        public ListModel(IEnumerable<T> collection, long total)
        {
            Collection = collection;
            Total = total;
        }

        public IEnumerable<T> Collection { get; }

        public long Total { get; }
    }
}