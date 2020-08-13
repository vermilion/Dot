using AutoMapper;
using Microsoft.Extensions.Logging;
using PlatformFramework.EFCore.Context;
using PlatformFramework.EFCore.Eventing.Handlers;

namespace Web.Service.BusinessLogic
{
    public class UpdateHandler : EntityUpdateHandlerBase<MyEntity, MyEntityModel, UpdateRequest>
    {
        public UpdateHandler(ILoggerFactory loggerFactory, IUnitOfWork unitOfWork, IMapper mapper) 
            : base(loggerFactory, unitOfWork, mapper)
        {
        }
    }
}
