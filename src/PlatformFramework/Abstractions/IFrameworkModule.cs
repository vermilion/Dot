using Microsoft.Extensions.DependencyInjection;

namespace PlatformFramework.Abstractions
{
    /// <summary>
    /// Framework module marker
    /// </summary>
    public interface IFrameworkModule
    {
        /// <summary>
        /// Used for registering services
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/></param>
        void ConfigureServices(IServiceCollection services);
    }
}