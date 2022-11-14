using Cofoundry.Web;
using Cofoundry.Web.Admin;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;

namespace Cofoundry.BasicTestSite
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // the default config from the starer template requires consent
            // but CF uses an essental cookie for login and therefore should
            // continue to work.
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddFastEndpoints();
            services.AddAuthenticationJWTBearer("my_super_simple_jwt_bearer_key");

            services.AddCors(options =>
            {
                options.AddPolicy("Default", x => x
                       .AllowCredentials()
                       .SetIsOriginAllowed(isOriginAllowed: _ => true)
                       .AllowAnyMethod()
                       .AllowAnyHeader());
            });

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.EnableForHttps = true;
            });

            services
                .AddDot<AppStartup>(Configuration)               ;

            services
                .AddControllers()
                .AddDotUI()
                .AddJsonOptions(o =>
                {
                    o.JsonSerializerOptions.WriteIndented = true;
                    o.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            // configure openapi
            services.AddSwaggerDoc(c =>
            {
                c.Title = "API";
                c.Version = "v1";
                c.Description = "Swagger";
            }, tagIndex: 2, addJWTBearerAuth: false);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseOpenApi();
                app.UseSwaggerUi3(s => s.ConfigureDefaults());
            }
            else
            {
                app.UseHttpsRedirection();
                app.UseHsts();
            }

            app.UseResponseCompression();

            app.UseDefaultExceptionHandler();

            app.UseCors("Default");

            app.UseDot();
            app.UseDotUI();

            app.UseRouting();

            app.UseAuthentication(); //add this
            app.UseAuthorization();
            MainExtensions.UseFastEndpointsMiddleware(app);

            app.UseEndpoints(options =>
            {
                options.MapControllers();
                options.MapDefaultControllerRoute();

                options.MapFastEndpoints();
            });

            var assembly = typeof(BaseApiController).Assembly;
            app.UseFileServer(new FileServerOptions
            {
                RequestPath = new PathString("/admin"),
                FileProvider = new EmbeddedFileProvider(assembly, assembly.GetName().Name + ".ClientApp.dist"),
                EnableDirectoryBrowsing = true
            });
        }
    }
}
