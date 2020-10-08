using PlatformFramework.EFCore.Eventing.Requests;
using PlatformFramework.EFCore.Identity.Models;
using PlatformFramework.Models;

namespace PlatformFramework.EFCore.Identity.Features.UserLogins.GetAll
{
    public class GetAllRequest : EntityPagedSelectRequest<UserLoginModel>
    {
        public GetAllRequest(PagedModel model)
            : base(model.Limit, model.Offset)
        {
        }
    }
}
