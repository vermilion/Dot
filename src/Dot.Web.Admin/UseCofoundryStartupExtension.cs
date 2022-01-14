using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
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
        /// <param name="path"></param>
        public static void UseDotUI(this IApplicationBuilder app, string path = "administration")
        {
            var assembly = Assembly.GetExecutingAssembly();
            app.UseFileServer(new FileServerOptions
            {
                RequestPath = new PathString("/" + path),
                FileProvider = new EmbeddedFileProvider(assembly, assembly.GetName().Name + ".ClientApp.output.administration"),
                EnableDirectoryBrowsing = true
            });

            app.UseFileServer(new FileServerOptions
            {
                RequestPath = new PathString("/setup"),
                FileProvider = new EmbeddedFileProvider(assembly, assembly.GetName().Name + ".ClientApp.output.administration"),
                EnableDirectoryBrowsing = true
            });
        }
    }
}