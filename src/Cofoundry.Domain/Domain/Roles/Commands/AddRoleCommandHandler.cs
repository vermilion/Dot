﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Cofoundry.Core;
using Cofoundry.Core.Validation;
using Cofoundry.Domain.CQS;
using Cofoundry.Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace Cofoundry.Domain.Internal
{
    /// <summary>
    /// Adds a new role to a user area with a set of permissions.
    /// </summary>
    public class AddRoleCommandHandler 
        : IRequestHandler<AddRoleCommand, AddRoleCommandResult>
        , IPermissionRestrictedRequestHandler<AddRoleCommand>
    {
        #region constructor

        private readonly CofoundryDbContext _dbContext;
        private readonly IMediator _mediator;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IPermissionValidationService _permissionValidationService;

        public AddRoleCommandHandler(
            CofoundryDbContext dbContext,
            IMediator mediator,
            IPermissionRepository permissionRepository,
            IPermissionValidationService permissionValidationService
            )
        {
            _dbContext = dbContext;
            _mediator = mediator;
            _permissionRepository = permissionRepository;
            _permissionValidationService = permissionValidationService;
        }

        #endregion

        #region execution

        public async Task<AddRoleCommandResult> ExecuteAsync(AddRoleCommand command, IExecutionContext executionContext)
        {
            ValidatePermissions(command);
            var isUnique = await _mediator.ExecuteAsync(GetUniqueQuery(command), executionContext);
            ValidateIsUnique(isUnique);

            var permissions = await GetCommandPermissionsAsync(command, executionContext);
            var role = MapAndAddRole(command, executionContext, permissions);
            await _dbContext.SaveChangesAsync();

            return new AddRoleCommandResult { RoleId = role.RoleId };
        }

        #endregion

        #region helpers

        private Role MapAndAddRole(AddRoleCommand command, IExecutionContext executionContext, List<Permission> permissions)
        {
            var role = new Role
            {
                Title = command.Title.Trim()
            };

            foreach (var permission in EnumerableHelper.Enumerate(permissions))
            {
                var rolePermission = new RolePermission
                {
                    Permission = permission
                };
                role.RolePermissions.Add(rolePermission);
            }
            
            _dbContext.Roles.Add(role);
            return role;
        }

        private void ValidatePermissions(AddRoleCommand command)
        {
            if (!EnumerableHelper.IsNullOrEmpty(command.Permissions))
            {
                var entityWithoutReadPermission = command.Permissions
                    .Where(p => !string.IsNullOrEmpty(p.EntityDefinitionCode))
                    .GroupBy(p => p.EntityDefinitionCode)
                    .Where(g => !g.Any(p => p.PermissionCode == CommonPermissionTypes.ReadPermissionCode));

                foreach (var entity in entityWithoutReadPermission)
                {
                    var entityCode = entity.First().EntityDefinitionCode;
                    var readPermission = _permissionRepository.GetByEntityAndPermissionType(entityCode, CommonPermissionTypes.ReadPermissionCode);

                    if (readPermission != null)
                    {
                        var msg = "Read permissions must be granted to entity " + entityCode + " in order to assign additional permissions";
                        throw new ValidationException(msg);
                    }
                }
            }
        }

        private void ValidateIsUnique(bool isUnique)
        {
            if (!isUnique)
            {
                throw ValidationErrorException.CreateWithProperties("A role with this title already exists", nameof(Role.Title));
            }
        }

        private IsRoleTitleUniqueQuery GetUniqueQuery(AddRoleCommand command)
        {
            return new IsRoleTitleUniqueQuery()
            {
                Title = command.Title.Trim(),
            };
        }

        private async Task<List<Permission>> GetCommandPermissionsAsync(AddRoleCommand command, IExecutionContext executionContext)
        {
            var permissions = new List<Permission>();

            if (!EnumerableHelper.IsNullOrEmpty(command.Permissions))
            {
                var dbPermissions = await _dbContext
                    .Permissions
                    .ToListAsync();

                foreach (var permissionCommand in command.Permissions)
                {
                    var dbPermission = dbPermissions
                        .SingleOrDefault(p => 
                            p.PermissionCode == permissionCommand.PermissionCode 
                            && (string.IsNullOrWhiteSpace(permissionCommand.EntityDefinitionCode) || p.EntityDefinitionCode == permissionCommand.EntityDefinitionCode)
                            );
                    // Because permissions are defined in code, it might not exists yet. If it doesn't lets add it.
                    if (dbPermission == null)
                    {

                        var codePermission = _permissionRepository.GetByCode(permissionCommand.PermissionCode, permissionCommand.EntityDefinitionCode);
                        dbPermission = new Permission();
                        dbPermission.PermissionCode = codePermission.PermissionType.Code;

                        if (codePermission is IEntityPermission)
                        {
                            var definitionCode = ((IEntityPermission)codePermission).EntityDefinition.EntityDefinitionCode;
                            await _mediator.ExecuteAsync(new EnsureEntityDefinitionExistsCommand(definitionCode), executionContext);
                            dbPermission.EntityDefinitionCode = definitionCode;
                        }
                    }

                    permissions.Add(dbPermission);
                }
            }

            return permissions;
        }

        #endregion
        
        #region Permission

        public IEnumerable<IPermissionApplication> GetPermissions(AddRoleCommand command)
        {
            yield return new RoleCreatePermission();
        }

        #endregion
    }
}
