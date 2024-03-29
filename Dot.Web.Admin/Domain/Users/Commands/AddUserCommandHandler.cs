﻿using Cofoundry.Core.Mail;
using Cofoundry.Core.Validation;
using Cofoundry.Domain.CQS;
using Cofoundry.Domain.Data;
using Dot.EFCore.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cofoundry.Domain.Internal
{
    /// <summary>
    /// A generic user creation command for use with Cofoundry users and
    /// other non-Cofoundry users. Does not send any email notifications.
    /// </summary>
    public class AddUserCommandHandler
        : IRequestHandler<AddUserCommand, AddUserCommandResult>
        , IPermissionRestrictedRequestHandler<AddUserCommand>
    {
        #region constructor

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _queryExecutor;
        private readonly IPasswordCryptographyService _passwordCryptographyService;
        private readonly IMailService _mailService;
        private readonly UserCommandPermissionsHelper _userCommandPermissionsHelper;
        private readonly IPermissionValidationService _permissionValidationService;

        public AddUserCommandHandler(
            IUnitOfWork unitOfWork,
            IMediator queryExecutor,
            IPasswordCryptographyService passwordCryptographyService,
            IMailService mailService,
            UserCommandPermissionsHelper userCommandPermissionsHelper,
            IPermissionValidationService permissionValidationService
            )
        {
            _unitOfWork = unitOfWork;
            _queryExecutor = queryExecutor;
            _passwordCryptographyService = passwordCryptographyService;
            _mailService = mailService;
            _userCommandPermissionsHelper = userCommandPermissionsHelper;
            _permissionValidationService = permissionValidationService;
        }

        #endregion

        #region execution

        public async Task<AddUserCommandResult> ExecuteAsync(AddUserCommand command, IExecutionContext executionContext)
        {
            ValidateCommand(command);
            var isUnique = await _queryExecutor.ExecuteAsync(GetUniqueQuery(command), executionContext);
            ValidateIsUnique(isUnique);

            var newRole = await _userCommandPermissionsHelper.GetAndValidateNewRoleAsync(command.RoleId, null, executionContext);

            var user = MapAndAddUser(command, executionContext, newRole);
            await _unitOfWork.SaveChangesAsync();

            return new AddUserCommandResult { UserId = user.UserId };
        }

        #endregion

        #region helpers

        /// <summary>
        /// Perform some additional command validation that we can't do using data 
        /// annotations.
        /// </summary>
        private void ValidateCommand(AddUserCommand command)
        {
            // Password
            var isPasswordEmpty = string.IsNullOrWhiteSpace(command.Password);

            if (isPasswordEmpty)
            {
                throw ValidationErrorException.CreateWithProperties("Password field is required", "Password");
            }

            // Username
            if (string.IsNullOrWhiteSpace(command.Username))
            {
                throw ValidationErrorException.CreateWithProperties("Username field is required", "Username");
            }
        }

        private User MapAndAddUser(AddUserCommand command, IExecutionContext executionContext, Role role)
        {
            var hashResult = _passwordCryptographyService.CreateHash(command.Password);

            var user = new User
            {
                FirstName = command.FirstName?.Trim(),
                LastName = command.LastName?.Trim(),
                Email = command.Email?.Trim(),
                RequirePasswordChange = command.RequirePasswordChange,
                LastPasswordChangeDate = executionContext.ExecutionDate,
                CreateDate = executionContext.ExecutionDate,
                Role = role,
                Username = command.Username?.Trim(),
                Password = hashResult.Hash
            };

            _unitOfWork.Users().Add(user);

            return user;
        }

        private void ValidateIsUnique(bool isUnique)
        {
            if (!isUnique)
            {
                throw ValidationErrorException.CreateWithProperties("This username is already registered", "Username");
            }
        }

        private IsUsernameUniqueQuery GetUniqueQuery(AddUserCommand command)
        {
            var query = new IsUsernameUniqueQuery
            {
                Username = command.Username?.Trim()
            };

            return query;
        }

        #endregion

        #region Permission

        public IEnumerable<IPermissionApplication> GetPermissions(AddUserCommand command)
        {
            yield return new CofoundryUserCreatePermission();
        }

        #endregion
    }
}
