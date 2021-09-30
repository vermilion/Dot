using Cofoundry.Domain;
using Cofoundry.Domain.CQS;
using Cofoundry.Web;
using Cofoundry.Web.Admin;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection;
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
                .AddControllers()
                .AddDot<AppStartup>(Configuration)
                .AddJsonOptions(o =>
                {
                    o.JsonSerializerOptions.WriteIndented = true;
                    o.JsonSerializerOptions.IgnoreNullValues = true;
                    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            // configure openapi
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "API ",
                    Description = "Swagger"
                });

                /*var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Cookie Authentication",
                    Description = "Enter Cookie contents **_only_**",
                    In = ParameterLocation.Cookie,
                    Type = SecuritySchemeType.Http,
                    Scheme = CookieAuthenticationDefaults.AuthenticationScheme, // must be lower case
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = CookieAuthenticationDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securityScheme, System.Array.Empty<string>() }
                });*/
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

            app.UseExceptionHandler(err => err.UseCustomErrors(env));

            app.UseCors("Default");

            app.UseCookiePolicy();

            app.UseDot();

            app.UseMiddleware<IsSetupMiddleware>();

            app.UseDotUI();

            if (env.IsDevelopment())
            {
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
                    c.DefaultModelsExpandDepth(-1);
                });

                app.UseSwagger();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(options =>
            {
                options.MapControllers();
                options.MapDefaultControllerRoute();
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
