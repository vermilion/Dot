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
        private readonly AccountManagementControllerHelper _accountManagementControllerHelper;

        public AuthController(
            IMediator mediator,
            IUserContextService userContextService,
            AuthenticationControllerHelper authenticationHelper,
            AccountManagementControllerHelper accountManagementControllerHelper
            )
        {
            _mediator = mediator;
            _authenticationHelper = authenticationHelper;
            _userContextService = userContextService;
            _accountManagementControllerHelper = accountManagementControllerHelper;
        }

        #endregion

        #region routes

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel command)
        {
            var result = await _authenticationHelper.LogUserInAsync(this, command);

            if (result.RequiresPasswordChange)
            {
                return Unauthorized();
                //return Redirect(_adminRouteLibrary.Auth.ChangePassword(returnUrl));
            }
            else if (result.IsAuthenticated)
            {
                //var context = await _userContextService.GetCurrentContextAsync();
                //var user = await _queryExecutor.ExecuteAsync(new GetUserMicroSummaryByIdQuery(context.UserId.Value));
                return Ok();
            }
            else if (result.IsAuthenticated)
            {
                _userContextService.ClearCache();
                return Ok();
                //return await GetLoggedInDefaultRedirectActionAsync();
            }

            return Ok();
            //var viewPath = ViewPathFormatter.View(CONTROLLER_NAME, nameof(Login));
            //return View(viewPath, command);
        }

        [HttpPost]
        public async Task<ActionResult> Logout()
        {
            await _authenticationHelper.LogoutAsync();
            return Ok();
            //return Redirect(_adminRouteLibrary.Auth.Login(Request.Query["ReturnUrl"].FirstOrDefault()));
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel command)
        {
            var template = new ResetPasswordTemplate();
            var settings = await _mediator.ExecuteAsync(new GetSettingsQuery<GeneralSiteSettings>());
            template.ApplicationName = settings.ApplicationName;

            await _authenticationHelper.SendPasswordResetNotificationAsync(this, command, template);

            return Ok();
            //var viewPath = ViewPathFormatter.View(CONTROLLER_NAME, nameof(ForgotPassword));
            //return View(viewPath, command);
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
                //var completeViewPath = ViewPathFormatter.View(CONTROLLER_NAME, nameof(ResetPassword) + "Complete");
                //return View(completeViewPath);
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
                await _authenticationHelper.LogoutAsync();
            }

            await _accountManagementControllerHelper.ChangePasswordAsync(this, vm);

            //ViewBag.ReturnUrl = returnUrl;

            return Ok();
            //var viewPath = ViewPathFormatter.View(CONTROLLER_NAME, nameof(ChangePassword));
            //return View(viewPath, vm);
        }

        #endregion
    }
}