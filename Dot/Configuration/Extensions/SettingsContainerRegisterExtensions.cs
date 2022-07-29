using Cofoundry.Core.Configuration;
using Cofoundry.Core.DependencyInjection;
using System.Reflection;

namespace Dot.Configuration.Extensions
{
    public static class SettingsContainerRegisterExtensions
    {
        public static void Settings(this IContainerRegister container, Action<SettingsBuilder> builder)
        {
            var defaultBuilder = new SettingsBuilder(container);
            builder(defaultBuilder);
        }
    }

    public class SettingsBuilder
    {
        private readonly IContainerRegister _container;

        public SettingsBuilder(IContainerRegister container)
        {
            _container = container;
        }

        public IContainerRegister Register<TOptions>()
            where TOptions : class, IConfigurationSettings
        {
            var settingName = GetSettingsSectionName(typeof(TOptions).GetTypeInfo());
            var section = _container.ConfigurationHelper.Configuration.GetSection(settingName);

            _container.Configure<TOptions>(section);

            return _container;
        }

        private static string GetSettingsSectionName(TypeInfo settingsType)
        {
            const string SETTINGS_SUFFIX = "Settings";

            string name = settingsType.Name;

            if (name.EndsWith(SETTINGS_SUFFIX))
            {
                name = name.Remove(name.Length - SETTINGS_SUFFIX.Length);
            }

            var namespaceAttribute = settingsType
                .GetCustomAttributes(true)
                .Where(a => a is NamespacedConfigurationSettingAttribute)
                .Cast<NamespacedConfigurationSettingAttribute>()
                .FirstOrDefault();

            if (namespaceAttribute != null)
            {
                name = namespaceAttribute.Namespace + ":" + name;
            }

            return name;
        }
    }
}