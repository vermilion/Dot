using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PlatformFramework;
using PlatformFramework.EFCore;
using PlatformFramework.EFCore.Identity;
using PlatformFramework.EFCore.Identity.Models;
using PlatformFramework.Exceptions;
using System.Reflection;
using System.Text.Json.Serialization;

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
                    x.AddModule<ApplicationModule>();
                    x.AddModule<PlatformIdentityModule>();
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
                .AddEfCore<ProjectDbContext>(o =>
                {
                    o.EnableSensitiveDataLogging();

                    const string connectionString = "Server=localhost; Database=PlatformDb;User ID=postgres;Password=Qwerty12;";
                    o.UseNpgsql(connectionString, assembly => assembly.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));
                })
                .WithMigrationInitializer()
                .WithHooks(x =>
                {
                    x.WithTrackingHooks();
                    x.WithSoftDeletedEntityHook();
                });

            //services.AddProblemDetails();

            var jwtTokenConfig = Configuration.GetSection("JwtTokenConfig").Get<JwtTokenConfig>();

            services
                .AddJwtAuthorization()
                .AddJwtAuthentication(jwtTokenConfig)
                .AddPlatformIdentity<ProjectDbContext>(x =>
                {
                    x.Password.RequireDigit = false;
                    x.Password.RequiredLength = 4;
                    x.Password.RequireNonAlphanumeric = false;
                    x.Password.RequireUppercase = false;
                    x.Password.RequireLowercase = false;
                });

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
                .AddApiExplorer();

            // configure openapi
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "API ",
                    Description = "Swagger"
                });

                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter JWT Bearer token **_only_**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer", // must be lower case
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securityScheme, new string[] { } }
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

            app.UseAuthentication();
            app.UseAuthorization();

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
