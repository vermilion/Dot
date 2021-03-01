using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PlatformFramework.Exceptions;
using PlatformFramework.Validation;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlatformFramework.Eventing.Helpers
{
    internal abstract class RequestHandlerWrapper<TResponse>
    {
        public ILogger? _logger;

        internal void InitializeLogger(ILogger logger)
        {
            _logger = logger;
        }

        public abstract Task<TResponse> Handle(IRequest<TResponse> request, IServiceProvider serviceProvider, CancellationToken cancellationToken);
    }

    internal class RequestHandlerWrapperImpl<TRequest, TResponse> : RequestHandlerWrapper<TResponse>
        where TRequest : IRequest<TResponse>
    {
        public override async Task<TResponse> Handle(IRequest<TResponse> request, IServiceProvider serviceProvider, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Executing request of type {@RequestType}: {@Request}", request.GetType().Name, request);

            var req = (TRequest)request;

            var handler = GetHandler<IRequestHandler<TRequest, TResponse>>(serviceProvider);

            var validator = new InlineValidator<TRequest>();
            await handler.Validate(validator, cancellationToken);

            await ValidateAndThrow(req, handler, cancellationToken);

            var result = await handler.Handle(req, cancellationToken).ConfigureAwait(false);

            _logger.LogDebug("Executed request: {@Result}", result);

            return result;
        }

        private async Task ValidateAndThrow(TRequest request, IRequestHandler<TRequest, TResponse> handler, CancellationToken cancellationToken)
        {
            var validator = new InlineValidator<TRequest>();
            await handler.Validate(validator, cancellationToken);

            var context = new ValidationContext<TRequest>(request);
            var validationResults = await validator.ValidateAsync(context, cancellationToken);

            if (validationResults.IsValid)
                return;

            _logger.LogWarning("Validation failed with results: {@Results}", validationResults);

            var errors = validationResults.Errors.Select(x => new ValidationError(x.PropertyName, x.ErrorMessage));
            throw new ValidationFailedException("Validation failed", errors);
        }

        private static THandler GetHandler<THandler>(IServiceProvider serviceProvider)
            where THandler : notnull
        {
            THandler handler;

            try
            {
                handler = serviceProvider.GetRequiredService<THandler>();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Error constructing handler for request of type {typeof(THandler)}. Register your handlers with the container", e);
            }

            if (handler == null)
            {
                throw new InvalidOperationException($"Handler was not found for request of type {typeof(THandler)}. Register your handlers with the container");
            }

            return handler;
        }
    }
}
