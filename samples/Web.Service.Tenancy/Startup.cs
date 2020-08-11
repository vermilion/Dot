using PlatformFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.ResponseCompression;
using System.Reflection;
using PlatformFramework.Web;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using PlatformFramework.Web.ExceptionHandling;
using PlatformFramework.Mapping;
using PlatformFramework.Tenancy;
using Finbuckle.MultiTenant;

namespace Web.Service
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddFramework(x =>
                {
                    x.Assemblies.Clear();
                    x.Assemblies.Add(Assembly.GetExecutingAssembly());
                })
                .WithMappers(x =>
                {
                    x.AddProfile<MyEntityMappingProfile>();
                })
                .WithDefaults();

            services
                .AddWebFramework()
                .WithPermissionAuthorization()
                .WithCors(options =>
                {
                    options.AddPolicy("Default", x => x
                        .AllowCredentials()
                        .SetIsOriginAllowed(isOriginAllowed: _ => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader());
                })
                .WithResponseCompression(options =>
                {
                    options.Providers.Add<BrotliCompressionProvider>();
                    options.EnableForHttps = true;
                });

            services
                .AddMultitenantEfCore<ProjectDbContext>()
                .WithMigrationInitializer()
                .WithHooks(x =>
                {
                    x.WithTrackingHooks();
                    x.WithSoftDeletedEntityHook();
                })
                .WithEntities(x =>
                {
                    x.ApplyConfiguration<MyEntity, MyEntityConfiguration>();
                });

            //services.AddProblemDetails();

            services
                .AddMvcCore(o =>
                {
                })
                .AddControllersAsServices()
                .AddJsonOptions(o =>
                {
                    o.JsonSerializerOptions.WriteIndented = true;
                    o.JsonSerializerOptions.IgnoreNullValues = true;
                    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                })
                .AddApiExplorer()
                .AddAuthorization();

            // configure openapi
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "API ",
                    Description = "Swagger"
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHttpsRedirection();
                app.UseHsts();
            }

            app.UseResponseCompression();

            app.UseCors("Default");

            //app.UseProblemDetails();
            app.UseExceptionHandler(err => err.UseCustomErrors(env));

            app.UseRouting();
            app.UseMultiTenant();

            //app.UseAuthentication();
            //app.UseAuthorization();

            app.UseHttpsRedirection();
            app.UseFileServer(new FileServerOptions
            {
                // Don't expose file system
                EnableDirectoryBrowsing = false
            });
            app.UseCookiePolicy();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            if (env.IsDevelopment())
            {
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
                    c.OAuthClientId("Swagger");
                    c.OAuthClientSecret("swagger");
                    c.OAuthAppName("Api");
                    c.OAuthUseBasicAuthenticationWithAccessCodeGrant();

                    c.DefaultModelsExpandDepth(-1);
                });

                app.UseSwagger();
            }

            app.UseFileServer(new FileServerOptions
            {
                RequestPath = new PathString("/admin"),
                FileProvider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly(), Assembly.GetExecutingAssembly().GetName().Name + ".web"),
                EnableDirectoryBrowsing = true
            });
        }
    }
}