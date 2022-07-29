using Cofoundry.Domain.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cofoundry.Domain.Internal
{
    /// <summary>
    /// Used internally by other query handlers to get settings. Bypasses permissions
    /// so should not be used outside of a query handler.
    /// </summary>
    public class InternalSettingsRepository : IInternalSettingsRepository
    {
        #region constructor

        private readonly DbContextCore _dbContext;
        private readonly ISettingCache _settingsCache;

        public InternalSettingsRepository(
            DbContextCore dbContext,
            ISettingCache settingsCache
            )
        {
            _dbContext = dbContext;
            _settingsCache = settingsCache;
        }

        #endregion

        public async Task<Dictionary<string, string>> GetAllSettingsAsync()
        {
            var settings = await _settingsCache.GetOrAddSettingsTableAsync(() =>
            {
                return _dbContext
                    .Settings
                    .AsNoTracking()
                    .ToDictionaryAsync(k => k.SettingKey, v => v.SettingValue);
            });

            return settings;
        }
    }
}
