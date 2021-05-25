using System.Threading.Tasks;

namespace Cofoundry.Domain.CQS
{
    /// <summary>
    /// Send a request through the mediator pipeline to be handled by a single handler.
    /// </summary>
    public interface IMediator
    {
        /// <summary>
        /// Asynchronously send a request to a single handler
        /// </summary>
        /// <typeparam name="TResponse">Response type</typeparam>
        /// <param name="request">Request object</param>
        /// <param name="executionContext">
        /// Optional custom execution context which can be used to impersonate/elevate permissions 
        /// or change the execution date.
        /// </param>
        /// <returns>A task that represents the send operation. The task result contains the handler response</returns>
        Task<TResponse> ExecuteAsync<TResponse>(IRequest<TResponse> request, IExecutionContext executionContext = null);
    }
}
