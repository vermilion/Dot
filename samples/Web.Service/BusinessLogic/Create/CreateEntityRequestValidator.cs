using FluentValidation;

namespace Web.Service.BusinessLogic
{
    public class CreateEntityRequestValidator : AbstractValidator<CreateRequest>
    {
        public CreateEntityRequestValidator()
        {
            RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is not valid.");
        }
    }
}
