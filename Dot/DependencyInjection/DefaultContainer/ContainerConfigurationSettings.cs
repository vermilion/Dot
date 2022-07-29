using Microsoft.Extensions.Configuration;

namespace Cofoundry.Core.DependencyInjection
{
    /// <summary>
    /// Simple abstraction over configuration settings that gets exposed in
    /// an IContainerRegister instance.
    /// </summary>
    public class ContainerConfigurationHelper : IContainerConfigurationHelper
    {
        public IConfiguration Configuration { get; }

        public ContainerConfigurationHelper(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Extracts the value with the specified key and converts it to type T.
        /// </summary>
        /// <typeparam name="T">The type to convert the value to.</typeparam>
        /// <param name="key">The configuration key for the value to convert.</param>
        /// <returns>The type to convert the value to.</returns>
        public T GetValue<T>(string key)
        {
            return Configuration.GetValue<T>(key);
        }

        /// <summary>
        /// Extracts the value with the specified key and converts it to type T.
        /// </summary>
        /// <typeparam name="T">The type to convert the value to.</typeparam>
        /// <param name="key">The configuration key for the value to convert.</param>
        /// <param name="defaultValue">The default value to use if no value is found.</param>
        /// <returns>The type to convert the value to.</returns>
        public T GetValue<T>(string key, T defaultValue)
        {
            return Configuration.GetValue(key, defaultValue);
        }
    }
}
