using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PlatformFramework.Abstractions;
using PlatformFramework.Caching;
using PlatformFramework.Eventing;
using PlatformFramework.Eventing.Helpers;
using PlatformFramework.Hosting;
using PlatformFramework.Sessions;
using PlatformFramework.Threading;
using PlatformFramework.Timing;

namespace PlatformFramework
{
    public class FrameworkModule : IFrameworkModule
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // clock
            services.TryAddTransient<IClockProvider, ClockProvider>();

            // validation
            services.TryAddTransient<IValidatorFactory, ServiceProviderValidatorFactory>();

            //services.AddValidatorsFromAssemblies(options.Assemblies);

            // caching
            services.AddDistributedMemoryCache();
            services.TryAddSingleton<ICacheService, MemoryCacheService>();

            // task queue
            services.TryAddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
            services.AddHostedService<QueuedHostedService>();

            // mediator
            services.TryAddTransient<IMediator, Mediator>();

            // session
            services.AddHttpContextAccessor();
            services.AddScoped<IUserSession, UserSession>();
        }
    }
}