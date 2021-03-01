using PlatformFramework.EFCore.Identity.Models;

namespace PlatformFramework.EFCore.Identity.Features.Account
{
    public class LoginResponse
    {
        public LoginResponse(TokenResponse model, string refreshToken)
        {
            Model = model;
            RefreshToken = refreshToken;
        }

        public TokenResponse Model { get; }

        public string RefreshToken { get; }
    }
}
