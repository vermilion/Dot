namespace PlatformFramework.Eventing.Requests
{
    /// <summary>
    /// Request used for paging
    /// </summary>
    public abstract class PagingRequest
    {
        protected PagingRequest(int? limit = null, int? offset = null)
        {
            Limit = limit;
            Offset = offset;
        }

        public int? Limit { get; }
        public int? Offset { get; }
    }

}