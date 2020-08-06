using Microsoft.Extensions.DependencyInjection;
using PlatformFramework.Interfaces.Caching;

namespace PlatformFramework.Caching
{
    public static class FrameworkBuilderExtensions
    {
        public static FrameworkBuilder WithCaching(this FrameworkBuilder builder)
        {
            var services = builder.Services;

            services.AddDistributedMemoryCache();
            services.AddSingleton<ICacheService, MemoryCacheService>();

            return builder;
        }
    }
}