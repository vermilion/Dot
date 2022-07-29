using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dot.EFCore.Health.Services
{
    public class DbHealthChecker : IDbHealthChecker
    {
        protected virtual int MaxAttepmts { get { return 10; } }

        protected virtual int Delay { get { return 5000; } }

        public async Task TestConnection(DbContext context, CancellationToken cancellationToken = default)
        {
            for (var i = 0; i < MaxAttepmts; i++)
            {
                var canConnect = await CanConnect(context, cancellationToken);
                if (canConnect)
                    return;

                await Task.Delay(Delay, cancellationToken);
            }

            // after a few attemps we give up
            throw new Exception("Error waiting database. Check ConnectionString and ensure database exist", null!);
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