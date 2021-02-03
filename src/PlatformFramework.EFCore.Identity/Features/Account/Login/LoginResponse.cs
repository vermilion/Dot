using PlatformFramework.EFCore.Identity.Models;

namespace PlatformFramework.EFCore.Identity.Features.Account
{
    public class LoginResponse
    {
        public class Success : LoginResponse
        {
            public Success(TokenResponse model, string refreshToken)
            {
                Model = model;
                RefreshToken = refreshToken;
            }

            public TokenResponse Model { get; }

            public string RefreshToken { get; }
        }

        public class NotFound : LoginResponse
        {
        }

        public class BadRequest : LoginResponse
        {
            public BadRequest(LoginRequest model)
            {
                Model = model;
            }

            public LoginRequest Model { get; }
        }
    }
}
