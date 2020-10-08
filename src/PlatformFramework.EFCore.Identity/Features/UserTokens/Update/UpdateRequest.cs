using PlatformFramework.EFCore.Eventing.Requests;
using PlatformFramework.EFCore.Identity.Models;

namespace PlatformFramework.EFCore.Identity.Features.UserTokens.Update
{
    public class UpdateRequest : EntityUpdateRequest<UserTokenModel>
    {
        public UpdateRequest(int id, UserTokenModel model) : base(id, model)
        {
        }
    }
}
