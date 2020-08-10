using Ardalis.GuardClauses;
using FluentValidation;

namespace Web.Service.BusinessLogic
{
    public class CreateEntityRequestValidator : AbstractValidator<CreateRequest>
    {
        public CreateEntityRequestValidator(MyEntityModelValidator modelValidator)
        {
            Guard.Against.Null(modelValidator, nameof(modelValidator));

            RuleFor(x => x.Model)
                .NotNull();

            RuleFor(x => x.Model).SetValidator(modelValidator!);
        }
    }

    public class MyEntityModelValidator : AbstractValidator<MyEntityModel>
    {
        public MyEntityModelValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title is not valid.");
        }
    }
}
