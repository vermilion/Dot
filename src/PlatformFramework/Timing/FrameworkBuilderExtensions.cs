using Microsoft.Extensions.DependencyInjection;
using PlatformFramework.Interfaces.Timing;

namespace PlatformFramework.Timing
{
    public static class FrameworkBuilderExtensions
    {
        public static FrameworkBuilder WithClockService(this FrameworkBuilder builder)
        {
            var services = builder.Services;

            services.AddTransient<IClockProvider, ClockProvider>();

            return builder;
        }
    }
}