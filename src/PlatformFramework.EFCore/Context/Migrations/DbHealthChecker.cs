using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlatformFramework.EFCore.Exceptions;

namespace PlatformFramework.EFCore.Context.Migrations
{
    public static class DbHealthChecker
    {
        public static async Task TestConnection(DbContext context, CancellationToken cancellationToken = default)
        {
            const int maxAttemps = 10;
            const int delay = 5000;

            for (var i = 0; i < maxAttemps; i++)
            {
                var canConnect = await CanConnect(context, cancellationToken);
                if (canConnect)
                    return;

                await Task.Delay(delay, cancellationToken);
            }

            // after a few attemps we give up
            throw new DbException("Error waiting database. Check ConnectionString and ensure database exist", null!);
        }

        private static async Task<bool> CanConnect(DbContext context, CancellationToken cancellationToken)
        {
            try
            {
                // Check the database connection
                await context.Database.CanConnectAsync(cancellationToken);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}