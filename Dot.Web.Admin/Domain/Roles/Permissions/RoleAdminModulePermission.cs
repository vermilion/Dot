﻿namespace Cofoundry.Domain
{
    public class RoleAdminModulePermission : IEntityPermission
    {
        public RoleAdminModulePermission()
        {
            EntityDefinition = new RoleEntityDefinition();
            PermissionType = CommonPermissionTypes.AdminModule("Roles");
        }

        public IEntityDefinition EntityDefinition { get; private set; }
        public PermissionType PermissionType { get; private set; }
    }
}
