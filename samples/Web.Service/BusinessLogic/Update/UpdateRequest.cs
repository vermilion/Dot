using PlatformFramework.EFCore.Eventing.Requests;

namespace Web.Service.BusinessLogic
{
    public class UpdateRequest : EntityUpdateRequest<MyEntityModel>
    {
        public UpdateRequest(long id, MyEntityModel model) : base(id, model)
        {
        }
    }
}
