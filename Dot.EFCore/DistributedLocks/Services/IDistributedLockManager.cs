﻿using Medallion.Threading;
using System;
using System.Threading.Tasks;

namespace Cofoundry.Core.DistributedLocks
{
    /// <summary>
    /// A distributed lock is a locking mechanism that can be used to
    /// prevents code from running in multiple concurrent processes, particularly 
    /// in scenarios where processes might be running in more than one applicatin
    /// or application instance. A central database is used to hold the locking
    /// information.
    /// </summary>
    public interface IDistributedLockManager
    {
        Task<bool> IsLockedAsync(string lockKey);

        /// <summary>
        /// Creates a lock for the specified definition using a unique
        /// lock id that represents the running process. You can
        /// query the returned DistributedLock instance to determine if 
        /// the lock was successful.
        /// </summary>
        /// <param name="lockingId">
        /// A unique identifier that represents the specific instance of
        /// the process you want lock. 
        /// </param>
        /// <returns>
        /// Returns the current state of the lock for the specified 
        /// definition. If the lock could not be made then the state will
        /// contain information about the existing lock, if the lock was 
        /// successful then the new lock will be returned. You can
        /// query the returned object to determine if the lock was successful.
        /// </returns>
        ValueTask<IDistributedSynchronizationHandle> LockAsync(string lockKey, TimeSpan? timeout = null);

        /// <summary>
        /// Unlocks the specified distributed lock, freeing it up
        /// for other processes to use.
        /// </summary>
        /// <param name="distributedLock">
        /// The distributed lock entry to unlock. This should be the instance
        /// you received from a call to the LockAsync method.
        /// </param>
        Task UnlockAsync(IDistributedSynchronizationHandle distributedLock);
    }
}
