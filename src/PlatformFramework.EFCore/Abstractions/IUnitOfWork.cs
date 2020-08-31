using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace PlatformFramework.EFCore.Abstractions
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
        /// Allows to use existing transaction
        /// </summary>
        /// <param name="transaction">Instance</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="Task"/></returns>
        Task UseTransaction(DbTransaction transaction, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets underlying connection
        /// </summary>
        DbConnection Connection { get; }

        /// <summary>
        /// Currently used transaction
        /// </summary>
        IDbContextTransaction? Transaction { get; }

        /// <summary>
        /// Starts the transaction
        /// </summary>
        /// <param name="isolationLevel"><see cref="IsolationLevel"/></param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns>Transaction</returns>
        Task<IDbContextTransaction> BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, CancellationToken cancellationToken = default);

        /// <summary>
        /// Commits the transaction
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="Task"/></returns>
        Task CommitTransaction(CancellationToken cancellationToken = default);

        /// <summary>
        /// Rolls back entire transaction
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="Task"/></returns>
        Task RollbackTransaction(CancellationToken cancellationToken = default);
    }
}