﻿using Cofoundry.Core.AutoUpdate;
using Dot.EFCore.Transactions.Services.Interfaces;
using System.Text;

namespace Cofoundry.Domain.Installation
{
    /// <summary>
    /// This import job is to add all the permissions defined in code in
    /// a single batch job during first startup, reducing the burden of 
    /// doing this individually as roles are created
    /// </summary>
    public class ImportPermissionsCommandHandler : IVersionedUpdateCommandHandler<ImportPermissionsCommand>
    {
        private readonly ITransactionScopeManager _transactionScopeManager;
        private readonly IPermissionRepository _permissionRepository;

        public ImportPermissionsCommandHandler(
            IPermissionRepository permissionRepository,
            ITransactionScopeManager transactionScopeManager
            )
        {
            _permissionRepository = permissionRepository;
            _transactionScopeManager = transactionScopeManager;
        }


        public async Task ExecuteAsync(ImportPermissionsCommand command)
        {
            var sb = new StringBuilder();

            foreach (var permission in _permissionRepository.GetAll())
            {
                if (permission is IEntityPermission)
                {
                    var entityDefinition = ((IEntityPermission)permission).EntityDefinition;

                    sb.AppendLine(string.Format("if not exists (select * from Cofoundry.[EntityDefinition] where EntityDefinitionCode = '{0}')", entityDefinition.EntityDefinitionCode));
                    sb.AppendLine("begin");
                    sb.AppendLine(string.Format("insert into Cofoundry.[EntityDefinition] (EntityDefinitionCode, Name) values ('{0}', '{1}')", entityDefinition.EntityDefinitionCode, entityDefinition.Name));
                    sb.AppendLine("end");
                    sb.AppendLine(string.Format("insert into Cofoundry.[Permission] (EntityDefinitionCode, PermissionCode) values ('{0}', '{1}')", entityDefinition.EntityDefinitionCode, permission.PermissionType.Code));
                }
                else
                {
                    sb.AppendLine(string.Format("insert into Cofoundry.[Permission] (PermissionCode) values ('{0}')", permission.PermissionType.Code));
                }
            }

            var sql = sb.ToString();
            //using (var scope = _transactionScopeManager.Create(_db))
            {
                // TODO: use EFCore
                //await _db.ExecuteAsync(sql);
                //await scope.CompleteAsync();
            }
        }
    }
}
