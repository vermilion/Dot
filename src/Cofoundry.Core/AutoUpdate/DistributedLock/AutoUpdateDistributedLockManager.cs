using Cofoundry.Core.DistributedLocks;
using System;
using System.Threading.Tasks;

namespace Cofoundry.Core.AutoUpdate.Internal
{
    /// <summary>
    /// Manages locking of the auto-update process to prevent concurrent update
    /// processes being run in multi-instance deployments.
    /// </summary>
    /// <remarks>
    /// This usage of distributed locks is different to others in that it needs
    /// to boostrap the sql locking tables first as this will run prior to the
    /// autoupdate process and therefore the database may be empty.
    /// </remarks>
    public class AutoUpdateDistributedLockManager : IAutoUpdateDistributedLockManager
    {
        private readonly IDistributedLockManager _distributedLockManager;
        private readonly AutoUpdateSettings _autoUpdateSettings;

        public AutoUpdateDistributedLockManager(
            IDistributedLockManager distributedLockManager,
            AutoUpdateSettings autoUpdateSettings
            )
        {
            _distributedLockManager = distributedLockManager;
            _autoUpdateSettings = autoUpdateSettings;
        }

        public async Task<DistributedLock> LockAsync()
        {
            var distributedLock = await _distributedLockManager.LockAsync<AutoUpdateDistributedLockDefinition>();

            if (distributedLock == null || !distributedLock.IsLocked())
            {
                throw new Exception($"Error attempting to lock the auto update process.");
            }

            if (distributedLock.IsLockedByAnotherProcess())
            {
                throw new AutoUpdateProcessLockedException(distributedLock);
            }

            return distributedLock;
        }

        public Task UnlockAsync(DistributedLock distributedLock)
        {
            return _distributedLockManager.UnlockAsync(distributedLock);
        }
    }
}
