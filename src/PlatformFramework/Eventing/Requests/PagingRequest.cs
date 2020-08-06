namespace PlatformFramework.Domain.Domain
{
    /// <summary>
    /// Request used for paging
    /// </summary>
    public abstract class PagingRequest
    {
        public PagingRequest(int? limit = null, int? offset = null)
        {
            Limit = limit;
            Offset = offset;
        }

        public int? Limit { get; }
        public int? Offset { get; }
    }

}