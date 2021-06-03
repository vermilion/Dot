using Cofoundry.Core.Validation;
using Cofoundry.Domain.CQS;
using System.Threading.Tasks;

namespace Cofoundry.Domain.Internal
{
    /// <summary>
    /// Service to validate the model of command/query handler prior to execution.
    /// </summary>
    public class ExecuteModelValidationService : IExecuteModelValidationService
    {
        #region constructor

        private readonly IModelValidationService _modelValidationService;

        public ExecuteModelValidationService(
            IModelValidationService modelValidationService
            )
        {
            _modelValidationService = modelValidationService;
        }

        #endregion

        public virtual Task Validate<TRequest, TResponse>(TRequest query, IRequestHandler<TRequest, TResponse> queryHandler, IExecutionContext executionContext) 
            where TRequest : IRequest<TResponse>
        {
            return _modelValidationService.Validate(query, queryHandler, executionContext);
        }
    }
}
