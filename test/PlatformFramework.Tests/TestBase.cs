using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace PlatformFramework.Tests
{
    public class TestBase
    {
        protected (IServiceCollection, TestRunContext<T>) PrepareDefaults<T>()
             where T : struct, IConvertible
        {
            var services = new ServiceCollection();
            services.AddLogging();

            var counter = new TestRunContext<T>();
            services.AddSingleton(counter);

            return (services, counter);
        }

        public class TestRunContext<T> 
            where T : struct, IConvertible
        {
            public ConcurrentDictionary<T, int> Data = new ConcurrentDictionary<T, int>();

            public void Set(T key)
            {
                Data.AddOrUpdate(key, number =>
                {
                    return 1;
                },
                (key, number) =>
                {
                    return number + 1;
                });
            }

            public int Get(T key)
            {
                return Data.GetValueOrDefault(key);
            }
        }
    }
}
