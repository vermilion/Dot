using Cofoundry.Core.Mail;
using Cofoundry.Domain.CQS;
using Cofoundry.Domain.MailTemplates;
using Microsoft.AspNetCore.Html;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cofoundry.Domain.Internal
{
    /// <summary>
    /// Adds a user to the Cofoundry user area and sends a welcome notification.
    /// </summary>
    public class AddCofoundryUserCommandHandler
        : ICommandHandler<AddCofoundryUserCommand>
        , IPermissionRestrictedCommandHandler<AddCofoundryUserCommand>
    {
        #region constructor

        private readonly ICommandExecutor _commandExecutor;
        private readonly IPasswordCryptographyService _passwordCryptographyService;
        private readonly IPasswordGenerationService _passwordGenerationService;
        private readonly IMailService _mailService;
        private readonly IQueryExecutor _queryExecutor;

        public AddCofoundryUserCommandHandler(
            ICommandExecutor commandExecutor,
            IPasswordCryptographyService passwordCryptographyService,
            IPasswordGenerationService passwordGenerationService,
            IMailService mailService,
            IQueryExecutor queryExecutor
            )
        {
            _commandExecutor = commandExecutor;
            _passwordCryptographyService = passwordCryptographyService;
            _passwordGenerationService = passwordGenerationService;
            _mailService = mailService;
            _queryExecutor = queryExecutor;
        }

        #endregion

        #region execution

        public async Task ExecuteAsync(AddCofoundryUserCommand command, IExecutionContext executionContext)
        {
            var newUserCommand = MapCommand(command, executionContext);
            await _commandExecutor.ExecuteAsync(newUserCommand, executionContext);

            var siteSettingsQuery = new GetSettingsQuery<GeneralSiteSettings>();
            var siteSettings = await _queryExecutor.ExecuteAsync(siteSettingsQuery);
            var emailTemplate = MapEmailTemplate(newUserCommand, siteSettings);
            await _mailService.SendAsync(newUserCommand.Email, GetDisplayName(newUserCommand), emailTemplate);
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
