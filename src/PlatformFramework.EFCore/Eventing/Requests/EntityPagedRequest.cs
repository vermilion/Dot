using PlatformFramework.Eventing;
using PlatformFramework.Models;

namespace PlatformFramework.EFCore.Eventing.Requests
{
    /// <summary>
    /// Base paged select entity request type
    /// </summary>
    /// <typeparam name="TReadModel">Model</typeparam>
    public abstract class EntityPagedSelectRequest<TReadModel> : PagedModel, IRequest<PagedCollection<TReadModel>>
        where TReadModel : class
    {
        protected EntityPagedSelectRequest(int? limit = null, int? offset = null)
            : base(limit, offset)
        {
        }
    }
}
