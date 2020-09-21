using PlatformFramework.Eventing;

namespace PlatformFramework.EFCore.Identity.Features.Account
{
    public class RevokeTokenRequest : IRequest<RevokeTokenResponse>
    {
        public string Token { get; set; }
    }
}
