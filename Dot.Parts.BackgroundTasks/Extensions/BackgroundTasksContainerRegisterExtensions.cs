using Cofoundry.Core.BackgroundTasks;
using Cofoundry.Core.DependencyInjection;

namespace Dot.Configuration.Extensions
{
    public static class BackgroundTasksContainerRegisterExtensions
    {
        public static IContainerRegister RegisterAsyncBackgroundTask<TTask>(this IContainerRegister container)
            where TTask : class, IAsyncBackgroundTask
        {
            container.Register<IAsyncBackgroundTask, TTask>();

            return container;
        }

        public static IContainerRegister RegisterBackgroundTaskRegistration<TTask>(this IContainerRegister container)
            where TTask : class, IBackgroundTaskRegistration
        {
            container.Register<IBackgroundTaskRegistration, TTask>();

            return container;
        }
    }
}