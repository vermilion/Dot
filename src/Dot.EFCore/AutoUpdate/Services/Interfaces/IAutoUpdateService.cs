using System.Threading;
using System.Threading.Tasks;

namespace Dot.EFCore.AutoUpdate.Services.Interfaces
{
    /// <summary>
    /// Service to update applications and modules.
    /// </summary>
    public interface IAutoUpdateService
    {
        /// <summary>
        /// Updates an application and referenced modules by scanning for implementations
        /// of IUpdatePackageFactory and executing any packages found.
        /// </summary>
        /// <param name="cancellationToken">
        /// Optional cancellation token that can be used to try and stop the update early, although
        /// a unit of updates will attempt to be completed before stopping.
        /// </param>
        Task UpdateAsync(CancellationToken cancellationToken = default);
    }
}
