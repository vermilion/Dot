using Microsoft.EntityFrameworkCore;
using PlatformFramework.EFCore.Abstractions;
using PlatformFramework.EFCore.Eventing.Requests;
using PlatformFramework.Extensions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlatformFramework.EFCore.Eventing.Handlers
{
    /// <summary>
    /// Base class implementing Entity get single item scenario
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TReadModel">Model type</typeparam>
    /// <typeparam name="TRequest">Unique request type</typeparam>
    public abstract class EntitySelectSingleHandlerBase<TEntity, TReadModel, TRequest> : EntityHandlerBase<TRequest, TReadModel>
        where TEntity : class, IEntity, new()
        where TReadModel : class
        where TRequest : EntitySelectSingleRequest<TReadModel>
    {
        protected EntitySelectSingleHandlerBase(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        public override async Task<TReadModel> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var query = UnitOfWork
                .Set<TEntity>()
                .AsNoTracking();

            // build query from filter
            query = BuildQuery(request, query);

            // page the query and convert to read model
            var result = await QuerySelect(request, query, cancellationToken)
                .ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Override this for additional hook into entity query process
        /// </summary>
        /// <param name="request">Request instance</param>
        /// <param name="query">Query</param>
        /// <returns>Query</returns>
        protected virtual IQueryable<TEntity> BuildQuery(TRequest request, IQueryable<TEntity> query)
        {
            // build query from filter

            return query;
        }

        /// <summary>
        /// Override this for custom projection logic
        /// </summary>
        /// <param name="request">Request instance</param>
        /// <param name="query">Query</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns>Projected collection</returns>
        protected virtual async Task<TReadModel> QuerySelect(TRequest request, IQueryable<TEntity> query, CancellationToken cancellationToken)
        {
            return await query
                .Project<TReadModel>()
                .SingleOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
