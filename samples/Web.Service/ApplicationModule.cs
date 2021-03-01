using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PlatformFramework.Abstractions;
using PlatformFramework.EFCore.Abstractions;
using PlatformFramework.Eventing;
using System.Reflection;
using Web.Service;
using Web.Service.BusinessLogic;

namespace PlatformFramework.EFCore.Identity
{
    /// <summary>
    /// App module services registration
    /// </summary>
    public class ApplicationModule : IFrameworkModule
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services
                .AddMediatorHandlers(assembly)
                .AddValidatorsFromAssembly(assembly);

            services.AddTransient<IDbSeedProvider, ProjectDbContextSeedProvider>();

            services.AddScoped<IMyEntityService, MyEntityService>();
        }
    }
}
