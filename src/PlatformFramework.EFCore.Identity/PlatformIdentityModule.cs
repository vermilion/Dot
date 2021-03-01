using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PlatformFramework.Abstractions;
using PlatformFramework.Eventing;
using System.Reflection;

namespace PlatformFramework.EFCore.Identity
{
    /// <summary>
    /// Common Identity services
    /// </summary>
    public class PlatformIdentityModule : IFrameworkModule
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services
                .AddMediatorHandlers(assembly)
                .AddValidatorsFromAssembly(assembly);
        }
    }
}
