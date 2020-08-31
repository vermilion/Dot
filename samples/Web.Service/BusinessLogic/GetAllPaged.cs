using PlatformFramework.EFCore.Eventing.Handlers;
using PlatformFramework.EFCore.Eventing.Requests;
using System;

namespace Web.Service.BusinessLogic
{
    public class GetAllPagedRequest : EntityPagedSelectRequest<MyEntityModel>
    {
    }

    public class GetAllPagedHandler : EntitySelectPagedHandlerBase<MyEntity, MyEntityModel, GetAllPagedRequest>
    {
        public GetAllPagedHandler(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }
    }
}
