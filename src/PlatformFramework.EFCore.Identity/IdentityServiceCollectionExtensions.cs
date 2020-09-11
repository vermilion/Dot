using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PlatformFramework.EFCore.Identity.Context;
using PlatformFramework.EFCore.Identity.Entities;
using PlatformFramework.EFCore.Identity.Stores;
using System;

namespace PlatformFramework.EFCore.Identity
{
    public static class IdentityServiceCollectionExtensions
    {
        public static IdentityBuilder AddPlatformIdentity<TDbContext>(this IServiceCollection services, Action<IdentityOptions>? setupAction = null)
            where TDbContext : IdentityDbContextCore
        {
            var builder = services
                .AddIdentity<User, Role>(setupAction)
                .AddUserManager<UserManager<User>>()
                .AddEntityFrameworkStores<TDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IUserStore<User>, UserStore<TDbContext>>();
            services.AddScoped<IRoleStore<Role>, RoleStore<TDbContext>>();

            return builder;
        }
    }
}
