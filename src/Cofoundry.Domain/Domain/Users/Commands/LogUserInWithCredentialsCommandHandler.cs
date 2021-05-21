using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cofoundry.Domain.CQS;
using System.ComponentModel.DataAnnotations;

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
        : ICommandHandler<LogUserInWithCredentialsCommand>
    {
        #region constructor
        
        private readonly IQueryExecutor _queryExecutor;
        private readonly ILoginService _loginService;

        public LogUserInWithCredentialsCommandHandler(
            IQueryExecutor queryExecutor,
            ILoginService loginService
            )
        {
            _queryExecutor = queryExecutor;
            _loginService = loginService;
        }

        #endregion

        public async Task ExecuteAsync(LogUserInWithCredentialsCommand command, IExecutionContext executionContext)
        {
            if (IsLoggedInAlready(command, executionContext)) return;

            var hasExceededMaxLoginAttempts = await _queryExecutor.ExecuteAsync(GetMaxLoginAttemptsQuery(command), executionContext);
            ValidateMaxLoginAttemptsNotExceeded(hasExceededMaxLoginAttempts);

            var user = await GetUserLoginInfoAsync(command, executionContext);

            if (user == null)
            {
                await _loginService.LogFailedLoginAttemptAsync(command.Username);

                throw new InvalidCredentialsAuthenticationException(nameof(command.Password));
            }

            if (user.RequirePasswordChange)
            {
                throw new PasswordChangeRequiredException();
            }

            await _loginService.LogAuthenticatedUserInAsync(user.UserId, command.RememberUser);
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

            return _queryExecutor.ExecuteAsync(query, executionContext);
        }
    }
}
