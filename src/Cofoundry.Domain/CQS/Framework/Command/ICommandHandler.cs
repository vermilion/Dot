using System.Threading.Tasks;

namespace Cofoundry.Domain.CQS
{
    /// <summary>
    /// Handles asynchronous execution of an ICommand.
    /// </summary>
    /// <typeparam name="TCommand">Type of Command to handle</typeparam>
    public interface ICommandHandler<TCommand>
         where TCommand : ICommand
    {
        Task ExecuteAsync(TCommand command, IExecutionContext executionContext);
    }
}
