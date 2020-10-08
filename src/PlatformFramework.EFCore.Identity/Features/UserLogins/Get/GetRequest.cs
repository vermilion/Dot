using PlatformFramework.EFCore.Eventing.Requests;
using PlatformFramework.EFCore.Identity.Models;

namespace PlatformFramework.EFCore.Identity.Features.UserLogins.Get
{
    public class GetRequest : EntitySelectSingleRequest<UserLoginModel>
    {
        public GetRequest(int id) : base(id)
        {
        }
    }
}
