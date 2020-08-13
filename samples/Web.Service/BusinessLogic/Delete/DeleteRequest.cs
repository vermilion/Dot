using PlatformFramework.EFCore.Eventing.Requests;

namespace Web.Service.BusinessLogic
{
    public class DeleteRequest : EntityDeleteRequest<MyEntityModel>
    {
        public DeleteRequest(long id) : base(id)
        {
        }
    }
}
