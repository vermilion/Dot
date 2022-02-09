﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cofoundry.Domain.Data;
using Cofoundry.Domain.CQS;
using Microsoft.EntityFrameworkCore;
using Cofoundry.Core.AutoUpdate;
using Cofoundry.Core.Data;
using Dot.EFCore.AutoUpdate.Services;

namespace Cofoundry.Domain.Internal
{
    public class UpdateGeneralSiteSettingsCommandHandler 
        : IRequestHandler<UpdateGeneralSiteSettingsCommand, Unit>
        , IPermissionRestrictedRequestHandler<UpdateGeneralSiteSettingsCommand>
    {
        #region constructor

        private readonly DbContextCore _dbContext;
        private readonly SettingCommandHelper _settingCommandHelper;
        private readonly ISettingCache _settingCache;
        private readonly IAutoUpdateService _autoUpdateService;
        private readonly ITransactionScopeManager _transactionScopeFactory;

        public UpdateGeneralSiteSettingsCommandHandler(
            DbContextCore dbContext,
            SettingCommandHelper settingCommandHelper,
            ISettingCache settingCache,
            IAutoUpdateService autoUpdateService,
            ITransactionScopeManager transactionScopeFactory
            )
        {
            _settingCommandHelper = settingCommandHelper;
            _dbContext = dbContext;
            _settingCache = settingCache;
            _autoUpdateService = autoUpdateService;
            _transactionScopeFactory = transactionScopeFactory;
        }

        #endregion

        #region execute

        public async Task<Unit> ExecuteAsync(UpdateGeneralSiteSettingsCommand command, IExecutionContext executionContext)
        {
            var allSettings = await _dbContext
                .Settings
                .ToListAsync();

            _settingCommandHelper.SetSettingProperty(command, c => c.ApplicationName, allSettings, executionContext);

            using (var scope = _transactionScopeFactory.Create(_dbContext))
            {
                await _dbContext.SaveChangesAsync();
                await _autoUpdateService.SetLockedAsync(!command.AllowAutomaticUpdates);

                await scope.CompleteAsync();
            }

            await _transactionScopeFactory.QueueCompletionTaskAsync(_dbContext, () => Task.Run(_settingCache.Clear));

            return Unit.Value;
        }

        #endregion

        #region Permission

        public IEnumerable<IPermissionApplication> GetPermissions(UpdateGeneralSiteSettingsCommand command)
        {
            yield return new GeneralSettingsUpdatePermission();
        }

        #endregion
    }
}
