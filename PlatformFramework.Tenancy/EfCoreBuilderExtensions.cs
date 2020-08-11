using Finbuckle.MultiTenant;
using Microsoft.Extensions.DependencyInjection;
using PlatformFramework.EFCore;
using PlatformFramework.EFCore.Context;

namespace PlatformFramework.Tenancy
{
    public static class EfCoreBuilderExtensions
    {
        /// <summary>
        ///     Add the services (application specific class)
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/></param>
        /// <returns>Builder instance</returns>
        public static EfCoreBuilder<TDbContext> AddMultitenantEfCore<TDbContext>(this IServiceCollection services)
            where TDbContext : MultiTenantDbContextCore, IUnitOfWork
        {
            services
                .AddMultiTenant<TenantInfo>()
                .WithConfigurationStore()
                .WithBasePathStrategy();

            return services.AddEfCore<TDbContext>(null);
        }
    }
}
