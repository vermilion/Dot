using PlatformFramework.EFCore.Eventing.Requests;
using PlatformFramework.EFCore.Identity.Models;

namespace PlatformFramework.EFCore.Identity.Features.UserLogins.Create
{
    public class CreateRequest : EntityCreateRequest<UserLoginModel>
    {
        public CreateRequest(UserLoginModel model) : base(model)
        {
        }
    }
}
