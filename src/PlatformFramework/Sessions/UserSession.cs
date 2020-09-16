using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Http;
using PlatformFramework.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace PlatformFramework.Sessions
{
    internal sealed class UserSession : IUserSession
    {
        private readonly IHttpContextAccessor _context;

        public UserSession(IHttpContextAccessor context)
        {
            _context = Guard.Against.Null(context, nameof(context));
        }

        private HttpContext? HttpContext => _context.HttpContext;
        private ClaimsPrincipal? Principal => HttpContext?.User;

        public bool IsAuthenticated => Principal?.Identity?.IsAuthenticated ?? false;
        public string? UserId => Principal?.FindFirstValue(ClaimTypes.NameIdentifier);
        public string? UserName => Principal?.FindFirstValue(ClaimTypes.Name);
        
        public IReadOnlyList<string>? Roles => Principal?.Claims
                .Where(c => c.Type.Equals(ClaimTypes.Role, StringComparison.OrdinalIgnoreCase))
                .Select(c => c.Value).ToList();

        public IReadOnlyList<Claim>? Claims => Principal?.Claims.ToList();
    }
}
