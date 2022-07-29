using Cofoundry.Core;
using Cofoundry.Domain.Data;
using Dot.EFCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Cofoundry.Domain.Internal
{
    /// <summary>
    /// Internal repository for fetching roles which bypasses CQS and permissions infrastructure
    /// to avoid circular references. Not to be used outside of Cofoundry core projects.
    /// </summary>
    /// <remarks>
    /// Not actually marked internal due to internal visibility restrictions and dependency injection
    /// </remarks>
    public class InternalRoleRepository : IInternalRoleRepository
    {
        #region constructor

        private readonly IRoleCache _roleCache;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoleDetailsMapper _roleMappingHelper;

        public InternalRoleRepository(
            IRoleCache roleCache,
            IUnitOfWork dbContext,
            IRoleDetailsMapper roleMappingHelper
            )
        {
            _roleCache = roleCache;
            _unitOfWork = dbContext;
            _roleMappingHelper = roleMappingHelper;
        }

        #endregion

        #region public methods

        public async Task<RoleDetails> GetByIdAsync(int? roleId)
        {
            if (roleId == null || roleId < 1) return await GetAnonymousRoleAsync();

            var cachedRole = await _roleCache.GetOrAddAsync(roleId.Value, async () =>
            {
                var dbRole = await QueryRoleById(roleId.Value).FirstOrDefaultAsync();
                var result = _roleMappingHelper.Map(dbRole);

                return result;
            });

            if (cachedRole == null) return await GetAnonymousRoleAsync();
            return cachedRole;
        }

        #endregion

        #region private helpers

        private Task<RoleDetails> GetAnonymousRoleAsync()
        {
            return _roleCache.GetOrAddAnonymousRoleAsync(async () =>
            {
                var dbRole = await QueryAnonymousRole().FirstOrDefaultAsync();
                EntityNotFoundException.ThrowIfNull(dbRole, AnonymousRole.AnonymousRoleCode);
                var role = _roleMappingHelper.Map(dbRole);

                return role;
            });
        }

        private IQueryable<Role> QueryAnonymousRole()
        {
            return _unitOfWork
                    .Roles()
                    .AsNoTracking()
                    .Include(r => r.RolePermissions)
                    .ThenInclude(p => p.Permission)
                    .Where(r => r.RoleCode == AnonymousRole.AnonymousRoleCode);

        }

        private IQueryable<Role> QueryRoleById(int roleId)
        {
            return _unitOfWork
                    .Roles()
                    .AsNoTracking()
                    .Include(r => r.RolePermissions)
                    .ThenInclude(p => p.Permission)
                    .Where(r => r.RoleId == roleId);
        }

        #endregion
    }
}
