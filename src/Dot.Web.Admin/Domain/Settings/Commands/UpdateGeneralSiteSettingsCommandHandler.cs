using Cofoundry.Domain.CQS;
using Cofoundry.Domain.Data;
using Dot.EFCore.Transactions.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cofoundry.Domain.Internal
{
    public class UpdateGeneralSiteSettingsCommandHandler 
        : IRequestHandler<UpdateGeneralSiteSettingsCommand, Unit>
        , IPermissionRestrictedRequestHandler<UpdateGeneralSiteSettingsCommand>
    {
        private readonly DbContextCore _dbContext;
        private readonly SettingCommandHelper _settingCommandHelper;
        private readonly ISettingCache _settingCache;
        private readonly ITransactionScopeManager _transactionScopeFactory;

        public UpdateGeneralSiteSettingsCommandHandler(
            DbContextCore dbContext,
            SettingCommandHelper settingCommandHelper,
            ISettingCache settingCache,
            ITransactionScopeManager transactionScopeFactory
            )
        {
            _settingCommandHelper = settingCommandHelper;
            _dbContext = dbContext;
            _settingCache = settingCache;
            _transactionScopeFactory = transactionScopeFactory;
        }

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
