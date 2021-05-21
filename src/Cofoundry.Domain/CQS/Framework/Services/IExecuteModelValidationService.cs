namespace Cofoundry.Domain.CQS
{
    /// <summary>
    /// Service to validate the model of command/query handler prior to execution.
    /// </summary>
    public interface IExecuteModelValidationService
    {
        void Validate<TCommand>(TCommand command, ICommandHandler<TCommand> commandHandler, IExecutionContext executionContext) where TCommand : ICommand;

        void Validate<TQuery, TResult>(TQuery query, IQueryHandler<TQuery, TResult> queryHandler, IExecutionContext executionContext) where TQuery : IQuery<TResult>;
    }
}
