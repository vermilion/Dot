using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.Reflection;

namespace Cofoundry.Web
{
    public static class UseCofoundryStartupExtension
    {
        /// <summary>
        /// Registers Cofoundry into the application pipeline and runs all the registered
        /// Cofoundry StartupTasks.
        /// </summary>
        /// <param name="app">Application configuration.</param>
        public static void UseDot(this IApplicationBuilder app)
        {
            using (var childContext = app.ApplicationServices.CreateScope())
            {
                var startupTasks = childContext
                    .ServiceProvider
                    .GetServices<IStartupConfigurationTask>();

                foreach (var startupTask in startupTasks)
                {
                    startupTask.Configure(app);
                }
            }
        }
    }
}