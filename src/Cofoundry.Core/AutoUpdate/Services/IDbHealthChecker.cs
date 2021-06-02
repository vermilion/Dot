using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace PlatformFramework.EFCore.Context.Migrations
{
    public interface IDbHealthChecker
    {
        Task TestConnection(DbContext context, CancellationToken cancellationToken = default);
    }
}