using System;
using System.Linq;
using Cofoundry.Domain.CQS;
using Cofoundry.Domain;
using Cofoundry.Domain.MailTemplates;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cofoundry.Web.Identity
{
    /// <summary>
    /// A helper class with shared functionality between controllers
    /// that manage user login.
    /// </summary>
    public class AuthenticationControllerHelper
    {
        #region constructor

        private readonly IQueryExecutor _queryExecutor;
        private readonly ICommandExecutor _commandExecutor;
        private readonly ILoginService _loginService;
        private readonly IUserContextService _userContextService;

        public AuthenticationControllerHelper(
            IQueryExecutor queryExecutor,
            ICommandExecutor commandExecutor,
            ILoginService loginService,
            IUserContextService userContextService
            )
        {
            _queryExecutor = queryExecutor;
            _commandExecutor = commandExecutor;
            _loginService = loginService;
            _userContextService = userContextService;
        }

        #endregion

        #region Log in

        public async Task<AuthenticationResult> LogUserInAsync(ControllerBase controller, LoginViewModel vm)
        {
            if (controller == null) throw new ArgumentNullException(nameof(controller));
            if (vm == null) throw new ArgumentNullException(nameof(vm));

            var result = new AuthenticationResult();

            if (!controller.ModelState.IsValid) return result;

            var command = new LogUserInWithCredentialsCommand()
            {
                Username = vm.Username,
                Password = vm.Password,
                RememberUser = vm.RememberMe
            };

            try
            {
                await _commandExecutor.ExecuteAsync(command);
            }
            catch (PasswordChangeRequiredException ex)
            {
                result.RequiresPasswordChange = true;
                // Add modelstate error as a precaution, because
                // result.RequiresPasswordChange may not be handled by the caller
                controller.ModelState.AddModelError(string.Empty, "Password change required.");
            }

            result.IsAuthenticated = controller.ModelState.IsValid;

            return result;
        }

        #endregion

        #region log out

        public Task LogoutAsync()
        {
            return _loginService.SignOutAsync();
        }

        #endregion

        #region forgot password

        public Task SendPasswordResetNotificationAsync<TNotificationViewModel>(ControllerBase controller, ForgotPasswordViewModel vm, TNotificationViewModel notificationTemplate) 
            where TNotificationViewModel : IResetPasswordTemplate
        {
            if (!controller.ModelState.IsValid) return Task.CompletedTask;

            var command = new ResetUserPasswordByUsernameCommand
            {
                Username = vm.Username,
                MailTemplate = notificationTemplate
            };

            return _commandExecutor.ExecuteAsync(command);
        }

        public async Task<PasswordResetRequestAuthenticationResult> IsPasswordRequestValidAsync(ControllerBase controller, string requestId, string token)
        {
            var result = new PasswordResetRequestAuthenticationResult
            {
                ValidationErrorMessage = "Invalid password reset request"
            };

            if (!controller.ModelState.IsValid) return result;

            if (string.IsNullOrWhiteSpace(requestId) || string.IsNullOrWhiteSpace(token))
            {
                AddPasswordRequestInvalidError(controller);
                return result;
            }

            if (!Guid.TryParse(requestId, out Guid requestGuid))
            {
                AddPasswordRequestInvalidError(controller);
                return result;
            }

            var query = new ValidatePasswordResetRequestQuery
            {
                UserPasswordResetRequestId = requestGuid,
                Token = Uri.UnescapeDataString(token),
            };

            result = await _queryExecutor.ExecuteAsync(query);

            return result;
        }

        public Task CompletePasswordResetAsync<TNotificationTemplate>(ControllerBase controller, CompletePasswordResetViewModel vm, TNotificationTemplate notificationTemplate) 
            where TNotificationTemplate : IPasswordChangedTemplate
        {
            if (!controller.ModelState.IsValid) return Task.CompletedTask;
            
            var command = new CompleteUserPasswordResetCommand();

            if (!Guid.TryParse(vm.UserPasswordResetRequestId, out Guid requestGuid))
            {
                AddPasswordRequestInvalidError(controller);
                return Task.CompletedTask;
            }

            command.NewPassword = vm.NewPassword;
            command.Token = vm.Token;
            command.MailTemplate = notificationTemplate;
            command.UserPasswordResetRequestId = requestGuid;

            return _commandExecutor.ExecuteAsync(command);
        }

        private static void AddPasswordRequestInvalidError(ControllerBase controller)
        {
            controller.ModelState.AddModelError(string.Empty, "Invalid password request");
        }

        #endregion
    }
}