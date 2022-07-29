using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Cofoundry.Core.DependencyInjection
{
    /// <summary>
    /// Default implementation of IContainerRegister that uses the basic
    /// DI utilities provided by Microsoft.Extensions.DependencyInjection
    /// </summary>
    public class DefaultContainerRegister : IContainerRegister
    {
        private static readonly InstanceLifetime DEFAULT_LIFETIME = InstanceLifetime.Transient;

        private readonly IServiceCollection _serviceCollection;
        private readonly DefaultContainerBuilder _containerBuilder;
        private readonly ContainerConfigurationHelper _containerConfigurationHelper;

        public DefaultContainerRegister(
            IServiceCollection serviceCollection,
            DefaultContainerBuilder containerBuilder,
            IConfiguration configuration
            )
        {
            if (serviceCollection == null) throw new ArgumentNullException(nameof(serviceCollection));
            if (containerBuilder == null) throw new ArgumentNullException(nameof(containerBuilder));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            _serviceCollection = serviceCollection;
            _containerBuilder = containerBuilder;
            _containerConfigurationHelper = new ContainerConfigurationHelper(configuration);
        }

        #region IContainerRegister implementation

        public IContainerConfigurationHelper ConfigurationHelper { get => _containerConfigurationHelper; }

        public IContainerRegister Configure<TOptions>(IConfigurationSection section)
             where TOptions : class
        {
            //_serviceCollection.Configure<TOptions>(section);
            _serviceCollection.AddOptions<TOptions>()
                .Bind(section)
                .ValidateDataAnnotations()
                .ValidateOnStart();

            return this;
        }

        public IContainerRegister RegisterSingleton<TRegisterAs>(TRegisterAs instance, RegistrationOptions options = null)
        {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            var typeToRegister = typeof(TRegisterAs);

            var fn = new Action(() =>
            {
                _serviceCollection.AddSingleton(typeToRegister, instance);
            });

            RegisterWithContainer<TRegisterAs>(fn, options);

            return this;
        }

        public IContainerRegister Register<TConcrete>(RegistrationOptions options = null)
        {
            return Register<TConcrete, TConcrete>(options);
        }

        public IContainerRegister Register<TRegisterAs, TConcrete>(RegistrationOptions options = null)
            where TConcrete : TRegisterAs
        {
            var fn = new Action(() =>
            {
                AddService(typeof(TRegisterAs), typeof(TConcrete), options);
            });

            RegisterWithContainer<TRegisterAs>(fn, options);

            return this;
        }

        public IContainerRegister Register<TConcrete>(ICollection<Type> types, RegistrationOptions options = null)
        {
            var fn = new Action(() =>
            {
                var concreteType = typeof(TConcrete);

                AddService(concreteType, concreteType, options);

                foreach (var type in types.Where(t => t != concreteType))
                {
                    AddServiceWithFactory(type, x => x.GetService<TConcrete>(), options);
                }
            });

            RegisterWithContainer<TConcrete>(fn, options);

            return this;
        }

        public IContainerRegister RegisterFactory<TToRegister, TFactory>(RegistrationOptions options = null)
            where TFactory : IInjectionFactory<TToRegister>
        {
            return RegisterFactory<TToRegister, TToRegister, TFactory>(options);
        }

        public IContainerRegister RegisterFactory<TRegisterAs, TConcrete, TFactory>(RegistrationOptions options = null)
            where TFactory : IInjectionFactory<TConcrete>
            where TConcrete : TRegisterAs
        {
            var fn = new Action(() =>
            {
                var factoryType = typeof(TFactory);
                // If the factory is a concrete type, we should make sure it is registered
                if (!factoryType.GetTypeInfo().IsInterface)
                {
                    // Note that the factory is registered with the same options
                    AddService(factoryType, factoryType, options);
                }

                AddServiceWithFactory(typeof(TRegisterAs), c => c.GetRequiredService<TFactory>().Create(), options);
            });

            RegisterWithContainer<TRegisterAs>(fn, options);

            return this;
        }

        public IContainerRegister RegisterGeneric(Type registerAs, Type typeToRegister, RegistrationOptions options = null)
        {
            AddService(registerAs, typeToRegister, options);

            return this;
        }

        #endregion

        #region helpers

        private void RegisterWithContainer<TTo>(Action register, RegistrationOptions options = null)
        {
            if (options != null && options.ReplaceExisting)
            {
                _containerBuilder.QueueRegistration<TTo>(register, options.RegistrationOverridePriority);
            }
            else
            {
                register();
            }
        }

        private void AddService(Type serviceType, Type implementationType, RegistrationOptions options = null)
        {
            var lifetime = ConvertToServiceLifetime(options);

            var descriptor = new ServiceDescriptor(serviceType, implementationType, lifetime);
            _serviceCollection.Add(descriptor);
        }

        private void AddServiceWithFactory(
           Type serviceType,
           Func<IServiceProvider, object> implementationFactory,
           RegistrationOptions options = null
            )
        {
            var lifetime = ConvertToServiceLifetime(options);

            var descriptor = new ServiceDescriptor(serviceType, implementationFactory, lifetime);
            _serviceCollection.Add(descriptor);
        }

        private static ServiceLifetime ConvertToServiceLifetime(RegistrationOptions options)
        {
            var scope = options?.Lifetime ?? DEFAULT_LIFETIME;

            if (scope == null)
            {
                scope = DEFAULT_LIFETIME;
            }

            if (scope == InstanceLifetime.Scoped)
            {
                return ServiceLifetime.Scoped;
            }

            if (scope == InstanceLifetime.Transient)
            {
                return ServiceLifetime.Transient;
            }

            if (scope == InstanceLifetime.Singleton)
            {
                return ServiceLifetime.Singleton;
            }

            throw new ArgumentException("InstanceScope '" + scope.GetType().FullName + "' not recognised");
        }

        #endregion
    }
}
