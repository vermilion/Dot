using System.Threading.Tasks;
using Cofoundry.Domain.CQS;

namespace Cofoundry.Domain.Internal
{
    /// <summary>
    /// Service for logging users in and out of the application.
    /// </summary>
    public class LoginService : ILoginService
    {
        #region Constructor
        
        private readonly IMediator _mediator;
        private readonly IUserSessionService _userSessionService;
        private readonly IUserContextService _userContextService;

        public LoginService(
            IMediator commandExecutor,
            IUserSessionService userSessionService,
            IUserContextService userContextService
            )
        {
            _mediator = commandExecutor;
            _userContextService = userContextService;
            _userSessionService = userSessionService;
        }

        #endregion

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
        public virtual async Task LogAuthenticatedUserInAsync(int userId, bool rememberUser)
        {
            // Clear any existing session
            await SignOutAsync();

            // Log in new session
            await _userSessionService.LogUserInAsync(userId, rememberUser);

            // Update the user record
            var command = new LogSuccessfulLoginCommand() { UserId = userId };
            await _mediator.ExecuteAsync(command);
        }

        /// <summary>
        /// Logs a failed login attempt. A history of logins is used
        /// to prevent brute force login attacks.
        /// </summary>
        /// <param name="userAreaCode">The code of the user area attempting to be logged into.</param>
        /// <param name="username">The username attempting to be logged in with.</param>
        public virtual async Task LogFailedLoginAttemptAsync(string username)
        {
            var command = new LogFailedLoginAttemptCommand(username);
            await _mediator.ExecuteAsync(command);
        }

        /// <summary>
        /// Signs the user out of the application and ends the session.
        /// </summary>
        /// <param name="userAreaCode">The code of the user area to log out of.</param>
        public virtual async Task SignOutAsync()
        {
            await _userSessionService.LogUserOutAsync();
            _userContextService.ClearCache();
        }
    }
}
