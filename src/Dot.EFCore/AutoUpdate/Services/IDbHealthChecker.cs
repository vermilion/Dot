using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Dot.EFCore.AutoUpdate.Services
{
    public interface IDbHealthChecker
    {
        Task TestConnection(DbContext context, CancellationToken cancellationToken = default);
    }
}