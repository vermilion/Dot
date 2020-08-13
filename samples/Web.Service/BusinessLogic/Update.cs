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
    public class UpdateRequest : EntityUpdateRequest<MyEntityModel>
    {
        public UpdateRequest(long id, MyEntityModel model) : base(id, model)
        {
        }
    }

    public class UpdateHandler : EntityUpdateHandlerBase<MyEntity, MyEntityModel, UpdateRequest>
    {
        public UpdateHandler(ILoggerFactory loggerFactory, IUnitOfWork unitOfWork, IMapper mapper)
            : base(loggerFactory, unitOfWork, mapper)
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
