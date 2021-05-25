namespace Cofoundry.Domain.CQS
{
    /// <summary>
    /// Service to validate the permissions of command/query handler prior to execution.
    /// </summary>
    public interface IExecutePermissionValidationService
    {
        void Validate<TQuery, TResult>(TQuery query, IRequestHandler<TQuery, TResult> queryHandler, IExecutionContext executionContext) where TQuery : IRequest<TResult>;
    }
}
