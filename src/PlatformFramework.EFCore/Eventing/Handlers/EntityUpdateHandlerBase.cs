using PlatformFramework.EFCore.Abstractions;
using PlatformFramework.EFCore.Eventing.Requests;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PlatformFramework.EFCore.Eventing.Handlers
{
    /// <summary>
    /// Base class implementing Entity update scenario
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TReadModel">Model type</typeparam>
    /// <typeparam name="TRequest">Unique request type</typeparam>
    public abstract class EntityUpdateHandlerBase<TEntity, TReadModel, TRequest> : EntityReadHandlerBase<TEntity, TReadModel, TRequest, TReadModel>
        where TEntity : class, IEntity, new()
        where TRequest : EntityUpdateRequest<TReadModel>
    {
        protected EntityUpdateHandlerBase(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        public override async Task<TReadModel> Handle(TRequest request, CancellationToken cancellationToken)
        {
            // find entity to update by message id, not model id
            var entity = await ReadEntity(request.Id, cancellationToken)
                .ConfigureAwait(false);

            if (entity == null)
                return default!;

            // copy updates from model to entity
            Mapper.Map(request.Model, entity);

            // restore Id
            entity.Id = request.Id;

            // save updates
            await UnitOfWork
                .SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);

            // return read model
            var readModel = await Read(entity.Id, cancellationToken)
                .ConfigureAwait(false);

            return readModel;
        }
    }
}
