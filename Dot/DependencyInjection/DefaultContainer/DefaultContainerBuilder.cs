using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cofoundry.Core.DependencyInjection
{
    /// <summary>
    /// Default implementation of IContainerBuilder that uses the basic
    /// DI utilities provided by Microsoft.Extensions.DependencyInjection
    /// </summary>
    public class DefaultContainerBuilder : IContainerBuilder
    {
        private bool isBuilt = false;

        #region constructor

        private readonly Dictionary<Type, RegistrationOverride> RegistrationOverrides = new();
        private readonly IServiceCollection _serviceCollection;
        private readonly IConfiguration _configurationRoot;

        public DefaultContainerBuilder(
            IServiceCollection serviceCollection,
            IConfiguration configurationRoot
            )
        {
            _serviceCollection = serviceCollection;
            _configurationRoot = configurationRoot;
        }

        #endregion

        #region public methods

        public void Build(IEnumerable<IDependencyRegistration> registrations)
        {
            CheckIsBuilt();

            var containerRegister = new DefaultContainerRegister(
                _serviceCollection, 
                this,
                _configurationRoot
                );

            foreach (var registration in registrations)
            {
                registration.Register(containerRegister);
            }

            BuildOverrides();
        }

        internal void QueueRegistration<TTo>(Action registration, int priority)
        {
            var typeToRegister = typeof(TTo);
            if (RegistrationOverrides.ContainsKey(typeToRegister))
            {
                var existingOverride = RegistrationOverrides[typeToRegister];

                // Don't allow the registrations with the same priority, but do 
                // replace lower priority registrations
                if (existingOverride.Priority == priority)
                {
                    var errorMessage = $"Type {typeToRegister.FullName} is already registered as an override with the specified priority value ({priority}). Multiple overrides with the same priority values are not supported.";
                    throw new InvalidTypeRegistrationException(typeToRegister, errorMessage);
                }
                else if (existingOverride.Priority < priority)
                {
                    existingOverride.Registration = registration;
                }
            }
            else
            {
                RegistrationOverrides.Add(typeToRegister, new RegistrationOverride(registration, priority));
            }
        }

        #endregion

        #region private helpers

        private struct RegistrationOverride
        {
            public RegistrationOverride(Action registration, int priority)
            {
                Registration = registration;
                Priority = priority;
            }

            public Action Registration;
            public int Priority;
        }

        private void CheckIsBuilt()
        {
            if (isBuilt)
            {
                throw new InvalidOperationException("The container has already been built.");
            }
            isBuilt = true;
        }

        private void BuildOverrides()
        {
            foreach (var registrationOverride in RegistrationOverrides)
            {
                registrationOverride.Value.Registration();
            }
        }

        #endregion
    }
}
