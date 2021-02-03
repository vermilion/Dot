using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using PlatformFramework.EFCore.Abstractions;
using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace PlatformFramework.EFCore.Context
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _context;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public DbConnection Connection => _context.Database.GetDbConnection();
        public IDbContextTransaction? Transaction { get; private set; }

        public async Task<IDbContextTransaction> BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, CancellationToken cancellationToken = default)
        {
            if (Transaction != null)
                return Transaction;

            return Transaction = await _context.Database.BeginTransactionAsync(isolationLevel, cancellationToken).ConfigureAwait(false);
        }

        public async Task CommitTransaction(CancellationToken cancellationToken = default)
        {
            if (Transaction == null)
                throw new NullReferenceException("Please call `BeginTransaction()` method first.");

            try
            {
                await Transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
            }
            catch
            {
                await RollbackTransaction(cancellationToken).ConfigureAwait(false);
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
                await Transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
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
            return _context.Database.UseTransactionAsync(transaction, cancellationToken);
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        public DbSet<TEntity> Set<TEntity>()
            where TEntity : class
        {
            return _context.Set<TEntity>();
        }

        public EntityEntry<TEntity> Entry<TEntity>(TEntity entity)
            where TEntity : class
        {
            return _context.Entry(entity);
        }

        public IEntityService<TEntity> GetEntityServiceFor<TEntity>() 
            where TEntity : class, IEntity, new()
        {
            return _context.GetService<IEntityService<TEntity>>();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null!;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}