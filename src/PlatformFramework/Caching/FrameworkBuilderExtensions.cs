using Microsoft.Extensions.DependencyInjection;
using PlatformFramework.Abstractions;

namespace PlatformFramework.Caching
{
    public static class FrameworkBuilderExtensions
    {
        /// <summary>
        /// Adds caching part to Framework configuration
        /// </summary>
        /// <param name="builder"><see cref="FrameworkBuilder"/></param>
        /// <returns><see cref="FrameworkBuilder"/></returns>
        public static FrameworkBuilder WithCaching(this FrameworkBuilder builder)
        {
            var services = builder.Services;

            services.AddDistributedMemoryCache();
            services.AddSingleton<ICacheService, MemoryCacheService>();

            return builder;
        }
    }
}