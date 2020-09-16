using Microsoft.Extensions.DependencyInjection;
using PlatformFramework.Caching;
using PlatformFramework.Hosting;
using PlatformFramework.Threading;
using PlatformFramework.Timing;
using PlatformFramework.Validation;
using System;
using PlatformFramework.Eventing;

namespace PlatformFramework
{
    public class FrameworkBuilder
    {
        public IServiceCollection Services { get; }

        public FrameworkOptions Options { get; } = new FrameworkOptions();

        internal FrameworkBuilder(IServiceCollection services, Action<FrameworkOptions>? configure = null)
        {
            Services = services;

            configure?.Invoke(Options);
        }

        internal T GetOption<T>()
            where T : class, new()
        {
            var defaults = new T();
            if (!Options.ConfigureActions.ContainsKey(typeof(T)))
                return defaults;

            var action = Options.ConfigureActions[typeof(T)];
            action.DynamicInvoke(defaults);

            return defaults;
        }

        /// <summary>
        /// Default Framework parts
        /// </summary>
        /// <returns><see cref="FrameworkBuilder"/></returns>
        public FrameworkBuilder WithDefaults()
        {
            this.WithClockService()
                .WithValidation()
                .WithCaching()
                .WithBackgroundTaskQueue()
                .WithMediator()
                .WithQueuedHostedService();

            return this;
        }
    }
}