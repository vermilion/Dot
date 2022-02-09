![image](https://user-images.githubusercontent.com/2167823/118759050-ed321200-b878-11eb-8c00-59e17cab63ad.png)

# Dot Framework ![GitHub Workflow Status](https://img.shields.io/github/workflow/status/vermilion/PlatformFramework/.NET%20Core?style=flat-square) ![GitHub code size in bytes](https://img.shields.io/github/languages/code-size/vermilion/PlatformFramework?style=flat-square)

[![Nuget](https://img.shields.io/nuget/v/PlatformFramework?logo=nuget&label=PlatformFramework&style=flat-square)](https://www.nuget.org/packages/PlatformFramework) An application framework for building applications on ASP.NET Core

[![Nuget](https://img.shields.io/nuget/v/PlatformFramework.EFCore?logo=nuget&label=PlatformFramework.EFCore&style=flat-square)](https://www.nuget.org/packages/PlatformFramework.EFCore) / A EntityFramework Core part of framework on top of the PlatformFramework

[![Nuget](https://img.shields.io/nuget/v/PlatformFramework.EFCore.Identity?logo=nuget&label=PlatformFramework.EFCore.Identity&style=flat-square)](https://www.nuget.org/packages/PlatformFramework.EFCore.Identity) / A Identity part of framework on top of the PlatformFramework.EFCore

## Technologies

* [.NET 6](https://dotnet.microsoft.com/download)
* [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core)
* [Entity Framework Core 6](https://docs.microsoft.com/en-us/ef/core)
* [C# 9.0](https://docs.microsoft.com/en-us/dotnet/csharp)

## Installing

From the Package Manager Console:

    PM> Install-Package Dot
    PM> Install-Package Dot.EFCore
    PM> Install-Package Dot.EFCore.Identity

### Getting Started
#### PlatformFramework
- In your `Startup.cs`
```csharp
public void ConfigureServices(IServiceCollection services)
{
    services
        .AddFramework(x =>
        {
            // add modules
            x.AddModule<ApplicationModule>();
            x.AddModule<PlatformIdentityModule>();
        });
    
    ....
}
```

#### PlatformFramework.EFCore
- In your `Startup.cs`
```csharp
public void ConfigureServices(IServiceCollection services)
{
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
            x.ApplyConfiguration<MyEntity, MyEntityConfiguration>();
        });
            
    ....
}
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
