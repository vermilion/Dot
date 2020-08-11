using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PlatformFramework.EFCore.Entities;

namespace PlatformFramework.EFCore.Context
{
    /// <summary>
    /// Framework's DbContext encapsulating all the entities/hooks/etc.. logic
    /// </summary>
    public abstract class DbContextCore : DbContext, IUnitOfWork
    {
        protected DbContextCore(DbContextOptions options)
            : base(options)
        {
        }

        public DbConnection Connection => Database.GetDbConnection();
        public IDbContextTransaction? Transaction { get; private set; }

        public async Task<IDbContextTransaction> BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, CancellationToken cancellationToken = default)
        {
            if (Transaction != null)
                return Transaction;

            return Transaction = await Database.BeginTransactionAsync(isolationLevel, cancellationToken);
        }

        public async Task CommitTransaction(CancellationToken cancellationToken = default)
        {
            if (Transaction == null)
                throw new NullReferenceException("Please call `BeginTransaction()` method first.");

            try
            {
                await Transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await RollbackTransaction(cancellationToken);
                throw;
            }
            finally
            {
                if (Transaction != null)
                {
                    Transaction.Dispose();
                    Transaction = null;
                }
            }
        }

        public async Task RollbackTransaction(CancellationToken cancellationToken = default)
        {
            if (Transaction == null)
                throw new NullReferenceException("Please call `BeginTransaction()` method first.");

            try
            {
                await Transaction.RollbackAsync(cancellationToken);
            }
            finally
            {
                if (Transaction != null)
                {
                    Transaction.Dispose();
                    Transaction = null;
                }
            }
        }

        public Task UseTransaction(DbTransaction transaction, CancellationToken cancellationToken = default)
        {
            return Database.UseTransactionAsync(transaction, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyEntitiesConfiguration(this);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return this.SaveChangesWithHooks(OnSaveCompleted, cancellationToken);
        }

        public override int SaveChanges()
        {
            return this.SaveChangesWithHooks(OnSaveCompleted).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Callback executed once save complete
        /// </summary>
        /// <param name="context">Context <see cref="EntityChangeContext"/></param>
        protected virtual void OnSaveCompleted(EntityChangeContext context)
        {
        }
    }
}