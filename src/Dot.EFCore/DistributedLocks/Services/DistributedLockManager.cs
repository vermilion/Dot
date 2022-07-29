using Medallion.Threading;
using System;
using System.Threading.Tasks;

namespace Cofoundry.Core.DistributedLocks.Internal
{
    /// <summary>
    /// A distributed lock is a locking mechanism that can be used to
    /// prevents code from running in multiple concurrent processes, particularly 
    /// in scenarios where processes might be running in more than one applicatin
    /// or application instance. A central database is used to hold the locking
    /// information.
    /// </summary>
    /// <remarks>
    /// The distributed lock manager is used by the auto-update 
    /// process and will run before any db updates have been made, 
    /// therefore SQL has to be defined in code instead of stored
    /// proces. The auto update process created the initial db
    /// infrastructure for the distributed lock manager.
    /// </remarks>
    public class DistributedLockManager : IDistributedLockManager
    {
        private readonly IDistributedLockProvider _distributedLockProvider;

        public DistributedLockManager(IDistributedLockProvider distributedLockProvider)
        {
            _distributedLockProvider = distributedLockProvider;
        }

        public async Task<bool> IsLockedAsync(string lockKey)
        {
            var distributedLock = _distributedLockProvider.CreateLock(lockKey);

            var handle = await distributedLock.TryAcquireAsync();
            return handle != null;
        }

        /// <summary>
        /// Creates a lock for the specified definition using a unique
        /// lock id that represents the running process. You can
        /// query the returned DistributedLock instance to determine if 
        /// the lock was successful.
        /// </summary>
        /// <typeparam name="TDefinition">
        /// The definition type that contains the locking parameters that
        /// represent the process to be run.
        /// </typeparam>
        /// <returns>
        /// Returns the current state of the lock for the specified 
        /// definition. If the lock could not be made then the state will
        /// contain information about the existing lock, if the lock was 
        /// successful then the new lock will be returned. You can
        /// query the returned object to determine if the lock was successful.
        /// </returns>
        public ValueTask<IDistributedSynchronizationHandle> LockAsync(string lockKey, TimeSpan? timeout = null)
        {
            var distributedLock = _distributedLockProvider.CreateLock(lockKey);

            return distributedLock.AcquireAsync(timeout);
        }

        /// <summary>
        /// Unlocks the specified distributed lock, freeing it up
        /// for other processes to use.
        /// </summary>
        /// <param name="distributedLockHandle">
        /// The distributed lock entry to unlock. This should be the instance
        /// you received from a call to the LockAsync method.
        /// </param>
        public async Task UnlockAsync(IDistributedSynchronizationHandle distributedLockHandle)
        {
            if (distributedLockHandle == null) throw new ArgumentNullException(nameof(distributedLockHandle));

            await distributedLockHandle.DisposeAsync();
        }
    }
}
