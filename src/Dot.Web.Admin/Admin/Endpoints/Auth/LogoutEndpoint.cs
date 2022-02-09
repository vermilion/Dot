using System.Threading;
using System.Threading.Tasks;
using Cofoundry.Domain;
using Cofoundry.Web.Identity;
using FastEndpoints;

namespace Cofoundry.Web.Admin
{
    public class LogoutEndpoint : Endpoint<LoginViewModel>
    {
        public IUserContextService UserContextService { get; set; }
        public IUserSessionService UserSessionService { get; set; }

        public override void Configure()
        {
            Post("/api/auth/logout");
        }

        public override async Task HandleAsync(LoginViewModel req, CancellationToken ct)
        {
            await SignOutAsync();

            await SendOkAsync(ct);
        }

        /// <summary>
        /// Signs the user out of the application and ends the session.
        /// </summary>
        private async Task SignOutAsync()
        {
            await UserSessionService.LogUserOutAsync();
            UserContextService.ClearCache();
        }
    }
}