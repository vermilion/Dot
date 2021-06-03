using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Cofoundry.Domain.Data;
using Cofoundry.Domain.CQS;
using Microsoft.EntityFrameworkCore;
using Cofoundry.Core.Data;
using Cofoundry.Core.Mail;
using Cofoundry.Core;
using PlatformFramework.EFCore.Abstractions;

namespace Cofoundry.Domain.Internal
{
    public class CompleteUserPasswordResetCommandHandler 
        : IRequestHandler<CompleteUserPasswordResetCommand, Unit>
    {
        private const int NUMHOURS_PASSWORD_RESET_VALID = 16;
        private readonly IUnitOfWork _unitOfWork;

        #region construstor

        private readonly IMediator _queryExecutor;
        private readonly IMailService _mailService;
        private readonly IPasswordUpdateCommandHelper _passwordUpdateCommandHelper;

        public CompleteUserPasswordResetCommandHandler(
            IUnitOfWork unitOfWork,
            IMediator queryExecutor,
            IMailService mailService,
            IPasswordUpdateCommandHelper passwordUpdateCommandHelper
            )
        {
            _unitOfWork = unitOfWork;
            _queryExecutor = queryExecutor;
            _mailService = mailService;
            _passwordUpdateCommandHelper = passwordUpdateCommandHelper;
        }

        #endregion

        #region execution

        public async Task<Unit> ExecuteAsync(CompleteUserPasswordResetCommand command, IExecutionContext executionContext)
        {
            var validationResult = await _queryExecutor.ExecuteAsync(CreateValidationQuery(command), executionContext);
            ValidatePasswordRequest(validationResult);

            var request = await QueryPasswordRequestIfToken(command).SingleOrDefaultAsync();
            EntityNotFoundException.ThrowIfNull(request, command.UserPasswordResetRequestId);

            UpdatePasswordAndSetComplete(request, command, executionContext);
            SetMailTemplate(command, request.User);

            using (var scope = _unitOfWork.BeginTransaction())
            {
                await _unitOfWork.SaveChangesAsync();
                await _mailService.SendAsync(request.User.Email, request.User.GetFullName(), command.MailTemplate);

                await scope.CompleteAsync();
            }

            return Unit.Value;
        }

        #endregion

        #region private helpers

        private void UpdatePasswordAndSetComplete(UserPasswordResetRequest request, CompleteUserPasswordResetCommand command, IExecutionContext executionContext)
        {
            _passwordUpdateCommandHelper.UpdatePassword(command.NewPassword, request.User, executionContext);

            request.IsComplete = true;
        }

        private IQueryable<UserPasswordResetRequest> QueryPasswordRequestIfToken(CompleteUserPasswordResetCommand command)
        {
            return _unitOfWork
                .UserPasswordResetRequests()
                .Include(r => r.User)
                .Where(r => r.UserPasswordResetRequestId == command.UserPasswordResetRequestId
                    && !r.User.IsSystemAccount
                    && !r.User.IsDeleted);
        }

        private ValidatePasswordResetRequestQuery CreateValidationQuery(CompleteUserPasswordResetCommand command)
        {
            var query = new ValidatePasswordResetRequestQuery
            {
                UserPasswordResetRequestId = command.UserPasswordResetRequestId,
                Token = command.Token
            };

            return query;
        }

        private void ValidatePasswordRequest(PasswordResetRequestAuthenticationResult result)
        {
            if (!result.IsValid)
            {
                throw new ValidationException(result.ValidationErrorMessage);
            }
        }

        private bool IsPasswordRecoveryDateValid(DateTime dt, IExecutionContext executionContext)
        {
            return dt > executionContext.ExecutionDate.AddHours(-NUMHOURS_PASSWORD_RESET_VALID);
        }

        private void SetMailTemplate(CompleteUserPasswordResetCommand command, User user)
        {
            command.MailTemplate.FirstName = user.FirstName;
            command.MailTemplate.LastName = user.LastName;
        }

        #endregion
    }
}
