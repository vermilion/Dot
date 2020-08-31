using Ardalis.GuardClauses;
using FluentValidation;
using PlatformFramework.EFCore.Eventing.Handlers;
using PlatformFramework.EFCore.Eventing.Requests;
using System;
using Web.Service.Domain;

namespace Web.Service.BusinessLogic
{
    public class UpdateRequest : EntityUpdateRequest<MyEntityModel>
    {
        public UpdateRequest(int id, MyEntityModel model) : base(id, model)
        {
        }
    }

    public class UpdateHandler : EntityUpdateHandlerBase<MyEntity, MyEntityModel, UpdateRequest>
    {
        public UpdateHandler(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }
    }

    public class UpdateEntityRequestValidator : AbstractValidator<UpdateRequest>
    {
        public UpdateEntityRequestValidator(MyEntityModelValidator modelValidator)
        {
            Guard.Against.Null(modelValidator, nameof(modelValidator));

            RuleFor(x => x.Model)
                .NotNull();

            RuleFor(x => x.Model).SetValidator(modelValidator!);
        }
    }
}
