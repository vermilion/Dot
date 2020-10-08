using FluentValidation;
using PlatformFramework.EFCore.Abstractions;
using PlatformFramework.EFCore.Identity.Entities;
using System.Linq;

namespace PlatformFramework.EFCore.Identity.Features.RoleClaims.Create
{
    public class CreateRequestValidator : AbstractValidator<CreateRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateRequestValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(m => m.Model.Id)
                .Must(BeUnique)
                .WithMessage("RoleClaim must be unique!");
        }

        private bool BeUnique(int id)
        {
            return !_unitOfWork.Set<RoleClaim>().Any(x => x.Id == id);
        }
    }
}
