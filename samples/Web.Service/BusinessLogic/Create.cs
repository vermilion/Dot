using Ardalis.GuardClauses;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PlatformFramework.EFCore.Context;
using PlatformFramework.EFCore.Eventing.Handlers;
using PlatformFramework.EFCore.Eventing.Requests;
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
        public CreateHandler(ILoggerFactory loggerFactory, IUnitOfWork unitOfWork, IMapper mapper)
            : base(loggerFactory, unitOfWork, mapper)
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
