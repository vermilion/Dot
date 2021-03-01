using PlatformFramework.Eventing;
using PlatformFramework.Results;

namespace PlatformFramework.EFCore.Identity.Features.Account
{
    public class LoginRequest : IRequest<Result<LoginResponse>>
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
