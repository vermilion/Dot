using Microsoft.Extensions.DependencyInjection;
using PlatformFramework.Eventing.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PlatformFramework.Eventing
{
    public static class FrameworkBuilderExtensions
    {
        public static FrameworkBuilder WithMediatr(this FrameworkBuilder builder)
        {
            var services = builder.Services;
            var options = builder.Options;

            services.AddTransient<IMediator, Mediator>();
            services.AddTransient<ServiceFactory>(p => p.GetService!);

            services.AddMessageHandlers(options.Assemblies);

            return builder;
        }

        private static void AddMessageHandlers(this IServiceCollection services, IEnumerable<Assembly> assemblies)
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
        }
    }
}