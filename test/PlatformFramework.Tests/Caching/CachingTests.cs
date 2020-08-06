using Microsoft.Extensions.DependencyInjection;
using PlatformFramework.Caching;
using PlatformFramework.Interfaces.Caching;
using System;
using System.Threading.Tasks;
using Xunit;

namespace PlatformFramework.Tests.Caching
{
    public class CachingTests
    {
        [Fact]
        public async Task Should_Allow_Add_To_Cache()
        {
            var services = new ServiceCollection();
            services
                .AddFramework()
                .WithCaching();

            var cacheService = services.BuildServiceProvider().GetService<ICacheService>();

            Assert.NotNull(cacheService);

            var id = Guid.NewGuid();
            var model1 = await cacheService.GetOrAdd("test-key", () => Task.FromResult(new TestModel(id)), TimeSpan.FromSeconds(60));

            Assert.NotNull(model1);
            Assert.Equal(model1.Id, id);

            var model2 = await cacheService.GetOrAdd("test-key", () => Task.FromResult(new TestModel(Guid.NewGuid())), TimeSpan.FromSeconds(60));

            Assert.NotNull(model2);
            Assert.Equal(model2.Id, id);
        }

        [Fact]
        public async Task Should_Clear_Cache()
        {
            var services = new ServiceCollection();
            services
                .AddFramework()
                .WithCaching();

            var cacheService = services.BuildServiceProvider().GetService<ICacheService>();

            Assert.NotNull(cacheService);

            var id = Guid.NewGuid();

            await cacheService.Add("test-key", new TestModel(id), TimeSpan.FromSeconds(60));
            var model1 = await cacheService.Get<TestModel>("test-key");

            Assert.NotNull(model1);
            Assert.Equal(model1.Id, id);

            await cacheService.Remove("test-key");

            var model2 = await cacheService.Get<TestModel>("test-key");

            Assert.Null(model2);
        }

        public class TestModel
        {
            public TestModel(Guid id)
            {
                Id = id;
            }

            public Guid Id { get; }
        }
    }
}
