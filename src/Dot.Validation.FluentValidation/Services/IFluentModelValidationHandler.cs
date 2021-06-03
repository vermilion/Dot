using FluentValidation;
using System.Threading.Tasks;

namespace Cofoundry.Domain
{
    /// <summary>
    /// Used to ensure a user has a specific set of permissions before the handler is executed
    /// </summary>
    public interface IFluentModelValidationHandler
    {
    }

    /// <summary>
    /// Used to ensure a user has a specific set of permissions before the handler is executed
    /// </summary>
    public interface IFluentModelValidationHandler<TModel> : IFluentModelValidationHandler
        where TModel : class
    {
        Task Validate(InlineValidator<TModel> validator);
    }
}
