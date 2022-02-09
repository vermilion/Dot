using Cofoundry.Domain;
using Cofoundry.Domain.CQS;
using Cofoundry.Domain.MailTemplates;
using Microsoft.AspNetCore.Mvc;
using System;
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

        private readonly IMediator _mediator;

        public AuthenticationControllerHelper(
            IMediator mediator
            )
        {
            _mediator = mediator;
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

            return _mediator.ExecuteAsync(command);
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

            result = await _mediator.ExecuteAsync(query);

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

            return _mediator.ExecuteAsync(command);
        }

        private static void AddPasswordRequestInvalidError(ControllerBase controller)
        {
            controller.ModelState.AddModelError(string.Empty, "Invalid password request");
        }

        #endregion
    }
}