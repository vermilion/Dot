using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlatformFramework.EFCore.Context.Hooks;
using PlatformFramework.EFCore.Context.Migrations;
using PlatformFramework.EFCore.Entities;
using System;

namespace PlatformFramework.EFCore
{
    public class EfCoreBuilder<TDbContext>
        where TDbContext : DbContext
    {
        private IServiceCollection Services { get; }

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

        /// <summary>
        /// Register Mapper configuration
        /// </summary>
        /// <param name="configureAction">Configure <see cref="MapperConfigurationExpression"/></param>
        /// <returns>Fluent builder</returns>
        public EfCoreBuilder<TDbContext> WithMappers(Action<MapperConfigurationExpression> configureAction)
        {
            var expression = new MapperConfigurationExpression();
            configureAction?.Invoke(expression);

            var automapperConfig = new MapperConfiguration(expression);
            Services.AddSingleton(automapperConfig.CreateMapper());

            return this;
        }

        /// <summary>
        /// Register Entities and their Customizers
        /// </summary>
        /// <param name="configureAction">Configure <see cref="EfCoreEntitiesRegistryBuilder"/></param>
        /// <returns>Fluent builder</returns>
        public EfCoreBuilder<TDbContext> WithEntities(Action<EfCoreEntitiesRegistryBuilder> configureAction)
        {
            var registry = new EntitiesRegistry();
            Services.AddSingleton(registry);

            var builder = new EfCoreEntitiesRegistryBuilder(registry);
            configureAction?.Invoke(builder);

            return this;
        }
    }
}