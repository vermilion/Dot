using PlatformFramework.EFCore.Eventing.Requests;
using PlatformFramework.EFCore.Identity.Models;

namespace PlatformFramework.EFCore.Identity.Features.UserTokens.Create
{
    public class CreateRequest : EntityCreateRequest<UserTokenModel>
    {
        public CreateRequest(UserTokenModel model) : base(model)
        {
        }
    }
}
