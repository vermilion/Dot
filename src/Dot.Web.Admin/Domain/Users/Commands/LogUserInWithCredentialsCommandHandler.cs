using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cofoundry.Domain.CQS;
using System.ComponentModel.DataAnnotations;
using Cofoundry.Core.Validation;

namespace Cofoundry.Domain.Internal
{
    /// <summary>
    /// Logs a user into the application for a specified user area
    /// using username and password credentials. Checks for valid
    /// credentials and includes additional security checking such
    /// as preventing excessive login attempts. Validation errors
    /// are thrown as ValidationExceptions.
    /// </summary>
    public class LogUserInWithCredentialsCommandHandler
        : IRequestHandler<LogUserInWithCredentialsCommand, Unit>
    {
        private readonly IMediator _mediator;
        private readonly IUserSessionService _userSessionService;
        private readonly IUserContextService _userContextService;

        public LogUserInWithCredentialsCommandHandler(
            IMediator queryExecutor,
            IUserSessionService userSessionService,
            IUserContextService userContextService
            )
        {
            _mediator = queryExecutor;
            _userSessionService = userSessionService;
            _userContextService = userContextService;
        }

        public async Task<Unit> ExecuteAsync(LogUserInWithCredentialsCommand command, IExecutionContext executionContext)
        {
            if (IsLoggedInAlready(command, executionContext)) return Unit.Value;

            var hasExceededMaxLoginAttempts = await _mediator.ExecuteAsync(GetMaxLoginAttemptsQuery(command), executionContext);
            ValidateMaxLoginAttemptsNotExceeded(hasExceededMaxLoginAttempts);

            var user = await GetUserLoginInfoAsync(command, executionContext);

            if (user == null)
            {
                var cmd = new LogFailedLoginAttemptCommand(command.Username);
                await _mediator.ExecuteAsync(cmd);

                throw ValidationErrorException.CreateWithProperties("Invalid username or password", nameof(command.Password));
            }

            if (user.RequirePasswordChange)
            {
                throw new PasswordChangeRequiredException();
            }

            await LogAuthenticatedUserInAsync(user.UserId, command.RememberUser);

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
            await _userSessionService.LogUserInAsync(userId, rememberUser);

            // Update the user record
            var command = new LogSuccessfulLoginCommand() { UserId = userId };
            await _mediator.ExecuteAsync(command);
        }

        /// <summary>
        /// Signs the user out of the application and ends the session.
        /// </summary>
        private async Task SignOutAsync()
        {
            await _userSessionService.LogUserOutAsync();
            _userContextService.ClearCache();
        }

        private static bool IsLoggedInAlready(LogUserInWithCredentialsCommand command, IExecutionContext executionContext)
        {
            var currentContext = executionContext.UserContext;

            return currentContext.UserId.HasValue;
        }

        private static HasExceededMaxLoginAttemptsQuery GetMaxLoginAttemptsQuery(LogUserInWithCredentialsCommand command)
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

        private Task<UserLoginInfo> GetUserLoginInfoAsync(LogUserInWithCredentialsCommand command, IExecutionContext executionContext)
        {
            var query = new GetUserLoginInfoIfAuthenticatedQuery()
            {
                Username = command.Username,
                Password = command.Password,
            };

            return _mediator.ExecuteAsync(query, executionContext);
        }
    }
}
