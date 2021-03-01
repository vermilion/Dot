using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace PlatformFramework.Eventing
{
    public interface IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        /// <summary>
        /// Handles a request
        /// </summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Response from the request</returns>
        Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);

        Task Validate(InlineValidator<TRequest> validator, CancellationToken cancellationToken);
    }
}