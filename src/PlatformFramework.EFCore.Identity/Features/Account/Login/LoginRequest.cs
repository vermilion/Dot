using PlatformFramework.Eventing;

namespace PlatformFramework.EFCore.Identity.Features.Account
{
    public class LoginRequest : IRequest<LoginResponse>
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
