using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Cofoundry.Web
{
    public static class UseCofoundryStartupExtension
    {
        /// <summary>
        /// Registers Cofoundry into the application pipeline and runs all the registered
        /// Cofoundry StartupTasks.
        /// </summary>
        /// <param name="application">Application configuration.</param>
        public static void UseCofoundry(this IApplicationBuilder application)
        {
            using (var childContext = application.ApplicationServices.CreateScope())
            {
                var startupTasks = childContext
                    .ServiceProvider
                    .GetServices<IStartupConfigurationTask>();

                foreach (var startupTask in startupTasks)
                {
                    startupTask.Configure(application);
                }
            }
        }
    }
}