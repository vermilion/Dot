using System.Threading;
using System.Threading.Tasks;

namespace PlatformFramework.EFCore.Abstractions
{
    public interface IDbSeedProvider
    {
        Task Seed(CancellationToken cancellationToken = default);
    }
}