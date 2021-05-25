using Cofoundry.Domain.CQS;
using FluentValidation;
using System.Collections.Generic;
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
    public interface IFluentModelValidationHandler<TRequest> : IFluentModelValidationHandler
        where TRequest : IRequest
    {
        Task Validate(InlineValidator<TRequest> validator);
    }
}
