using AutoMapper;
using Microsoft.Extensions.Logging;
using PlatformFramework.EFCore.Abstractions;
using PlatformFramework.EFCore.Context;
using PlatformFramework.EFCore.Eventing.Requests;
using System.Threading;
using System.Threading.Tasks;

namespace PlatformFramework.EFCore.Eventing.Handlers
{
    /// <summary>
    /// Base class implementing Entity creation scenario
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TReadModel">Model type</typeparam>
    /// <typeparam name="TRequest">Unique request type</typeparam>
    public abstract class EntityCreateHandlerBase<TEntity, TReadModel, TRequest> : EntityReadHandlerBase<TEntity, TReadModel, TRequest, TReadModel>
        where TEntity : class, IEntity, new()
        where TRequest : EntityCreateRequest<TReadModel>
    {
        protected EntityCreateHandlerBase(ILoggerFactory loggerFactory, IUnitOfWork unitOfWork, IMapper mapper)
            : base(loggerFactory, unitOfWork, mapper)
        {
        }

        public override async Task<TReadModel> Handle(TRequest request, CancellationToken cancellationToken)
        {
            // create new entity from model
            var entity = Mapper.Map<TEntity>(request.Model);

            var dbSet = UnitOfWork.Set<TEntity>();

            // add to data set, id should be generated
            await dbSet
                .AddAsync(entity, cancellationToken)
                .ConfigureAwait(false);

            // save to database
            await UnitOfWork
                .SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);

            // convert to read model
            var readModel = await Read(entity.Id, cancellationToken)
                .ConfigureAwait(false);

            return readModel;
        }
    }
}
