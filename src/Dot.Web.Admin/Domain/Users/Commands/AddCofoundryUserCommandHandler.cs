using Cofoundry.Core.Mail;
using Cofoundry.Domain.CQS;
using Cofoundry.Domain.MailTemplates;
using Microsoft.AspNetCore.Html;

namespace Cofoundry.Domain.Internal
{
    /// <summary>
    /// Adds a user to the Cofoundry user area and sends a welcome notification.
    /// </summary>
    public class AddCofoundryUserCommandHandler
        : IRequestHandler<AddCofoundryUserCommand, AddCofoundryUserCommandResult>
        , IPermissionRestrictedRequestHandler<AddCofoundryUserCommand>
    {
        #region constructor

        private readonly IPasswordCryptographyService _passwordCryptographyService;
        private readonly IPasswordGenerationService _passwordGenerationService;
        private readonly IMailService _mailService;
        private readonly IMediator _mediator;

        public AddCofoundryUserCommandHandler(
            IPasswordCryptographyService passwordCryptographyService,
            IPasswordGenerationService passwordGenerationService,
            IMailService mailService,
            IMediator mediator
            )
        {
            _passwordCryptographyService = passwordCryptographyService;
            _passwordGenerationService = passwordGenerationService;
            _mailService = mailService;
            _mediator = mediator;
        }

        #endregion

        #region execution

        public async Task<AddCofoundryUserCommandResult> ExecuteAsync(AddCofoundryUserCommand command, IExecutionContext executionContext)
        {
            var newUserCommand = MapCommand(command, executionContext);
            var result = await _mediator.ExecuteAsync(newUserCommand, executionContext);

            var siteSettingsQuery = new GetSettingsQuery<GeneralSiteSettings>();
            var siteSettings = await _mediator.ExecuteAsync(siteSettingsQuery);
            var emailTemplate = MapEmailTemplate(newUserCommand, siteSettings);
            await _mailService.SendAsync(newUserCommand.Email, GetDisplayName(newUserCommand), emailTemplate);

            return new AddCofoundryUserCommandResult { UserId = result.UserId };
        }

        #endregion

        #region private helpers

        private AddUserCommand MapCommand(AddCofoundryUserCommand command, IExecutionContext executionContext)
        {
            var newUserCommand = new AddUserCommand
            {
                Username = command.Username,
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
                Password = _passwordGenerationService.Generate(),
                RequirePasswordChange = true,
                RoleId = command.RoleId
            };

            return newUserCommand;
        }

        private NewUserWelcomeMailTemplate MapEmailTemplate(AddUserCommand user, GeneralSiteSettings siteSettings)
        {
            var template = new NewUserWelcomeMailTemplate
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                TemporaryPassword = new HtmlString(user.Password),
                ApplicationName = siteSettings.ApplicationName
            };

            return template;
        }

        private string GetDisplayName(AddUserCommand command)
        {
            return (command.FirstName + " " + command.LastName).Trim();
        }

        #endregion

        #region Permission

        public IEnumerable<IPermissionApplication> GetPermissions(AddCofoundryUserCommand command)
        {
            yield return new CofoundryUserCreatePermission();
        }

        #endregion
    }
}
