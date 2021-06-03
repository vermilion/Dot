using Cofoundry.Core.Validation;
using Cofoundry.Domain.CQS;
using System;
using System.Threading.Tasks;

namespace Cofoundry.Domain.Internal
{
    /// <summary>
    /// Updates the password of the currently logged in user, using the
    /// OldPassword field to authenticate the request.
    /// </summary>
    public class UpdateUnauthenticatedUserPasswordCommandHandler
        : IRequestHandler<UpdateUnauthenticatedUserPasswordCommand, UpdateUnauthenticatedUserPasswordCommandResult>
    {
        #region constructor

        private readonly IMediator _mediator;
        private readonly IExecutionContextFactory _executionContextFactory;

        public UpdateUnauthenticatedUserPasswordCommandHandler(
            IMediator mediator,
            IExecutionContextFactory executionContextFactory
            )
        {
            _mediator = mediator;
            _executionContextFactory = executionContextFactory;
        }

        #endregion

        #region execution
        
        public async Task<UpdateUnauthenticatedUserPasswordCommandResult> ExecuteAsync(UpdateUnauthenticatedUserPasswordCommand command, IExecutionContext executionContext)
        {
            if (IsLoggedInAlready(command, executionContext))
            {
                throw new Exception("UpdateUnauthenticatedUserPasswordCommand cannot be used when the user is already logged in.");
            }

            await ValidateMaxLoginAttemptsNotExceeded(command, executionContext);

            var userLoginInfo = await GetUserLoginInfoAsync(command, executionContext);

            if (userLoginInfo == null)
            {
                var failedLoginLogCommand = new LogFailedLoginAttemptCommand(command.Username);
                await _mediator.ExecuteAsync(failedLoginLogCommand);

                throw ValidationErrorException.CreateWithProperties("Invalid username or password", nameof(command.OldPassword));
            }

            var updatePasswordCommand = new UpdateUserPasswordByUserIdCommand()
            {
                UserId = userLoginInfo.UserId,
                NewPassword = command.NewPassword
            };

            // User is not logged in, so will need to elevate permissions here to change the password.
            var systemExecutionContext = await _executionContextFactory.CreateSystemUserExecutionContextAsync(executionContext);
            await _mediator.ExecuteAsync(updatePasswordCommand, systemExecutionContext);

            return new UpdateUnauthenticatedUserPasswordCommandResult { UserId = userLoginInfo.UserId };
        }

        private Task<UserLoginInfo> GetUserLoginInfoAsync(
            UpdateUnauthenticatedUserPasswordCommand command, 
            IExecutionContext executionContext
            )
        {
            var query = new GetUserLoginInfoIfAuthenticatedQuery()
            {
                Username = command.Username,
                Password = command.OldPassword,
            };

            return _mediator.ExecuteAsync(query, executionContext);
        }

        private static bool IsLoggedInAlready(UpdateUnauthenticatedUserPasswordCommand command, IExecutionContext executionContext)
        {
            var currentContext = executionContext.UserContext;

            return currentContext.UserId.HasValue;
        }

        private async Task ValidateMaxLoginAttemptsNotExceeded(UpdateUnauthenticatedUserPasswordCommand command, IExecutionContext executionContext)
        {
            var query = new HasExceededMaxLoginAttemptsQuery()
            {
                Username = command.Username
            };

            var hasAttemptsExceeded = await _mediator.ExecuteAsync(query, executionContext);
            if (hasAttemptsExceeded)
            {
                throw new TooManyFailedAttemptsAuthenticationException();
            }
        }

        #endregion
    }
}
