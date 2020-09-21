using PlatformFramework.EFCore.Identity.Models;

namespace PlatformFramework.EFCore.Identity.Features.Account
{
    public class RevokeTokenResponse
    {
        public class Success : RevokeTokenResponse
        {
        }

        public class Unauthorized : RevokeTokenResponse
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
