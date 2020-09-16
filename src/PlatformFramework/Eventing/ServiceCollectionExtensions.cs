using Microsoft.Extensions.DependencyInjection;
using PlatformFramework.Eventing.Helpers;
using System.Linq;
using System.Reflection;

namespace PlatformFramework.Eventing
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMediatorHandlers(this IServiceCollection services, params Assembly[] assemblies)
        {
            foreach (var type in assemblies.SelectMany(assembly => assembly.GetTypes()))
            {
                var handlerInterfaces = type.GetInterfaces()
                   .Where(Utils.IsHandlerInterface)
                   .ToList();

                if (handlerInterfaces.Count > 0)
                {
                    var handlerFactory = new HandlerFactory(type);

                    // register handlers
                    foreach (var interfaceType in handlerInterfaces)
                    {
                        services.AddTransient(interfaceType, provider => handlerFactory.Create(provider, interfaceType));
                    }
                }
            }

            return services;
        }
    }
}