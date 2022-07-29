using System;
using System.Collections.Generic;
using System.Linq;

namespace Cofoundry.Domain.Internal
{
    public class PermissionRepository : IPermissionRepository
    {
        #region constructor

        private readonly IDictionary<string, IPermission> _permissions;

        public PermissionRepository(
            IEnumerable<IPermission> allPermissions
            )
        {
            DetectDuplicates(allPermissions);
            _permissions = allPermissions.ToDictionary(k => GetUniqueKey(k));
        }

        private void DetectDuplicates(IEnumerable<IPermission> permissions)
        {
            var dulpicateCodes = permissions
                .GroupBy(e => GetUniqueKey(e))
                .Where(g => g.Count() > 1);

            if (dulpicateCodes.Any())
            {
                throw new Exception("Duplicate IPermission.CustomEntityDefinitionCode: " + dulpicateCodes.First().Key);
            }
        }

        #endregion

        #region public

        public IPermission? GetByCode(string permissionTypeCode, string entityDefinitionCode)
        {
            var key = CreateUniqueToken(permissionTypeCode, entityDefinitionCode);
            return _permissions.TryGetValue(key, out var value) ? value : default;
        }

        public IEnumerable<IPermission> GetAll()
        {
            return _permissions.Select(p => p.Value);
        }

        public IPermission? GetByEntityAndPermissionType(IEntityDefinition entityDefinition, PermissionType permissionType)
        {
            if (entityDefinition == null || permissionType == null) return null;
            return GetByEntityAndPermissionType(entityDefinition.EntityDefinitionCode, permissionType.Code);
        }

        public IPermission? GetByEntityAndPermissionType(string entityDefinitionCode, string permissionTypeCode)
        {
            if (string.IsNullOrEmpty(entityDefinitionCode) || string.IsNullOrEmpty(permissionTypeCode)) return null;
            var key = CreateUniqueToken(permissionTypeCode, entityDefinitionCode);
            return _permissions.TryGetValue(key, out var value) ? value : default;
        }

        #endregion

        #region helpers

        private string? GetUniqueKey(IPermission permission)
        {
            if (permission == null) return null;

            if (permission is IEntityPermission)
            {
                return CreateUniqueToken(permission.PermissionType, ((IEntityPermission)permission).EntityDefinition);
            }

            return CreateUniqueToken(permission.PermissionType);
        }

        private string? CreateUniqueToken(PermissionType permissionType, IEntityDefinition? definition = null)
        {
            if (permissionType == null || string.IsNullOrWhiteSpace(permissionType.Code)) return null;

            if (definition == null) return CreateUniqueToken(permissionType.Code);
            return CreateUniqueToken(permissionType.Code, definition.EntityDefinitionCode);
        }

        private string CreateUniqueToken(string permissionTypeCode, string? entityDefinitionCode = null)
        {
            if (string.IsNullOrWhiteSpace(entityDefinitionCode)) return permissionTypeCode.ToUpperInvariant();
            return (permissionTypeCode + entityDefinitionCode).ToUpperInvariant();
        }

        #endregion
    }
}
