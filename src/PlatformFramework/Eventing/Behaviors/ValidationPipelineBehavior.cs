using Ardalis.GuardClauses;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using PlatformFramework.Exceptions;
using PlatformFramework.Validation;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlatformFramework.Eventing.Behaviors
{
    [DebuggerStepThrough]
    public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IValidatorFactory _validatorFactory;

        private readonly ILogger _logger;

        public ValidationPipelineBehavior(
            ILoggerFactory loggerFactory,
            IValidatorFactory validatorFactory)
        {
            _validatorFactory = validatorFactory;

            _logger = Guard.Against.Null(loggerFactory, nameof(loggerFactory))
                .CreateLogger(GetType().Name);
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var validationResults = await RunValidations(request);

            if (validationResults.IsValid)
                return await next();

            _logger.LogWarning("Validation failed with results: {@Results}", validationResults);

            var errors = validationResults.Errors.Select(x => new ValidationError(x.PropertyName, x.ErrorMessage));
            throw new ValidationFailedException("Validation failed", errors);
        }

        private async Task<ValidationResult> RunValidations(TRequest model)
        {
            var validator = _validatorFactory.GetValidator<TRequest>();

            if (validator != null)
                return await validator.ValidateAsync(model);

            _logger.LogDebug("Validator not found for type: {@Type}", typeof(TRequest));

            return new ValidationResult();
        }
    }
}
