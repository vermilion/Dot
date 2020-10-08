using PlatformFramework.EFCore.Eventing.Requests;
using PlatformFramework.EFCore.Identity.Models;

namespace PlatformFramework.EFCore.Identity.Features.UserTokens.Get
{
    public class GetRequest : EntitySelectSingleRequest<UserTokenModel>
    {
        public GetRequest(int id) : base(id)
        {
        }
    }
}
