using Cofoundry.Core.DependencyInjection;
using Cofoundry.Web;

namespace Dot.Configuration.Extensions
{
    public static class StartupTasksContainerRegisterExtensions
    {
        public static void Startup(this IContainerRegister container, Action<StartupBuilder> builder)
        {
            var defaultBuilder = new StartupBuilder(container);
            builder(defaultBuilder);
        }
    }

    public class StartupBuilder
    {
        private readonly IContainerRegister _container;

        public StartupBuilder(IContainerRegister container)
        {
            _container = container;
        }

        public IContainerRegister RegisterConfigurationTask<TTask>()
            where TTask : class, IStartupConfigurationTask
        {
            _container.Register<IStartupConfigurationTask, TTask>();

            return _container;
        }

        public IContainerRegister RegisterServiceConfigurationTask<TTask>()
            where TTask : class, IStartupServiceConfigurationTask
        {
            _container.Register<IStartupServiceConfigurationTask, TTask>();

            return _container;
        }
    }
}