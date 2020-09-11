using PlatformFramework.EFCore.Eventing.Handlers;
using PlatformFramework.EFCore.Eventing.Requests;
using PlatformFramework.Eventing.Decorators.DatabaseRetry;
using System;

namespace Web.Service.BusinessLogic
{
    public class GetAllRequest : EntitySelectRequest<MyEntityModel>
    {
    }

    [DatabaseRetry(retryTimes: 2)]
    public class GetAllHandler : EntitySelectHandlerBase<MyEntity, MyEntityModel, GetAllRequest>
    {
        public GetAllHandler(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }
    }
}
