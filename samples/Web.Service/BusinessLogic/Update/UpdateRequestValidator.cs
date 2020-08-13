using Ardalis.GuardClauses;
using FluentValidation;

namespace Web.Service.BusinessLogic
{
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
