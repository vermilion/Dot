using MediatR;
using System;
using Microsoft.Extensions.DependencyInjection;
using PlatformFramework.Extensions;

namespace PlatformFramework.Eventing
{
    public static class FrameworkBuilderExtensions
    {
        public static FrameworkBuilder WithMediatr(this FrameworkBuilder builder)
        {
            var services = builder.Services;
            var options = builder.Options;

            var config = builder.GetOption<FrameworkMediatrOptions>();

            services.AddMediatR(options.Assemblies, x => x.AsTransient());

            foreach (var behavior in config.Behaviors)
            {
                if (!behavior.IsAssignableToGenericType(typeof(IPipelineBehavior<,>)))
                    throw new ArgumentException("Invalid behavior type");

                services.AddTransient(typeof(IPipelineBehavior<,>), behavior);
            }

            return builder;
        }
    }
}