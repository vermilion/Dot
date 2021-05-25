namespace Cofoundry.Domain.CQS
{
    /// <summary>
    /// Service to validate the model of command/query handler prior to execution.
    /// </summary>
    public interface IExecuteModelValidationService
    {
        void Validate<TQuery, TResult>(TQuery query, IRequestHandler<TQuery, TResult> queryHandler, IExecutionContext executionContext) where TQuery : IRequest<TResult>;
    }
}
