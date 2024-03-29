﻿using Cofoundry.Domain.Data;
using Dot.EFCore.Transactions.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Dot.EFCore.UnitOfWork
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

        public DbContext Context
        {
            get { return _context; }
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