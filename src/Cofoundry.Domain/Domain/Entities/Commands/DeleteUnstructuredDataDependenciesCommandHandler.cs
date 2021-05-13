﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Cofoundry.Core;
using Cofoundry.Domain.CQS;
using Cofoundry.Domain.Data;

namespace Cofoundry.Domain.Internal
{
    public class DeleteUnstructuredDataDependenciesCommandHandler 
        : ICommandHandler<DeleteUnstructuredDataDependenciesCommand>
        , IPermissionRestrictedCommandHandler<DeleteUnstructuredDataDependenciesCommand>
    {
        #region constructor

        private readonly CofoundryDbContext _dbContext;
        private readonly IQueryExecutor _queryExecutor;
        private readonly IEntityDefinitionRepository _entityDefinitionRepository;
        private readonly IPermissionRepository _permissionRepository;

        public DeleteUnstructuredDataDependenciesCommandHandler(
            CofoundryDbContext dbContext,
            IQueryExecutor queryExecutor,
            IEntityDefinitionRepository entityDefinitionRepository,
            IPermissionRepository permissionRepository
            )
        {
            _dbContext = dbContext;
            _queryExecutor = queryExecutor;
            _entityDefinitionRepository = entityDefinitionRepository;
            _permissionRepository = permissionRepository;
        }

        #endregion

        #region Execute

        public async Task ExecuteAsync(DeleteUnstructuredDataDependenciesCommand command, IExecutionContext executionContext)
        {
            string entityName;

            var entityDefinition = _entityDefinitionRepository.GetByCode(command.RootEntityDefinitionCode);
            EntityNotFoundException.ThrowIfNull(entityDefinition, command.RootEntityDefinitionCode);
            entityName = entityDefinition.Name;

            var query = new GetEntityDependencySummaryByRelatedEntityQuery(command.RootEntityDefinitionCode, command.RootEntityId);
            var dependencies = await _queryExecutor.ExecuteAsync(query, executionContext);

            var requiredDependency = dependencies.FirstOrDefault(d => !d.CanDelete);
            if (requiredDependency != null)
            {
                throw new ValidationException(
                    string.Format("Cannot delete this {0} because {1} '{2}' has a dependency on it.",
                    entityName,
                    requiredDependency.Entity.EntityDefinitionName.ToLower(),
                    requiredDependency.Entity.RootEntityTitle));
            }

            /*await _entityFrameworkSqlExecutor
                .ExecuteCommandAsync(_dbContext,
                    "Cofoundry.UnstructuredDataDependency_Delete",
                    new SqlParameter("EntityDefinitionCode", command.RootEntityDefinitionCode),
                    new SqlParameter("EntityId", command.RootEntityId)
                    );*/
        }


        #endregion

        #region permissions

        public IEnumerable<IPermissionApplication> GetPermissions(DeleteUnstructuredDataDependenciesCommand command)
        {
            var entityDefinition = _entityDefinitionRepository.GetByCode(command.RootEntityDefinitionCode);
            EntityNotFoundException.ThrowIfNull(entityDefinition, command.RootEntityDefinitionCode);

            // Try and get a delete permission for the root entity.
            var permission = _permissionRepository.GetByEntityAndPermissionType(entityDefinition, CommonPermissionTypes.Delete("Entity"));

            if (permission != null)
            {
                yield return permission;
            }
        }

        #endregion
    }
}
