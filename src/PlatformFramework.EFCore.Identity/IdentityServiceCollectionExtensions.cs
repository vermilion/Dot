using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PlatformFramework.EFCore.Identity.Entities;
using PlatformFramework.EFCore.Identity.Stores;
using System;

namespace PlatformFramework.EFCore.Identity
{
    public static class IdentityServiceCollectionExtensions
    {
        public static IdentityBuilder AddPlatformIdentity(this IServiceCollection services, Action<IdentityOptions>? setupAction = null)
        {
            var builder = services
                .AddIdentity<User, Role>(setupAction)
                .AddUserManager<UserManager<User>>()
                .AddDefaultTokenProviders();

            services.AddTransient<IUserStore<User>, UserStore>();
            services.AddTransient<IRoleStore<Role>, RoleStore>();

            return builder;
        }
    }
}
