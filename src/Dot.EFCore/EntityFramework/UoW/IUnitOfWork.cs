using Dot.EFCore.Transactions.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Dot.EFCore.UnitOfWork
{
    /// <summary>
    /// Unit-of-work abstraction hiding real DbContext type
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets entity Set
        /// </summary>
        /// <typeparam name="TEntity">Type of entity</typeparam>
        /// <returns>Set</returns>
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        /// <summary>
        /// Gets an underlying context
        /// </summary>
        /// <returns></returns>
        DbContext Context { get; }

        /// <summary>
        /// Gets <paramref name="entity"/> metadata
        /// </summary>
        /// <typeparam name="TEntity">Type of entity</typeparam>
        /// <param name="entity">Entity instance</param>
        /// <returns>Entity metadata</returns>
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Allows to save changes to context
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Starts the transaction
        /// </summary>
        /// <returns>Transaction</returns>
        ITransactionScope BeginTransaction();
    }
}