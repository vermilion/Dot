using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace PlatformFramework.Mapping
{
    public static class FrameworkBuilderExtensions
    {
        /// <summary>
        /// Register Mapper configuration
        /// </summary>
        /// <param name="builder">Builder</param>
        /// <param name="configureAction">Configure <see cref="MapperConfigurationExpression"/></param>
        /// <returns>Fluent builder</returns>
        public static FrameworkBuilder WithMappers(this FrameworkBuilder builder, Action<MapperConfigurationExpression> configureAction)
        {
            var services = builder.Services;
            var options = builder.Options;

            var expression = new MapperConfigurationExpression();
            configureAction?.Invoke(expression);

            var automapperConfig = new MapperConfiguration(expression);
            services.AddSingleton(automapperConfig.CreateMapper());

            return builder;
        }
    }
}