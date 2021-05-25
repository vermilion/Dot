using Cofoundry.Domain;
using Cofoundry.Domain.CQS;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cofoundry.Core.Validation.Internal
{
    /// <summary>
    /// Service for validating models using DataAnnotation validation.
    /// </summary>
    public class FluentModelValidationService : IModelValidationService
    {
        private readonly ILogger _logger;

        public FluentModelValidationService(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Validates the specific model and throws an exception if it is null or 
        /// contains any invalid properties.
        /// </summary>
        /// <typeparam name="TRequest">Type of the model to validate.</typeparam>
        /// <param name="model">The command to validate.</param>
        public async Task Validate<TRequest, TResponse>(TRequest model, IRequestHandler<TRequest, TResponse> handler, IExecutionContext executionContext)
            where TRequest : IRequest<TResponse>
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (handler is IFluentModelValidationHandler)
            {
                if (handler is IFluentModelValidationHandler<TRequest> fluentValidationHandler)
                {
                    await ValidateAndThrow(model, fluentValidationHandler);
                }
            }
        }

        private async Task ValidateAndThrow<TRequest>(TRequest request, IFluentModelValidationHandler<TRequest> handler)
            where TRequest : IRequest
        {
            var validator = new InlineValidator<TRequest>();
            await handler.Validate(validator);

            var context = new ValidationContext<TRequest>(request);
            var validationResults = await validator.ValidateAsync(context);

            if (validationResults.IsValid)
                return;

            _logger.LogWarning("Validation failed with results: {@Results}", validationResults);

            var errors = validationResults.Errors.Select(x => new ValidationError(x.PropertyName, x.ErrorMessage));
            throw new ValidationFailedException("Validation failed", errors);
        }
    }
}
