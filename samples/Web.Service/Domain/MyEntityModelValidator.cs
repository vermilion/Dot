using FluentValidation;

namespace Web.Service.Domain
{
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
