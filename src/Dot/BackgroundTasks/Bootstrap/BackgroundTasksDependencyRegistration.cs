using Cofoundry.Core.DependencyInjection;

namespace Cofoundry.Core.BackgroundTasks.Registration
{
    public class BackgroundTasksDependencyRegistration : IDependencyRegistration
    {
        public void Register(IContainerRegister container)
        {
            container.RegisterAll<IAsyncRecurringBackgroundTask>();
            container.RegisterAll<IBackgroundTaskRegistration>();
        }
    }
}
