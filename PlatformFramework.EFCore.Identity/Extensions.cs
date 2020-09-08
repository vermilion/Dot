using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlatformFramework.EFCore.Abstractions;
using PlatformFramework.EFCore.Context;
using System;

namespace PlatformFramework.EFCore.Identity
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
        public static EfCoreBuilder<TDbContext> AddIdentityEfCore<TDbContext>(this IServiceCollection services, Action<DbContextOptionsBuilder>? optionsAction = null)
            where TDbContext : IdentityDbContext
        {
            services.AddDbContext<TDbContext>(optionsAction);

            services.AddScoped<IUnitOfWork>(provider =>
            {
                var context = provider.GetRequiredService<TDbContext>();
                return new UnitOfWork(context);
            });

            return new EfCoreBuilder<TDbContext>(services);
        }
    }
}