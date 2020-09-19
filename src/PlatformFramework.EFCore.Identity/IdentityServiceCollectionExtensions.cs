using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PlatformFramework.EFCore.Identity.Abstrations;
using PlatformFramework.EFCore.Identity.Context;
using PlatformFramework.EFCore.Identity.Entities;
using PlatformFramework.EFCore.Identity.Models;
using PlatformFramework.EFCore.Identity.Services;
using System;
using System.Text;

namespace PlatformFramework.EFCore.Identity
{
    public static class IdentityServiceCollectionExtensions
    {
        /// <summary>
        /// Adds JWT authentication default config
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/></param>
        /// <param name="jwtTokenConfig">Token configuration <see cref="JwtTokenConfig"/></param>
        /// <param name="setupAction">Configure action for config <see cref="TokenValidationParameters"/></param>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, JwtTokenConfig jwtTokenConfig, Action<TokenValidationParameters>? setupAction = null)
        {
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtTokenConfig.Issuer,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtTokenConfig.Secret)),
                ValidAudience = jwtTokenConfig.Audience,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(1)
            };

            setupAction?.Invoke(parameters);

            services
                .AddSingleton(jwtTokenConfig)
                .AddAuthentication(x =>
                {
                    x.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = true;
                    x.SaveToken = true;
                    x.TokenValidationParameters = parameters;
                });

            services.AddSingleton<IJwtAuthService, JwtAuthService>();
            services.AddHostedService<JwtRefreshTokenCacheService>();

            return services;
        }

        public static IServiceCollection AddJwtAuthorization(this IServiceCollection services)
        {
            services
                 .AddAuthorization(options =>
                 {
                     options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                        .RequireAuthenticatedUser()
                        .Build();
                 });

            return services;
        }

        public static IdentityBuilder AddPlatformIdentity<TDbContext>(this IServiceCollection services, Action<IdentityOptions>? setupAction = null)
            where TDbContext : IdentityDbContextCore
        {
            var builder = services
                .AddIdentity<User, Role>(setupAction)
                .AddUserManager<UserManager<User>>()
                .AddEntityFrameworkStores<TDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IUserStore<User>, UserStore<User, Role, TDbContext, int, UserClaim, UserRole, UserLogin, UserToken, RoleClaim>>();
            services.AddScoped<IRoleStore<Role>, RoleStore<Role, TDbContext, int, UserRole, RoleClaim>>();

            return builder;
        }
    }
}
