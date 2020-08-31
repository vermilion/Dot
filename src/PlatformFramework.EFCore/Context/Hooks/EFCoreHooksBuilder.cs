using Microsoft.Extensions.DependencyInjection;
using PlatformFramework.EFCore.Context.Hooks.Interfaces;
using PlatformFramework.EFCore.Context.Hooks.PredefinedHooks;

namespace PlatformFramework.EFCore.Context.Hooks
{
    public class EfCoreHooksBuilder
    {
        private IServiceCollection Services { get; }

        public EfCoreHooksBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public EfCoreHooksBuilder WithTrackingHooks()
        {
            Services.AddTransient<IEntityHook, TrackCreatedHook>();
            Services.AddTransient<IEntityHook, TrackUpdatedHook>();

            return this;
        }

        public EfCoreHooksBuilder WithSoftDeletedEntityHook()
        {
            Services.AddTransient<IEntityHook, TrackDeletedHook>();
            return this;
        }

        /// <summary>
        /// Allows to add custom hook to provider, <see cref="IServiceCollection"/>
        /// </summary>
        /// <typeparam name="THook">Hook type</typeparam>
        /// <returns><see cref="EfCoreHooksBuilder"/></returns>
        public EfCoreHooksBuilder WithHook<THook>()
            where THook : class, IEntityHook
        {
            Services.AddTransient<IEntityHook, THook>();
            return this;
        }
    }
}