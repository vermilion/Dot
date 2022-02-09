using System.Threading.Tasks;

namespace Cofoundry.Core.AutoUpdate
{
    /// <summary>
    /// Handler for executing IVersionedUpdateCommand asynchronously
    /// </summary>
    /// <typeparam name="TCommand">Type of command to execute</typeparam>
    public interface IVersionedUpdateCommandHandler<in TCommand>
        where TCommand : IVersionedUpdateCommand
    {
        /// <summary>
        /// Executes the specified command asynchronously.
        /// </summary>
        /// <param name="command">Command to execute</param>
        Task ExecuteAsync(TCommand command);
    }
}
