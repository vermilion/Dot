using Ardalis.GuardClauses;
using FluentValidation;
using PlatformFramework.EFCore.Abstractions;
using PlatformFramework.EFCore.Eventing.Handlers;
using PlatformFramework.EFCore.Eventing.Requests;
using PlatformFramework.Eventing.Decorators.Validation;
using System;
using System.Threading.Tasks;
using Web.Service.Domain;

namespace Web.Service.BusinessLogic
{
    public class MyEntityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MyEntityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MyEntityModel> Create(MyEntityModel model)
        {
            var myEntityService = _unitOfWork.GetEntityServiceFor<MyEntity>();

            var entity = myEntityService.ProjectFrom(model);

            var result = await myEntityService.Add(entity);
            await _unitOfWork.SaveChangesAsync();

            return myEntityService.ProjectTo<MyEntityModel>(result);
        }
    }


    public class CreateRequest : EntityCreateRequest<MyEntityModel>
    {
        public CreateRequest(MyEntityModel model) : base(model)
        {
        }
    }

    [Validation(typeof(CreateRequestValidator))]
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
