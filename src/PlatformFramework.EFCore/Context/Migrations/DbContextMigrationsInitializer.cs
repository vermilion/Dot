using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PlatformFramework.EFCore.Abstractions;

namespace PlatformFramework.EFCore.Context.Migrations
{
    internal class DbContextMigrationsInitializer<TContext> : BackgroundService
        where TContext : DbContext
    {
        private readonly IServiceProvider _provider;

        public DbContextMigrationsInitializer(IServiceProvider provider)
        {
            _provider = provider;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            using (var scope = _provider.CreateScope())
            {
                var provider = scope.ServiceProvider;

                var logger = provider.GetRequiredService<ILogger<TContext>>();
                var setup = provider.GetService<IDbSeedProvider>();
                var context = provider.GetRequiredService<TContext>();

                try
                {
                    logger.LogInformation($"Migrating database associated with context {typeof(TContext).Name}");

                    await DbHealthChecker.TestConnection(context, cancellationToken);

                    await context.Database.MigrateAsync(cancellationToken);

                    if (setup != null)
                        await setup.Seed(cancellationToken);

                    logger.LogInformation($"Migrated database associated with context {typeof(TContext).Name}");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"An error occurred while migrating the database used on context {typeof(TContext).Name}");
                    throw;
                }
            }
        }
    }
}