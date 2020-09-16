using Microsoft.AspNetCore.Identity;
using PlatformFramework.EFCore.Abstractions;
using PlatformFramework.EFCore.Identity.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Web.Service
{
    public class ProjectDbContextSeedProvider : IDbSeedProvider
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public ProjectDbContextSeedProvider(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed(CancellationToken cancellationToken = default)
        {
            if (!await _roleManager.RoleExistsAsync(IdentityConstants.AdminRole))
            {
                var role = new Role
                {
                    Name = IdentityConstants.AdminRole
                };

                var roleResult = await _roleManager.CreateAsync(role);
            }

            var user = await _userManager.FindByNameAsync(IdentityConstants.AdminName);

            if (user == null)
            {
                var newUser = new User
                {
                    UserName = IdentityConstants.AdminName
                };

                var userResult = await _userManager.CreateAsync(newUser, IdentityConstants.AdminName);
                user = await _userManager.FindByNameAsync(IdentityConstants.AdminName);
            }

            if (!await _userManager.IsInRoleAsync(user, IdentityConstants.AdminRole))
            {
                var roleResult = await _userManager.AddToRoleAsync(user, IdentityConstants.AdminRole);
            }
        }
    }

    public static class IdentityConstants
    {
        public const string AdminName = "admin";
        public const string AdminRole = "Administrator";
    }
}
