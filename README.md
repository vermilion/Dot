# PlatformFramework ![GitHub Workflow Status](https://img.shields.io/github/workflow/status/vermilion/PlatformFramework/.NET%20Core?style=flat-square) ![GitHub code size in bytes](https://img.shields.io/github/languages/code-size/vermilion/PlatformFramework?style=flat-square)

| Package | Install |
| --- | --- |
| [PlatformFramework](https://www.nuget.org/packages/PlatformFramework) / An application framework for building applications on ASP.NET Core | ![Nuget](https://img.shields.io/nuget/v/PlatformFramework?logo=nuget&style=flat-square) |
| [PlatformFramework.Web](https://www.nuget.org/packages/PlatformFramework.Web) / A Web part of framework on top of the PlatformFramework | ![Nuget](https://img.shields.io/nuget/v/PlatformFramework.Web?logo=nuget&style=flat-square) |
| [PlatformFramework.EFCore](https://www.nuget.org/packages/PlatformFramework.EFCore) / A EntityFramework Core part of framework on top of the PlatformFramework | ![Nuget](https://img.shields.io/nuget/v/PlatformFramework.EFCore?logo=nuget&style=flat-square) |

## Technologies

* [.NET Core 3.1](https://dotnet.microsoft.com/download)
* [ASP.NET Core 3.1](https://docs.microsoft.com/en-us/aspnet/core)
* [Entity Framework Core 3.1](https://docs.microsoft.com/en-us/ef/core)
* [C# 8.0](https://docs.microsoft.com/en-us/dotnet/csharp)

## Installing

From the Package Manager Console:

    PM> Install-Package PlatformFramework
    PM> Install-Package PlatformFramework.Web
    PM> Install-Package PlatformFramework.EFCore

### Getting Started
#### PlatformFramework
- In your `Startup.cs`
```csharp
public void ConfigureServices(IServiceCollection services)
{
    services
        .AddFramework(x =>
        {
            x.Assemblies.Clear();
            x.Assemblies.Add(Assembly.GetExecutingAssembly());
            x.Assemblies.Add(typeof(ApplicationRegistry).Assembly);
        })
        .WithDefaults(); // add default framework services. This can be omitted and replaced by concrete extensions
```

#### PlatformFramework.Web
- In your `Startup.cs`
```csharp
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
```

#### PlatformFramework.EFCore
- In your `Startup.cs`
```csharp
services
    .AddEfCore<ProjectDbContext>(o => //configure DbContext and `IUnitOfWork`
    {
        var connectionString = "__CONNECTION_STRING__";
        o.UseNpgsql(connectionString, assembly => assembly.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));
    })
    .WithMigrationInitializer() // add DBContext migrator to startup
    .WithHooks(x => // add `on SaveChanges` entity hooks 
    {
        x.WithTrackingHooks();
        x.WithSoftDeletedEntityHook();
    })
    .WithEntities(x => // add Entities to DbContext
    {
        x.RegisterEntity<FederalProjectItem, FederalProjectItemCustomizer>();
    });
```

### Samples

- Download Visual Studio 2019 (any edition) from https://www.visualstudio.com/downloads/
- Open `PlatformFramework.sln` and wait for Visual Studio to restore all Nuget packages
- Samples are in to try out (navigate to /swagger for API test)

## Contributing
- Clone the repository using the command `git clone https://github.com/vermilion/PlatformFramework.git` and checkout the `master` branch.
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## Licences

Licenced under [MIT](LICENSE).
