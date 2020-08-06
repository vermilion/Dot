using System;
using Microsoft.Extensions.DependencyInjection;

namespace PlatformFramework
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавление функционала стандартного Framework
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/></param>
        /// <param name="configure">Метод конфигурирования для <see cref="FrameworkOptions"/></param>
        /// <returns><see cref="FrameworkBuilder"/></returns>
        public static FrameworkBuilder AddFramework(this IServiceCollection services, Action<FrameworkOptions> configure = null)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            return new FrameworkBuilder(services, configure);
        }
    }
}