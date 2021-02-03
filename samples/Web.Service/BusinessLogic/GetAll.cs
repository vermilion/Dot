using PlatformFramework.EFCore.Eventing.Handlers;
using PlatformFramework.EFCore.Eventing.Requests;
using System;

namespace Web.Service.BusinessLogic
{
    public class GetAllRequest : EntitySelectRequest<MyEntityModel>
    {
    }

    public class GetAllHandler : EntitySelectHandlerBase<MyEntity, MyEntityModel, GetAllRequest>
    {
        public GetAllHandler(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }
    }
}
