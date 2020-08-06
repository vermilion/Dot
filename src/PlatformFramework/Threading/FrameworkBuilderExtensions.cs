using Microsoft.Extensions.DependencyInjection;
using PlatformFramework.Abstractions;

namespace PlatformFramework.Threading
{
    public static class FrameworkBuilderExtensions
    {
        public static FrameworkBuilder WithBackgroundTaskQueue(this FrameworkBuilder builder)
        {
            var services = builder.Services;

            services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();

            return builder;
        }
    }
}