using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlatformFramework.EFCore.Abstractions;
using PlatformFramework.Extensions;
using PlatformFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlatformFramework.EFCore.Entities
{
    public class EntityService<TEntity> : IEntityService<TEntity>
        where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// Gets entity Set
        /// </summary>
        protected DbSet<TEntity> DbSet => UnitOfWork.Set<TEntity>();

        /// <summary>
        /// Current instance logger <see cref="ILogger"/>
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// Unit of work <see cref="IUnitOfWork"/>
        /// </summary>
        protected IUnitOfWork UnitOfWork { get; }

        protected EntityService(
            ILoggerFactory loggerFactory,
            IUnitOfWork unitOfWork)
        {
            Logger = Guard.Against.Null(loggerFactory, nameof(loggerFactory))
                .CreateLogger(GetType().Name);

            UnitOfWork = Guard.Against.Null(unitOfWork, nameof(unitOfWork));
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }

        public virtual async Task<PagedCollection<TModel>> GetAllPaged<TModel>(int? offset, int? limit,
            Func<IQueryable<TEntity>, IQueryable<TEntity>>? filter = default,
            CancellationToken cancellationToken = default)
            where TModel : class
        {
            var query = DbSet.AsNoTracking();

            // build query from filter
            if (filter != null)
                query = filter.Invoke(query);

            // get total for query
            var total = await query.CountAsync(cancellationToken).ConfigureAwait(false);

            // short circuit if total is zero
            if (total == 0)
                return new PagedCollection<TModel>(new List<TModel>(), total);

            // page the query and convert to read model
            if (offset.HasValue)
                query = query.Skip(offset.Value);

            if (limit.HasValue)
                query = query.Take(limit.Value);

            var result = await query
                .Project<TModel>()
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false); ;

            return new PagedCollection<TModel>(result, total);
        }

        public virtual async Task<TEntity> Add(TEntity entity, CancellationToken cancellationToken = default)
        {
            // add to data set, id should be generated
            var entry = await DbSet.AddAsync(entity, cancellationToken);
            return entry.Entity;
        }

        public virtual void Update(TEntity entity)
        {
            var entry = UnitOfWork.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        public virtual async Task Delete(object[] keyValues, CancellationToken cancellationToken = default)
        {
            var entity = await DbSet.FindAsync(keyValues, cancellationToken);
            if (entity != null)
            {
                Delete(entity);
            }
        }

        public virtual void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
        }
    }
}
