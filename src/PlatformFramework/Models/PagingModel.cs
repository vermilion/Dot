namespace PlatformFramework.Domain.Domain
{
    public class PagingModel
    {
        public PagingModel(int? limit = null, int? offset = null)
        {
            Limit = limit;
            Offset = offset;
        }

        public int? Limit { get; }
        public int? Offset { get; }
    }

}