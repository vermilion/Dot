using System;
using PlatformFramework.EFCore.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace PlatformFramework.EFCore
{
    /// <summary>
    ///     Nice method to create the EFCore builder
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        ///     Add the services (application specific class)
        /// </summary>
        /// <param name="services"></param>
        /// <param name="optionsAction"></param>
        /// <returns></returns>
        public static EfCoreBuilder<TDbContext> AddEfCore<TDbContext>(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction = null)
            where TDbContext : DbContext, IUnitOfWork
        {
            services.AddDbContext<TDbContext>(optionsAction);
            services.AddScoped(provider => (IUnitOfWork)provider.GetRequiredService(typeof(TDbContext)));

            return new EfCoreBuilder<TDbContext>(services);
        }
    }
}