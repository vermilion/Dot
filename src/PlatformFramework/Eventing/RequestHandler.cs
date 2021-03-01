using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace PlatformFramework.Eventing
{
    /// <summary>
    /// Defines a handler for a request
    /// </summary>
    /// <typeparam name="TRequest">The type of request being handled</typeparam>
    /// <typeparam name="TResponse">The type of response from the handler</typeparam>
    public abstract class RequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        /// <inheritdoc />
        public virtual Task Validate(InlineValidator<TRequest> validator, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
