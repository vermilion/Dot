using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Cofoundry.Domain.Data;
using Cofoundry.Domain.CQS;
using Cofoundry.Core.Validation;
using Microsoft.EntityFrameworkCore;
using Cofoundry.Core.Mail;
using PlatformFramework.EFCore.Abstractions;

namespace Cofoundry.Domain
{
    /// <summary>
    /// Helper used by ResetUserPasswordByUserIdCommandHandler and ResetUserPasswordByUsernameCommandHandler
    /// for shared functionality.
    /// </summary>
    public class ResetUserPasswordCommandHelper : IResetUserPasswordCommandHelper
    {
        private const int MAX_PASSWORD_RESET_ATTEMPTS = 16;
        private const int MAX_PASSWORD_RESET_ATTEMPTS_NUMHOURS = 24;
        private readonly IUnitOfWork _unitOfWork;

        #region construstor

        private readonly IPasswordCryptographyService _passwordCryptographyService;
        private readonly ISecurityTokenGenerationService _securityTokenGenerationService;
        private readonly IMailService _mailService;
        private readonly IClientConnectionService _clientConnectionService;

        public ResetUserPasswordCommandHelper(
            IUnitOfWork unitOfWork,
            IPasswordCryptographyService passwordCryptographyService,
            ISecurityTokenGenerationService securityTokenGenerationService,
            IMailService mailService,
            IClientConnectionService clientConnectionService
            )
        {
            _unitOfWork = unitOfWork;
            _passwordCryptographyService = passwordCryptographyService;
            _securityTokenGenerationService = securityTokenGenerationService;
            _mailService = mailService;
            _clientConnectionService = clientConnectionService;
        }

        #endregion

        #region public methods

        public async Task ValidateCommandAsync(IResetUserPasswordCommand command, IExecutionContext executionContext)
        {
            var numResetAttempts = await QueryNumberResetAttempts(executionContext).CountAsync();
            ValidateNumberOfResetAttempts(numResetAttempts);
        }

        public async Task ResetPasswordAsync(User user, IResetUserPasswordCommand command, IExecutionContext executionContext)
        {
            ValidateUserAccountExists(user);

            var existingIncompleteRequests = await QueryIncompleteRequests(user).ToListAsync();
            SetExistingRequestsComplete(existingIncompleteRequests);
            var request = CreateRequest(executionContext, user);
            SetMailTemplate(command, user, request);

            using (var scope = _unitOfWork.BeginTransaction())
            {
                await _unitOfWork.SaveChangesAsync();
                await _mailService.SendAsync(user.Email, user.GetFullName(), command.MailTemplate);

                await scope.CompleteAsync();
            }
        }

        #endregion

        #region private helpers

        private IQueryable<UserPasswordResetRequest> QueryNumberResetAttempts(IExecutionContext executionContext)
        {
            var connectionInfo = _clientConnectionService.GetConnectionInfo();

            var dateToDetectAttempts = executionContext.ExecutionDate.AddHours(-MAX_PASSWORD_RESET_ATTEMPTS_NUMHOURS);
            return _unitOfWork.UserPasswordResetRequests().Where(r => r.IPAddress == connectionInfo.IPAddress && r.CreateDate > dateToDetectAttempts);
        }

        private static void ValidateNumberOfResetAttempts(int numResetAttempts)
        {
            if (numResetAttempts > MAX_PASSWORD_RESET_ATTEMPTS)
            {
                throw new ValidationException("Maximum password reset attempts reached.");
            }
        }

        private static void ValidateUserAccountExists(User user)
        {
            if (user == null)
            {
                throw ValidationErrorException.CreateWithProperties("Account not found.", "Username");
            }
        }

        private IQueryable<UserPasswordResetRequest> QueryIncompleteRequests(User user)
        {
            return _unitOfWork
                .UserPasswordResetRequests()
                .Where(r => r.UserId == user.UserId && !r.IsComplete);
        }

        private void SetExistingRequestsComplete(List<UserPasswordResetRequest> existingIncompleteRequests)
        {
            foreach (var req in existingIncompleteRequests)
            {
                req.IsComplete = true;
            }
        }

        private UserPasswordResetRequest CreateRequest(IExecutionContext executionContext, User user)
        {
            var connectionInfo = _clientConnectionService.GetConnectionInfo();

            var request = new UserPasswordResetRequest
            {
                User = user,
                UserPasswordResetRequestId = Guid.NewGuid(),
                CreateDate = executionContext.ExecutionDate,
                IPAddress = connectionInfo.IPAddress,
                Token = _securityTokenGenerationService.Generate()
            };

            _unitOfWork.UserPasswordResetRequests().Add(request);

            return request;
        }

        private void SetMailTemplate(IResetUserPasswordCommand command, User user, UserPasswordResetRequest request)
        {
            command.MailTemplate.FirstName = user.FirstName;
            command.MailTemplate.LastName = user.LastName;
            command.MailTemplate.UserPasswordResetRequestId = request.UserPasswordResetRequestId;
            command.MailTemplate.Token = request.Token;
        }

        #endregion
    }
}
