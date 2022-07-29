using Cofoundry.Core.DependencyInjection;
using Dot.Configuration.Extensions;
using Dot.Time.Services;

namespace Cofoundry.Core.Time.Registration
{
    public class TimeDependencyRegistration : IDependencyRegistration
    {
        public void Register(IContainerRegister container)
        {
            container.Settings(x =>
            {
                x.Register<DateTimeSettings>();
            });

            container
                .RegisterSingleton<IDateTimeService, DateTimeService>()
                .RegisterScoped<ITimeZoneConverter, TimeZoneConverter>();
        }
    }
}
