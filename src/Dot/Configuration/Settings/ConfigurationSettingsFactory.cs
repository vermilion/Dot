using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Cofoundry.Core.Configuration
{
    /// <summary>
    /// An InjectionFactory for allowing injection and validation of configuration settings
    /// without having to request IOptions directly.
    /// </summary>
    /// <typeparam name="TSettings">Type of settings object to instantiate</typeparam>
    public class ConfigurationSettingsFactory<TSettings> : IConfigurationSettingsFactory<TSettings> where TSettings : class, IConfigurationSettings, new()
    {
        #region constructor

        private readonly IServiceProvider _serviceProvider;

        public ConfigurationSettingsFactory(
            IServiceProvider serviceProvider
            )
        {
            _serviceProvider = serviceProvider;
        }

        #endregion

        #region public

        /// <summary>
        /// Creates an instance of a settings objects, extracting setting values from
        /// a configuration source.
        /// </summary>
        public TSettings Create()
        {
            var settingsOptions = _serviceProvider.GetRequiredService<IOptions<TSettings>>();
            var settings = settingsOptions.Value;

            //var errors = _modelValidationService.GetErrors(settings);

            //if (!EnumerableHelper.IsNullOrEmpty(errors))
            //{
            //    throw new InvalidConfigurationException(typeof(TSettings), errors);
            //}

            return settings;
        }

        #endregion
    }
}
