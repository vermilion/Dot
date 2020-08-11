using FluentValidation;

namespace Web.Service
{
    public class MyEntityValidator : AbstractValidator<MyEntityModel>
    {
        public MyEntityValidator()
        {
            RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is not valid.");
        }

    }
}
