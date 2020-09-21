using PlatformFramework.EFCore.Identity.Models;

namespace PlatformFramework.EFCore.Identity.Features.Account
{
    public class RefreshTokenResponse
    {
        public class Success : RefreshTokenResponse
        {
            public Success(TokenResponse model, string refreshToken)
            {
                Model = model;
                RefreshToken = refreshToken;
            }

            public TokenResponse Model { get; }

            public string RefreshToken { get; }
        }

        public class Unauthorized : RefreshTokenResponse
        {
            public Unauthorized()
            {
            }

            public Unauthorized(string model)
            {
                Model = model;
            }

            public string? Model { get; }
        }
    }
}
