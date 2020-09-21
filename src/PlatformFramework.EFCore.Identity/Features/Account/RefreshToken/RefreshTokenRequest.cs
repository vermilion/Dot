using PlatformFramework.Eventing;

namespace PlatformFramework.EFCore.Identity.Features.Account
{
    public class RefreshTokenRequest : IRequest<RefreshTokenResponse>
    {
        public RefreshTokenRequest(string refreshToken)
        {
            RefreshToken = refreshToken;
        }

        public string RefreshToken { get; set; }
    }
}
