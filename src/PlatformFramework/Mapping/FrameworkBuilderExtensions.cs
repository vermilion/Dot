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
        /// <param name="builder"><see cref="FrameworkBuilder"/></param>
        /// <param name="configureAction">Configure <see cref="MapperConfigurationExpression"/></param>
        /// <returns>Fluent builder</returns>
        public static FrameworkBuilder WithMappers(this FrameworkBuilder builder, Action<MapperConfigurationExpression> configureAction)
        {
            var expression = new MapperConfigurationExpression();
            configureAction?.Invoke(expression);

            var automapperConfig = new MapperConfiguration(expression);
            builder.Services.AddSingleton(automapperConfig.CreateMapper());

            return builder;
        }
    }
}
