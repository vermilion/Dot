using MediatR;
using PlatformFramework.Models;

namespace PlatformFramework.EFCore.Eventing.Requests
{
    /// <summary>
    /// Base paged select entity request type
    /// </summary>
    /// <typeparam name="TReadModel">Model</typeparam>
    public abstract class EntityPagedSelectRequest<TReadModel> : IRequest<PagedModel<TReadModel>>
        where TReadModel : class
    {
        protected EntityPagedSelectRequest(int? limit = null, int? offset = null)
        {
            Limit = limit;
            Offset = offset;
        }

        public int? Limit { get; }
        public int? Offset { get; }
    }
}
