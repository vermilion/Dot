using Microsoft.Extensions.DependencyInjection;

namespace PlatformFramework.Hosting
{
    public static class FrameworkBuilderExtensions
    {
        public static FrameworkBuilder WithQueuedHostedService(this FrameworkBuilder builder)
        {
            var services = builder.Services;

            services.AddHostedService<QueuedHostedService>();

            return builder;
        }
    }
}