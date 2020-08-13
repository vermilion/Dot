using PlatformFramework.EFCore.Eventing.Requests;

namespace Web.Service.BusinessLogic
{
    public class CreateRequest : EntityCreateRequest<MyEntityModel>
    {
        public CreateRequest(MyEntityModel model) : base(model)
        {
        }
    }
}
