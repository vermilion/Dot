using System.Threading.Tasks;

namespace Cofoundry.Domain.CQS
{
    /// <summary>
    /// Service to validate the model of command/query handler prior to execution.
    /// </summary>
    public interface IExecuteModelValidationService
    {
        Task Validate<TRequest, TResponse>(TRequest model, IRequestHandler<TRequest, TResponse> handler, IExecutionContext executionContext)
             where TRequest : IRequest<TResponse>;
    }
}
