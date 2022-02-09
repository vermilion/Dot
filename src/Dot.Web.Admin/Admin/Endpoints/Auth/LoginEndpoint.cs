using System.Threading;
using System.Threading.Tasks;
using Cofoundry.Domain;
using Cofoundry.Domain.CQS;
using Cofoundry.Web.Identity;
using FastEndpoints;

namespace Cofoundry.Web.Admin
{
    public class LoginEndpoint : Endpoint<LoginViewModel>
    {
        public IUserContextService UserContextService { get; set; }
        public IMediator Mediator { get; set; }

        public override void Configure()
        {
            Post("/api/auth/login");
            AllowAnonymous();
        }

        public override async Task HandleAsync(LoginViewModel req, CancellationToken ct)
        {
            var result = await LogUserInAsync(req, ct);

            if (result.RequiresPasswordChange)
            {
                await SendUnauthorizedAsync(ct);
                return;
                //return Redirect(_adminRouteLibrary.Auth.ChangePassword(returnUrl));
            }
            else if (result.IsAuthenticated)
            {
                //var context = await _userContextService.GetCurrentContextAsync();
                //var user = await _queryExecutor.ExecuteAsync(new GetUserMicroSummaryByIdQuery(context.UserId.Value));
                await SendOkAsync(ct);
                return;
            }
            else if (result.IsAuthenticated)
            {
                UserContextService.ClearCache();
                await SendOkAsync(ct);
                return;
                //return await GetLoggedInDefaultRedirectActionAsync();
            }

            await SendOkAsync(ct);
        }

        private async Task<AuthenticationResult> LogUserInAsync(LoginViewModel vm, CancellationToken ct)
        {
            var result = new AuthenticationResult();

            var command = new LogUserInWithCredentialsCommand
            {
                Username = vm.Username,
                Password = vm.Password,
                RememberUser = vm.RememberMe
            };

            try
            {
                await Mediator.ExecuteAsync(command);
                result.IsAuthenticated = true;
            }
            catch (PasswordChangeRequiredException ex)
            {
                result.RequiresPasswordChange = true;
                // Add modelstate error as a precaution, because
                // result.RequiresPasswordChange may not be handled by the caller
                //controller.ModelState.AddModelError(string.Empty, "Password change required.");
            }

            return result;
        }
    }
}