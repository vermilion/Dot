using Ardalis.GuardClauses;
using FluentValidation;
using PlatformFramework.EFCore.Eventing.Handlers;
using PlatformFramework.EFCore.Eventing.Requests;
using System;
using Web.Service.Domain;

namespace Web.Service.BusinessLogic
{
    public class CreateRequest : EntityCreateRequest<MyEntityModel>
    {
        public CreateRequest(MyEntityModel model) : base(model)
        {
        }
    }

    public class CreateHandler : EntityCreateHandlerBase<MyEntity, MyEntityModel, CreateRequest>
    {
        public CreateHandler(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }
    }

    public class CreateRequestValidator : AbstractValidator<CreateRequest>
    {
        public CreateRequestValidator(MyEntityModelValidator modelValidator)
        {
            Guard.Against.Null(modelValidator, nameof(modelValidator));

            RuleFor(x => x.Model)
                .NotNull();

            RuleFor(x => x.Model).SetValidator(modelValidator!);
        }
    }
}
