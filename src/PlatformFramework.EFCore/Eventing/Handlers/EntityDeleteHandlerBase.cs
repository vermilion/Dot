using PlatformFramework.EFCore.Abstractions;
using PlatformFramework.EFCore.Eventing.Requests;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PlatformFramework.EFCore.Eventing.Handlers
{
    /// <summary>
    /// Base class implementing Entity delete scenario
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TReadModel">Model type</typeparam>
    /// <typeparam name="TRequest">Unique request type</typeparam>
    public abstract class EntityDeleteHandlerBase<TEntity, TReadModel, TRequest> : EntityReadHandlerBase<TEntity, TReadModel, TRequest, TReadModel>
       where TEntity : class, IEntity, new()
       where TRequest : EntityDeleteRequest<TReadModel>
    {
        protected EntityDeleteHandlerBase(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        public override async Task<TReadModel> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var dbSet = UnitOfWork.Set<TEntity>();

            var keyValue = new object[] { request.Id };

            var entity = await dbSet
                .FindAsync(keyValue, cancellationToken)
                .ConfigureAwait(false);

            if (entity == null)
                return default!;

            dbSet.Remove(entity);

            await UnitOfWork
                .SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);

            // convert deleted entity to read model
            return Mapper.Map<TReadModel>(entity);
        }
    }
}
