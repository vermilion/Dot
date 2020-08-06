using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace PlatformFramework.Validation
{
    /// <summary>
    /// Register the validation infrastructure's services
    /// </summary>
    public static class FrameworkBuilderExtensions
    {
        public static FrameworkBuilder WithValidation(this FrameworkBuilder builder)
        {
            var services = builder.Services;
            var options = builder.Options;

            services.AddTransient<IValidatorFactory, ServiceProviderValidatorFactory>();

            services.AddValidatorsFromAssemblies(options.Assemblies);

            return builder;
        }
    }
}