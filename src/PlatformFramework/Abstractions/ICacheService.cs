using System;
using System.Threading;
using System.Threading.Tasks;

namespace PlatformFramework.Interfaces.Caching
{
    /// <summary>
    /// ICacheService encapsulates caching functionality.
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// A thread-safe way of working with memory cache. First tries to get the key's value from the cache.
        /// Otherwise it will use the factory method to get the value and then inserts it.
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="factory"></param>
        /// <param name="slidingExpiration"></param>
        /// <typeparam name="T"></typeparam>
        Task<T> GetOrAdd<T>(string cacheKey, Func<Task<T>> factory, TimeSpan slidingExpiration, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the key's value from the cache.
        /// </summary>
        Task<T> Get<T>(string cacheKey, CancellationToken cancellationToken = default);

        /// Adds a key-value to the cache.
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="value"></param>
        /// <param name="slidingExpiration"></param>
        /// <typeparam name="T"></typeparam>
        Task Add<T>(string cacheKey, T value, TimeSpan slidingExpiration, CancellationToken cancellationToken = default);

        /// <summary>
        /// Removes the object associated with the given key.
        /// </summary>
        Task Remove(string cacheKey);
    }
}