using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using PlatformFramework.Shared.Common;
using PlatformFramework.Shared.Extensions;

namespace PlatformFramework.Web.Authorization
{
    public class AuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        private readonly ThreadSafeDictionary<string, AuthorizationPolicy> _policies =
            new ThreadSafeDictionary<string, AuthorizationPolicy>(StringComparer.OrdinalIgnoreCase);

        public AuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
            : base(options)
        {
        }

        public override async Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (!policyName.StartsWith(PermissionConstant.PolicyPrefix, StringComparison.OrdinalIgnoreCase))
            {
                return await base.GetPolicyAsync(policyName);
            }

            var policy = _policies.GetOrAdd(policyName, name =>
            {
                var permissions = policyName
                    .Substring(PermissionConstant.PolicyPrefix.Length)
                    .Split(new[] { PermissionConstant.PolicyNameSplitSymbol }, StringSplitOptions.None);

                return new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .AddRequirements(new PermissionAuthorizationRequirement(permissions))
                    .Build();
            });

            return policy;
        }
    }
}