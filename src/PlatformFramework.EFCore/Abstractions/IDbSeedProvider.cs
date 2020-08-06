using System.Threading;
using System.Threading.Tasks;

namespace PlatformFramework.Interfaces.Data
{
    public interface IDbSeedProvider
    {
        Task Seed(CancellationToken cancellationToken = default);
    }
}