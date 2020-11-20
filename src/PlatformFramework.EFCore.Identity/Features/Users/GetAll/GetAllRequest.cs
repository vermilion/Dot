using PlatformFramework.EFCore.Eventing.Requests;
using PlatformFramework.EFCore.Identity.Models;
using PlatformFramework.Models;

namespace PlatformFramework.EFCore.Identity.Features.Users.GetAll
{
    public class GetAllRequest : EntityPagedSelectRequest<UserListModel>
    {
        public GetAllRequest(PagedModel model)
            : base(model.Limit, model.Offset)
        {
        }
    }
}
