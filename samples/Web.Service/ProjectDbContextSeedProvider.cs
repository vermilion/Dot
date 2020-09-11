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
            var role = new Role
            {
                Name = "Administrator"
            };

            if (!await _roleManager.RoleExistsAsync(role.Name))
            {
                var roleResult = await _roleManager.CreateAsync(role);
            }

            var user = await _userManager.FindByNameAsync("admin");

            if (user == null)
            {
                var newUser = new User
                {
                    UserName = "admin"
                };

                var userResult = await _userManager.CreateAsync(newUser, "admin");
                user = await _userManager.FindByNameAsync("admin");
            }

            if (!await _userManager.IsInRoleAsync(user, role.Name))
            {
                var roleResult = await _userManager.AddToRoleAsync(user, role.Name);
            }
        }
    }
}
