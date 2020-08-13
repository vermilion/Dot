using AutoMapper;
using Microsoft.Extensions.Logging;
using PlatformFramework.EFCore.Context;
using PlatformFramework.EFCore.Eventing.Handlers;
using PlatformFramework.EFCore.Eventing.Requests;

namespace Web.Service.BusinessLogic
{
    public class DeleteRequest : EntityDeleteRequest<MyEntityModel>
    {
        public DeleteRequest(long id) : base(id)
        {
        }
    }

    public class DeleteHandler : EntityDeleteHandlerBase<MyEntity, MyEntityModel, DeleteRequest>
    {
        public DeleteHandler(ILoggerFactory loggerFactory, IUnitOfWork unitOfWork, IMapper mapper)
            : base(loggerFactory, unitOfWork, mapper)
        {
        }
    }
}
