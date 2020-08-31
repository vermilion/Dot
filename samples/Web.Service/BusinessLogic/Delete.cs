using PlatformFramework.EFCore.Eventing.Handlers;
using PlatformFramework.EFCore.Eventing.Requests;
using System;

namespace Web.Service.BusinessLogic
{
    public class DeleteRequest : EntityDeleteRequest<MyEntityModel>
    {
        public DeleteRequest(int id) : base(id)
        {
        }
    }

    public class DeleteHandler : EntityDeleteHandlerBase<MyEntity, MyEntityModel, DeleteRequest>
    {
        public DeleteHandler(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }
    }
}
