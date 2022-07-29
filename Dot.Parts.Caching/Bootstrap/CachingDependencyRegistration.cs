using Cofoundry.Core.Caching.Internal;
using Cofoundry.Core.DependencyInjection;
using Dot.Caching.Services;
using Dot.Configuration.Extensions;

namespace Cofoundry.Core.Caching.Registration
{
    public class CachingDependencyRegistration : IDependencyRegistration
    {
        public void Register(IContainerRegister container)
        {
            container.Settings(x =>
            {
                x.Register<InMemoryObjectCacheSettings>();
            });

            var cacheMode = container.ConfigurationHelper.GetValue("Cofoundry:InMemoryObjectCache:CacheMode", InMemoryObjectCacheMode.Persitent);

            if (cacheMode == InMemoryObjectCacheMode.PerScope)
            {
                container.RegisterScoped<IObjectCacheFactory, InMemoryObjectCacheFactory>();
            }
            else
            {
                container.RegisterSingleton<IObjectCacheFactory, InMemoryObjectCacheFactory>();
            }
        }
    }
}
