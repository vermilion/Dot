using System.Threading;
using System.Threading.Tasks;
using Cofoundry.Domain;
using FastEndpoints;

namespace Cofoundry.Web.Admin
{
    public class LogoutEndpoint : EndpointWithoutRequest
    {
        public IUserContextService UserContextService { get; set; }
        public IUserSessionService UserSessionService { get; set; }

        public override void Configure()
        {
            Post("/api/Auth/Logout");
        }

        public override async Task HandleAsync(CancellationToken ct)
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