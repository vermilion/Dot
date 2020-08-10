using Microsoft.Extensions.DependencyInjection;
using PlatformFramework.EFCore.Context.Hooks.Interfaces;
using PlatformFramework.EFCore.Context.Hooks.PredefinedHooks;

namespace PlatformFramework.EFCore
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
            Services.AddTransient<IEntityHook, CreationTrackingHook>();
            Services.AddTransient<IEntityHook, ModificationTrackingHook>();
            Services.AddTransient<IEntityHook, DeletionTrackingHook>();

            return this;
        }

        public EfCoreHooksBuilder WithSoftDeletedEntityHook()
        {
            Services.AddTransient<IEntityHook, SoftDeleteEntityHook>();
            return this;
        }

        public EfCoreHooksBuilder WithHook<THook>()
            where THook : class, IEntityHook
        {
            Services.AddTransient<IEntityHook, THook>();
            return this;
        }
    }
}