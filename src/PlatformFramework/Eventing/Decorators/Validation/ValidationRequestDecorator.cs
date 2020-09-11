using Ardalis.GuardClauses;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using PlatformFramework.Exceptions;
using PlatformFramework.Validation;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlatformFramework.Eventing.Decorators.Validation
{
    [Mapping(typeof(ValidationAttribute))]
    public class ValidationRequestDecorator<TRequest, TResult> : IRequestHandler<TRequest, TResult>
        where TRequest : IRequest<TResult>
    {
        private readonly IRequestHandler<TRequest, TResult> _handler;
        private readonly IServiceProvider _serviceProvider;
        private readonly ValidationAttribute _validationOptions;
        private readonly ILogger _logger;

        public ValidationRequestDecorator(
            IRequestHandler<TRequest, TResult> handler,
            IServiceProvider serviceProvider,
            ILoggerFactory loggerFactory,
            ValidationAttribute validationOptions)
        {
            _handler = handler;
            _serviceProvider = serviceProvider;
            _validationOptions = validationOptions;

            _logger = Guard.Against.Null(loggerFactory, nameof(loggerFactory))
                .CreateLogger(GetType().Name);
        }

        public async Task<TResult> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var validationResults = await RunValidations(request, cancellationToken).ConfigureAwait(false);

            if (validationResults.IsValid)
            {
                return await _handler.Handle(request, cancellationToken).ConfigureAwait(false);
            }

            _logger.LogWarning("Validation failed with results: {@Results}", validationResults);

            var errors = validationResults.Errors.Select(x => new ValidationError(x.PropertyName, x.ErrorMessage));
            throw new ValidationFailedException("Validation failed", errors);
        }

        private async Task<ValidationResult> RunValidations(TRequest model, CancellationToken cancellationToken)
        {
            var validator = _serviceProvider.GetService(_validationOptions.ValidatorType) as IValidator<TRequest>;

            if (validator != null)
            {
                var context = new ValidationContext<TRequest>(model);
                return await validator.ValidateAsync(context, cancellationToken).ConfigureAwait(false);
            }

            throw new Exception($"Validator of type {_validationOptions.ValidatorType.Name} not registered in DI");
        }
    }
}