using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlatformFramework.EFCore.Context;
using PlatformFramework.EFCore.Context.Migrations;
using PlatformFramework.EFCore.Entities;

namespace PlatformFramework.EFCore
{
    public class EfCoreBuilder<TDbContext>
        where TDbContext : DbContext, IUnitOfWork
    {
        private IServiceCollection Services { get; }

        public EfCoreBuilder(IServiceCollection services)
        {
            Services = services;
        }

        /// <summary>
        /// Использование стандартного сервиса миграций
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <returns></returns>
        public EfCoreBuilder<TDbContext> WithMigrationInitializer()
        {
            Services.AddHostedService<DbContextMigrationsInitializer<TDbContext>>();
            return this;
        }

        /// <summary>
        /// Использование хуков для контекста
        /// </summary>
        /// <param name="configureAction"></param>
        /// <returns></returns>
        public EfCoreBuilder<TDbContext> WithHooks(Action<EfCoreHooksBuilder> configureAction)
        {
            var builder = new EfCoreHooksBuilder(Services);
            configureAction?.Invoke(builder);

            return this;
        }

        /// <summary>
        /// Регистрация сущностей и их кастомизаторов
        /// </summary>
        /// <param name="configureAction"></param>
        /// <returns></returns>
        public EfCoreBuilder<TDbContext> WithEntities(Action<EfCoreEntitiesRegistryBuilder> configureAction)
        {
            var registry = new EntitiesRegistry();
            Services.AddSingleton(registry);

            var builder = new EfCoreEntitiesRegistryBuilder(registry);
            configureAction?.Invoke(builder);

            registry.ConfigureServices(Services);

            return this;
        }
    }
}