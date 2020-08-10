using System.Threading;
using System.Threading.Tasks;

namespace PlatformFramework.EFCore.Abstractions
{
    /// <summary>
    /// Interface incapsulating seed method
    /// </summary>
    public interface IDbSeedProvider
    {
        /// <summary>
        /// Seed method implemented against Context
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns></returns>
        Task Seed(CancellationToken cancellationToken = default);
    }
}