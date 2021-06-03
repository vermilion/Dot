using System.Threading.Tasks;

namespace Cofoundry.Core.AutoUpdate
{
    /// <summary>
    /// Base interface for IAlwaysRunUpdateCommand handlers
    /// </summary>
    /// <typeparam name="TCommand">Type of command to execute</typeparam>
    public interface IAlwaysRunUpdateCommandHandler<in TCommand> where TCommand : IAlwaysRunUpdateCommand
    {
        /// <summary>
        /// Executes the specified command asynchronously.
        /// </summary>
        /// <param name="command">Command to execute</param>
        Task ExecuteAsync(TCommand command);
    }
}
