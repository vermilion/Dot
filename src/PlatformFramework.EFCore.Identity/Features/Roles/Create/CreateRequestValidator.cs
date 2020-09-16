using FluentValidation;
using PlatformFramework.EFCore.Abstractions;
using PlatformFramework.EFCore.Eventing.Handlers;
using PlatformFramework.EFCore.Eventing.Requests;
using PlatformFramework.EFCore.Identity.Entities;
using PlatformFramework.EFCore.Identity.Models;
using System;
using System.Linq;

namespace PlatformFramework.EFCore.Identity.Features.Roles.Create
{
    public class CreateRequestValidator : AbstractValidator<CreateRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateRequestValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(m => m.Model.Id)
                .Must(BeUnique)
                .WithMessage("Role must be unique!");
        }

        private bool BeUnique(int id)
        {
            return !_unitOfWork.Set<Role>().Any(x => x.Id == id);
        }
    }
}
