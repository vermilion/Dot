using System.Threading.Tasks;
using Cofoundry.Domain;
using Cofoundry.Domain.CQS;
using Cofoundry.Domain.MailTemplates;
using Cofoundry.Web.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cofoundry.Web.Admin
{
    public class AuthController : BaseApiController
    {
        #region Constructors

        private readonly IMediator _mediator;
        private readonly IUserContextService _userContextService;
        private readonly AuthenticationControllerHelper _authenticationHelper;
        private readonly IUserSessionService _userSessionService;
        private readonly AccountManagementControllerHelper _accountManagementControllerHelper;

        public AuthController(
            IMediator mediator,
            IUserSessionService userSessionService,
            IUserContextService userContextService,
            AuthenticationControllerHelper authenticationHelper,
            AccountManagementControllerHelper accountManagementControllerHelper
            )
        {
            _mediator = mediator;
            _authenticationHelper = authenticationHelper;
            _userSessionService = userSessionService;
            _userContextService = userContextService;
            _accountManagementControllerHelper = accountManagementControllerHelper;
        }

        #endregion

        #region routes

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel command)
        {
            var template = new ResetPasswordTemplate();
            var settings = await _mediator.ExecuteAsync(new GetSettingsQuery<GeneralSiteSettings>());
            template.ApplicationName = settings.ApplicationName;

            await _authenticationHelper.SendPasswordResetNotificationAsync(this, command, template);

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> ResetPassword(CompletePasswordResetViewModel vm)
        {
            var template = new PasswordChangedTemplate();
            var settings = await _mediator.ExecuteAsync(new GetSettingsQuery<GeneralSiteSettings>());
            template.ApplicationName = settings.ApplicationName;

            await _authenticationHelper.CompletePasswordResetAsync(this, vm, template);

            if (ModelState.IsValid)
            {
                return Ok();
            }

            return Forbid();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel vm)
        {
            var user = await _userContextService.GetCurrentContextAsync();
            if (user.UserId.HasValue)
            {
                if (!user.IsPasswordChangeRequired)
                {
                    return Ok();
                    //return await GetLoggedInDefaultRedirectActionAsync();
                }

                // The user shouldn't be logged in, but if so, log them out
                await SignOutAsync();
            }

            await _accountManagementControllerHelper.ChangePasswordAsync(this, vm);

            //ViewBag.ReturnUrl = returnUrl;

            return Ok();
        }

        /// <summary>
        /// Signs the user out of the application and ends the session.
        /// </summary>
        private async Task SignOutAsync()
        {
            await _userSessionService.LogUserOutAsync();
            _userContextService.ClearCache();
        }

        #endregion
    }
}