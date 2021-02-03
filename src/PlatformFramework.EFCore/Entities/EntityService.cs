using Ardalis.GuardClauses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PlatformFramework.EFCore.Abstractions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlatformFramework.EFCore.Entities
{
    public abstract class EntityService
    {
        /// <summary>
        /// Current instalce logger <see cref="ILogger"/>
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// Unit of work <see cref="IUnitOfWork"/>
        /// </summary>
        protected IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Mapper <see cref="IMapper"/>
        /// </summary>
        protected IMapper Mapper { get; }

        protected EntityService(IServiceProvider serviceProvider)
        {
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
            var mapper = serviceProvider.GetRequiredService<IMapper>();

            Logger = Guard.Against.Null(loggerFactory, nameof(loggerFactory))
                .CreateLogger(GetType().Name);

            UnitOfWork = Guard.Against.Null(unitOfWork, nameof(unitOfWork));
            Mapper = Guard.Against.Null(mapper, nameof(mapper));
        }
    }

    public class EntityService<TEntity> : EntityService, IEntityService<TEntity>
        where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// Gets entity Set
        /// </summary>
        protected DbSet<TEntity> DbSet => UnitOfWork.Set<TEntity>();

        public EntityService(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
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

        public virtual IQueryable<TModel> ProjectTo<TModel>(IQueryable<TEntity> query)
        {
            return query.ProjectTo<TModel>(Mapper.ConfigurationProvider);
        }

        public TModel ProjectTo<TModel>(TEntity entity)
        {
            return Mapper.Map<TModel>(entity);
        }

        public TEntity ProjectFrom<TModel>(TModel model)
        {
            return Mapper.Map<TEntity>(model);
        }
    }
}
