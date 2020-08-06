using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using PlatformFramework.Abstractions;
using PlatformFramework.Web.Http;
using PlatformFramework.Web.Extensions;
using Ardalis.GuardClauses;

namespace PlatformFramework.Web.Runtime
{
    internal sealed class UserSession : IUserSession
    {
        private readonly IHttpContextAccessor _context;

        public UserSession(IHttpContextAccessor context)
        {
            _context = Guard.Against.Null(context, nameof(context));
        }

        private HttpContext HttpContext => _context.HttpContext;
        private ClaimsPrincipal? Principal => HttpContext?.User;
        
        public bool IsAuthenticated => Principal?.Identity?.IsAuthenticated ?? false;
        public string? UserId => Principal?.FindUserId();
        public string? UserName => Principal?.FindUserName();
        public IReadOnlyList<string>? Permissions => Principal?.FindPermissions();
        public IReadOnlyList<string>? Roles => Principal?.FindRoles();
        public IReadOnlyList<Claim>? Claims => Principal?.Claims.ToList();

        public string? UserDisplayName => Principal?.FindUserDisplayName();
        public string? UserBrowserName => HttpContext?.FindUserAgent();
        public string? UserIp => HttpContext?.FindUserIP();
        public string? ImpersonatorUserId => Principal?.FindImpersonatorUserId();

        public bool IsInRole(string role)
        {
            ThrowIfUnauthenticated();

            return Principal!.IsInRole(role);
        }

        public bool IsGranted(string permission)
        {
            ThrowIfUnauthenticated();

            return Principal!.HasPermission(permission);
        }

        private void ThrowIfUnauthenticated()
        {
            if (!IsAuthenticated) 
                throw new InvalidOperationException("This operation need user authenticated");
        }
    }
}