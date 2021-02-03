using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlatformFramework.EFCore.Abstractions;
using PlatformFramework.EFCore.Context;
using PlatformFramework.EFCore.Entities;
using System;

namespace PlatformFramework.EFCore
{
    /// <summary>
    ///     Nice method to create the EFCore builder
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        ///     Add the services (application specific class)
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/></param>
        /// <param name="optionsAction">Action for configuring <see cref="DbContextOptionsBuilder"/></param>
        /// <returns>Builder instance</returns>
        public static EfCoreBuilder<TDbContext> AddEfCore<TDbContext>(this IServiceCollection services, Action<DbContextOptionsBuilder>? optionsAction = null)
            where TDbContext : DbContext
        {
            services.AddDbContext<TDbContext>(optionsAction);

            services.AddScoped<IUnitOfWork>(provider =>
            {
                var context = provider.GetRequiredService<TDbContext>();
                return new UnitOfWork(context);
            });

            services.AddTransient(typeof(IEntityService<>), typeof(EntityService<>));

            return new EfCoreBuilder<TDbContext>(services);
        }
    }
}