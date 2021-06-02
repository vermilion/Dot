using Cofoundry.Core.Data;
using Cofoundry.Domain.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PlatformFramework.EFCore.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PlatformFramework.EFCore.Context
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContextCore _context;
        private readonly ITransactionScopeManager _transactionScopeManager;

        public UnitOfWork(
            DbContextCore context,
            ITransactionScopeManager transactionScopeManager)
        {
            _context = context;
            _transactionScopeManager = transactionScopeManager;
        }

        public ITransactionScope BeginTransaction()
        {
            return _transactionScopeManager.Create(_context);
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

        public DbContext Context()
        {
            return _context;
        }

        public EntityEntry<TEntity> Entry<TEntity>(TEntity entity)
            where TEntity : class
        {
            return _context.Entry(entity);
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