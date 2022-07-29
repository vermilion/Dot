using Cofoundry.Core;
using Cofoundry.Core.Validation;
using Cofoundry.Domain;
using Cofoundry.Domain.Data;
using Cofoundry.Domain.Internal;
using Dot.EFCore.UnitOfWork;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Cofoundry.Web.Admin
{
    public class UpdateCurrentUserPasswordEndpoint : Endpoint<UpdateCurrentUserPasswordModel>
    {
        public IUserContextService UserContextService { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }
        public UserAuthenticationHelper UserAuthenticationHelper { get; set; }
        public IPasswordCryptographyService PasswordCryptographyService { get; set; }

        public override void Configure()
        {
            Post("/api/Account/CurrentUser/UpdatePassword");
        }

        public override async Task HandleAsync(UpdateCurrentUserPasswordModel req, CancellationToken ct)
        {
            var userContext = await UserContextService.GetCurrentContextAsync();
            
            var user = await UnitOfWork.Users()
                .WithSpecification(new UserByIdSpec(userContext.UserId.Value))
                .SingleOrDefaultAsync(ct);

            EntityNotFoundException.ThrowIfNull(user, userContext.UserId);

            UpdatePassword(req, user);
            await UnitOfWork.SaveChangesAsync(ct);

            await SendOkAsync(ct);
        }

        private void UpdatePassword(UpdateCurrentUserPasswordModel command, User user)
        {
            if (!UserAuthenticationHelper.IsPasswordCorrect(user, command.OldPassword))
            {
                throw ValidationErrorException.CreateWithProperties("Incorrect password", "OldPassword");
            }

            user.RequirePasswordChange = false;
            user.LastPasswordChangeDate = DateTime.Now;

            var hashResult = PasswordCryptographyService.CreateHash(command.NewPassword);
            user.Password = hashResult.Hash;
        }
    }

    /// <summary>
    /// Dto used because json ignore on password properties prevent them
    /// from being serialized.
    /// </summary>
    public class UpdateCurrentUserPasswordModel
    {
        [Required]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 8)]
        public string NewPassword { get; set; }
    }
}