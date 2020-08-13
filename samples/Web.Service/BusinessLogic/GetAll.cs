using AutoMapper;
using Microsoft.Extensions.Logging;
using PlatformFramework.EFCore.Context;
using PlatformFramework.EFCore.Eventing.Handlers;
using PlatformFramework.EFCore.Eventing.Requests;

namespace Web.Service.BusinessLogic
{
    public class GetAllRequest : EntitySelectRequest<MyEntityModel>
    {
    }

    public class GetAllHandler : EntitySelectHandlerBase<MyEntity, MyEntityModel, GetAllRequest>
    {
        public GetAllHandler(ILoggerFactory loggerFactory, IUnitOfWork unitOfWork, IMapper mapper)
            : base(loggerFactory, unitOfWork, mapper)
        {
        }
    }
}
