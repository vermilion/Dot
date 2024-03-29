﻿namespace Cofoundry.Domain
{
    public class CofoundryUserCreatePermission : IEntityPermission
    {
        public CofoundryUserCreatePermission()
        {
            EntityDefinition = new UserEntityDefinition();
            PermissionType = CommonPermissionTypes.Create("Cofoundry Users");
        }

        public IEntityDefinition EntityDefinition { get; private set; }
        public PermissionType PermissionType { get; private set; }
    }
}
