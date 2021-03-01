using PlatformFramework.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlatformFramework.EFCore.Abstractions
{
    /// <summary>
    /// Services for entity type
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity</typeparam>
    public interface IEntityService<TEntity> 
        where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// Gets a queryable to underlying table
        /// </summary>
        /// <returns>A <see cref="IQueryable{TEntity}"/></returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Gets the collection as <see cref="PagedCollection{TModel}"/>
        /// </summary>
        /// <typeparam name="TModel">Projection type</typeparam>
        /// <param name="offset">Page offset</param>
        /// <param name="limit">Page size</param>
        /// <param name="filter">Predicate filter for query</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns></returns>
        Task<PagedCollection<TModel>> GetAllPaged<TModel>(int? offset, int? limit,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> filter = default,
            CancellationToken cancellationToken = default)
            where TModel : class;

        /// <summary>
        /// Inserts a new entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous insert operation.</returns>
        Task<TEntity> Add(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes the entity by the specified primary key.
        /// </summary>
        /// <param name="keyValues">The primary key value.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        Task Delete(object[] keyValues, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Update(TEntity entity);
    }
}