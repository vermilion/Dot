using System;
using System.Threading.Tasks;
using Cofoundry.Core;
using Cofoundry.Core.Mail;
using Cofoundry.Domain;
using Cofoundry.Domain.CQS;
using Cofoundry.Domain.MailTemplates;
using Microsoft.AspNetCore.Mvc;

namespace Cofoundry.Web.Identity
{
    /// <summary>
    /// A helper class with shared functionality between controllers
    /// that manage the currently logged in users account.
    /// </summary>
    public class AccountManagementControllerHelper
    {
        #region constructor

        private readonly IMediator _mediator;
        private readonly IUserContextService _userContextService;
        private readonly IMailService _mailService;

        public AccountManagementControllerHelper(
            IMediator mediator,
            IMailService mailService,
            IUserContextService userContextService
            )
        {
            _mediator = mediator;
            _userContextService = userContextService;
            _mailService = mailService;
        }

        #endregion

        #region change password

        public async Task InitViewModelAsync(ChangePasswordViewModel vm)
        {
            var cx = await _userContextService.GetCurrentContextAsync();
            vm.IsPasswordChangeRequired = cx.IsPasswordChangeRequired;
        }

        /// <summary>
        /// Changes a users password, sending them an email notification if the operation 
        /// was successful.
        /// </summary>
        /// <param name="controller">Controller instance</param>
        /// <param name="vm">The IChangePasswordTemplate containing the data entered by the user.</param>
        /// <param name="notificationTemplate">An IPasswordChangedNotificationTemplate to use when sending the notification</param>
        public async Task ChangePasswordAsync<TNotificationTemplate>(
            ControllerBase controller,
            ChangePasswordViewModel vm,
            TNotificationTemplate notificationTemplate
            )
            where TNotificationTemplate : IPasswordChangedTemplate
        {
            var userId = await ChangePasswordAsync(controller, vm);

            if (controller.ModelState.IsValid)
            {
                if (!userId.HasValue)
                {
                    throw new Exception("UpdateUnauthenticatedUserPasswordCommand: OutputUserId not set");
                }

                var user = await _mediator.ExecuteAsync(new GetUserMicroSummaryByIdQuery(userId.Value));
                EntityNotFoundException.ThrowIfNull(user, userId.Value);

                // In some configuratons, an email isn't always required, only a username
                if (string.IsNullOrWhiteSpace(user.Email)) return;

                // Send notification
                if (notificationTemplate != null)
                {
                    notificationTemplate.FirstName = user.FirstName;
                    notificationTemplate.LastName = user.LastName;

                    await _mailService.SendAsync(user.Email, user.GetFullName(), notificationTemplate);
                }
            }
        }

        /// <summary>
        /// Changes a users password, without sending them an email notification
        /// </summary>
        /// <param name="controller">Controller instance</param>
        /// <param name="vm">The IChangePasswordTemplate containing the data entered by the user.</param>
        /// <returns>The user id of the updated user if the action was successful; otheriwse null.</returns>
        public async Task<int?> ChangePasswordAsync(ControllerBase controller, ChangePasswordViewModel vm)
        {
            if (controller == null) throw new ArgumentNullException(nameof(controller));
            if (vm == null) throw new ArgumentNullException(nameof(vm));

            if (controller.ModelState.IsValid)
            {
                var command = new UpdateUnauthenticatedUserPasswordCommand
                {
                    Username = vm.Username,
                    NewPassword = vm.NewPassword,
                    OldPassword = vm.OldPassword
                };

                var result = await _mediator.ExecuteAsync(command);

                return result.UserId;
            }

            return null;
        }

        #endregion
    }
}