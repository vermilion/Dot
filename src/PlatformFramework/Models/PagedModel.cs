namespace PlatformFramework.Models
{
    public class PagedModel
    {
        public PagedModel(int? limit = null, int? offset = null)
        {
            Limit = limit;
            Offset = offset;
        }

        public int? Limit { get; }
        public int? Offset { get; }
    }
}