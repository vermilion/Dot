using Microsoft.Extensions.DependencyInjection;
using PlatformFramework.EFCore.Context.Hooks.Interfaces;
using PlatformFramework.EFCore.Context.Hooks.PrefefinedHooks;

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
            Services.AddTransient<IDbContextEntityHook, CreationTrackingHook>();
            Services.AddTransient<IDbContextEntityHook, ModificationTrackingHook>();
            Services.AddTransient<IDbContextEntityHook, DeletionTrackingHook>();

            return this;
        }

        public EfCoreHooksBuilder WithSoftDeletedEntityHook()
        {
            Services.AddTransient<IDbContextEntityHook, SoftDeleteEntityHook>();
            return this;
        }

        public EfCoreHooksBuilder WithHook<THook>()
            where THook : class, IDbContextEntityHook
        {
            Services.AddTransient<IDbContextEntityHook, THook>();
            return this;
        }
    }
}