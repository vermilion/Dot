using AutoMapper;
using Microsoft.Extensions.Logging;
using PlatformFramework.EFCore.Context;
using PlatformFramework.EFCore.Eventing.Handlers;

namespace Web.Service.BusinessLogic
{
    public class DeleteHandler : EntityDeleteHandlerBase<MyEntity, MyEntityModel, DeleteRequest>
    {
        public DeleteHandler(ILoggerFactory loggerFactory, IUnitOfWork unitOfWork, IMapper mapper) 
            : base(loggerFactory, unitOfWork, mapper)
        {
        }
    }
}
