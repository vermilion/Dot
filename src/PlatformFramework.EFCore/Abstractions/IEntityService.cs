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

        /// <summary>
        /// Defines entity projection process
        /// </summary>
        /// <param name="query">Source query</param>
        /// <returns>Projected query</returns>
        IQueryable<TModel> ProjectTo<TModel>(IQueryable<TEntity> query);

        /// <summary>
        /// Defines entity projection process
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Projected model</returns>
        TModel ProjectTo<TModel>(TEntity entity);

        /// <summary>
        /// Defines entity projection process
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>Entity</returns>
        TEntity ProjectFrom<TModel>(TModel model);
    }
}