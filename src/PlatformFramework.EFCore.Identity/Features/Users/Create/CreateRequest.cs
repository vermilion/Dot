using PlatformFramework.EFCore.Eventing.Requests;
using PlatformFramework.EFCore.Identity.Models;

namespace PlatformFramework.EFCore.Identity.Features.Users.Create
{
    public class CreateRequest : EntityCreateRequest<UserModel>
    {
        public CreateRequest(UserModel model) : base(model)
        {
        }
    }
}
