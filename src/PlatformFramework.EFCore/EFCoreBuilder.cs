using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlatformFramework.EFCore.Context.Hooks;
using PlatformFramework.EFCore.Context.Migrations;
using System;

namespace PlatformFramework.EFCore
{
    public class EfCoreBuilder<TDbContext>
        where TDbContext : DbContext
    {
        public IServiceCollection Services { get; }

        public EfCoreBuilder(IServiceCollection services)
        {
            Services = services;
        }

        /// <summary>
        /// Use default migrations initializer
        /// </summary>
        /// <returns>Fluent builder</returns>
        public EfCoreBuilder<TDbContext> WithMigrationInitializer()
        {
            Services.AddHostedService<DbContextMigrationsInitializer<TDbContext>>();
            return this;
        }

        /// <summary>
        /// Use hooks for context entities interception
        /// </summary>
        /// <param name="configureAction">Configure <see cref="EfCoreHooksBuilder"/></param>
        /// <returns>Fluent builder</returns>
        public EfCoreBuilder<TDbContext> WithHooks(Action<EfCoreHooksBuilder> configureAction)
        {
            var builder = new EfCoreHooksBuilder(Services);
            configureAction?.Invoke(builder);

            return this;
        }
    }
}