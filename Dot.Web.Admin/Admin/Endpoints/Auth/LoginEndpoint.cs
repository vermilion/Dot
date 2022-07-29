using System.Threading;
using System.Threading.Tasks;
using Cofoundry.Core.Validation;
using Cofoundry.Domain;
using Cofoundry.Domain.CQS;
using Cofoundry.Web.Identity;
using FastEndpoints;

namespace Cofoundry.Web.Admin
{
    public class LoginEndpoint : Endpoint<LoginRequestModel>
    {
        public IUserContextService UserContextService { get; set; }
        public IUserSessionService UserSessionService { get; set; }
        public IMediator Mediator { get; set; }

        public override void Configure()
        {
            Post("/api/Auth/Login");
            AllowAnonymous();
        }

        public override async Task HandleAsync(LoginRequestModel req, CancellationToken ct)
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

        private async Task<AuthenticationResult> LogUserInAsync(LoginRequestModel req, CancellationToken ct)
        {
            var result = new AuthenticationResult();

            try
            {
                await ExecuteAsync(req);
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

        public async Task<Unit> ExecuteAsync(LoginRequestModel command)
        {
            if (await IsLoggedInAlready(command)) return Unit.Value;

            var hasExceededMaxLoginAttempts = await Mediator.ExecuteAsync(GetMaxLoginAttemptsQuery(command));
            ValidateMaxLoginAttemptsNotExceeded(hasExceededMaxLoginAttempts);

            var user = await GetUserLoginInfoAsync(command);

            if (user == null)
            {
                var cmd = new LogFailedLoginAttemptCommand(command.Username);
                await Mediator.ExecuteAsync(cmd);

                throw ValidationErrorException.CreateWithProperties("Invalid username or password", nameof(command.Password));
            }

            if (user.RequirePasswordChange)
            {
                throw new PasswordChangeRequiredException();
            }

            await LogAuthenticatedUserInAsync(user.UserId, command.RememberMe);

            return Unit.Value;
        }

        /// <summary>
        /// Logs a user into the application but performs no 
        /// authentication. The user should have already passed 
        /// authentication prior to calling this method.
        /// </summary>
        /// <param name="userId">The id of the user to log in.</param>
        /// <param name="rememberUser">
        /// True if the user should stay logged in perminantely; false
        /// if the user should only stay logged in for the duration of
        /// the session.
        /// </param>
        public async Task LogAuthenticatedUserInAsync(int userId, bool rememberUser)
        {
            // Clear any existing session
            await SignOutAsync();

            // Log in new session
            await UserSessionService.LogUserInAsync(userId, rememberUser);

            // Update the user record
            var command = new LogSuccessfulLoginCommand() { UserId = userId };
            await Mediator.ExecuteAsync(command);
        }

        /// <summary>
        /// Signs the user out of the application and ends the session.
        /// </summary>
        private async Task SignOutAsync()
        {
            await UserSessionService.LogUserOutAsync();
            UserContextService.ClearCache();
        }

        private async Task<bool> IsLoggedInAlready(LoginRequestModel command)
        {
            var currentContext = await UserContextService.GetCurrentContextAsync();

            return currentContext.UserId.HasValue;
        }

        private static HasExceededMaxLoginAttemptsQuery GetMaxLoginAttemptsQuery(LoginRequestModel command)
        {
            return new HasExceededMaxLoginAttemptsQuery()
            {
                Username = command.Username
            };
        }

        private static void ValidateMaxLoginAttemptsNotExceeded(bool hasAttemptsExceeded)
        {
            if (hasAttemptsExceeded)
            {
                throw new TooManyFailedAttemptsAuthenticationException();
            }
        }

        private Task<UserLoginInfo> GetUserLoginInfoAsync(LoginRequestModel command)
        {
            var query = new GetUserLoginInfoIfAuthenticatedQuery()
            {
                Username = command.Username,
                Password = command.Password,
            };

            return Mediator.ExecuteAsync(query);
        }
    }

    public class LoginRequestModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}