using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.Reflection;

namespace Cofoundry.Web
{
    public static class UseCofoundryStartupExtension
    {
        public static IMvcBuilder AddDotUI(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder = EnsureCoreMVCServicesAdded(mvcBuilder);
            AddAdditionalTypes(mvcBuilder);

            return mvcBuilder;
        }


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

        /// <summary>
        /// Ensure the correct component parts required to run Cofoundry have been added
        /// </summary>
        private static IMvcBuilder EnsureCoreMVCServicesAdded(IMvcBuilder mvcBuilder)
        {
            return mvcBuilder.Services
                .AddControllersWithViews();
        }

        private static void AddAdditionalTypes(IMvcBuilder mvcBuilder)
        {
            // Ensure IHttpContextAccessor is added, because it isn't by default
            // see https://github.com/aspnet/Hosting/issues/793
            mvcBuilder.Services.AddHttpContextAccessor();
        }
    }
}