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

            modelBuilder.RegisterPlatformEntities(this);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return new PlatformDbContextExtensions(this).SaveChanges(OnSaveCompleted, cancellationToken);
        }

        public override int SaveChanges()
        {
            return new PlatformDbContextExtensions(this).SaveChanges(OnSaveCompleted).GetAwaiter().GetResult();
        }

        protected virtual void OnSaveCompleted(EntityChangeContext context)
        {
        }
    }
}