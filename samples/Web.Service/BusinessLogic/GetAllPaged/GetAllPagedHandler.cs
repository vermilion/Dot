using AutoMapper;
using Microsoft.Extensions.Logging;
using PlatformFramework.EFCore.Context;
using PlatformFramework.EFCore.Eventing.Handlers;

namespace Web.Service.BusinessLogic
{
    public class GetAllPagedHandler : EntitySelectPagedHandlerBase<MyEntity, MyEntityModel, GetAllPagedRequest>
    {
        public GetAllPagedHandler(ILoggerFactory loggerFactory, IUnitOfWork unitOfWork, IMapper mapper)
            : base(loggerFactory, unitOfWork, mapper)
        {
        }
    }
}
