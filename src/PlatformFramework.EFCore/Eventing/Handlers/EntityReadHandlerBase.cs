using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PlatformFramework.EFCore.Abstractions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlatformFramework.EFCore.Eventing.Handlers
{
    /// <summary>
    /// Base class implementing Entity get scenario
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TReadModel">Model type</typeparam>
    /// <typeparam name="TRequest">Unique request type</typeparam>
    /// <typeparam name="TResponse">Response type</typeparam>
    public abstract class EntityReadHandlerBase<TEntity, TReadModel, TRequest, TResponse> : EntityHandlerBase<TRequest, TResponse>
        where TEntity : class, IEntity, new()
        where TRequest : IRequest<TResponse>
    {
        protected EntityReadHandlerBase(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        /// <summary>
        /// Override this for custom entity retrival process
        /// </summary>
        /// <param name="key">Entity key</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns>Entity Model</returns>
        protected virtual async Task<TReadModel> Read(long key, CancellationToken cancellationToken = default)
        {
            var model = await UnitOfWork.Set<TEntity>()
                .AsNoTracking()
                .Where(p => Equals(p.Id, key))
                .ProjectTo<TReadModel>(Mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);

            return model;
        }

        /// <summary>
        /// Override this for custom entity query process
        /// </summary>
        /// <param name="key">Entity key</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns>Tracked Entity</returns>
        protected virtual async Task<TEntity> ReadEntity(long key, CancellationToken cancellationToken = default)
        {
            var entity = await UnitOfWork.Set<TEntity>()
                 .Where(p => Equals(p.Id, key))
                 .SingleOrDefaultAsync(cancellationToken)
                 .ConfigureAwait(false);

            return entity;
        }
    }
}
