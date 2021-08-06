using System;
using System.Threading.Tasks;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Caching.Services
{
    /// <summary>
    ///     Handles all the caching for Color-Chan.Discord.
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        ///     Get or creates a cached value.
        ///     Returns the cached value if it is cached.
        ///     It will get the value from <paramref name="getValue"/> and cache it if the value is not current cached.
        /// </summary>
        /// <param name="key">The key of the cached value.</param>
        /// <param name="getValue">The task that will be executed if the cached value does not exist.</param>
        /// <typeparam name="TValue">The type of the cached value.</typeparam>
        /// <returns>
        ///     Return a <see cref="Result{T}"/> with the <see cref="TValue"/> that was cached.
        /// </returns>
        Task<Result<TValue>> GetOrCreateValueAsync<TValue>(string key, Task<TValue> getValue) where TValue : notnull;

        /// <summary>
        ///     Tries to get a value from the cache.
        /// </summary>
        /// <param name="key">The key of the cached value.</param>
        /// <typeparam name="TValue">The type of the cached value.</typeparam>
        /// <returns>
        ///     Return a <see cref="Result{T}"/> with the <see cref="TValue"/> if the value was cached.
        /// </returns>
        Task<Result<TValue>> GetValueAsync<TValue>(string key) where TValue : notnull;

        /// <summary>
        ///     Removes a value from the cache.
        /// </summary>
        /// <param name="key">The key of the cached value.</param>
        Task RemoveValueAsync(string key);

        /// <summary>
        ///     Caches a value.
        /// </summary>
        /// <param name="key">The key of the value that will be cached.</param>
        /// <param name="cachedValue">The value that will be cached.</param>
        /// <typeparam name="TValue">The type of the cached value.</typeparam>
        Task CacheValueAsync<TValue>(string key, TValue cachedValue) where TValue : notnull;

        /// <summary>
        ///     Caches a value.
        /// </summary>
        /// <param name="key">The key of the value that will be cached.</param>
        /// <param name="cachedValue">The value that will be cached.</param>
        /// <param name="slidingExpirationOverwrite">How long a cache entry can be inactive (e.g. not accessed) before it will be removed.</param>
        /// <param name="absoluteExpirationOverwrite">The absolute expiration time, relative to now.</param>
        /// <typeparam name="TValue">The type of the cached value.</typeparam>
        Task CacheValueAsync<TValue>(string key, TValue cachedValue, TimeSpan slidingExpirationOverwrite, TimeSpan absoluteExpirationOverwrite) where TValue : notnull;
        
        /// <summary>
        ///     Caches a value.
        /// </summary>
        /// <param name="key">The key of the value that will be cached.</param>
        /// <param name="cachedValue">The value that will be cached.</param>
        /// <typeparam name="TValue">The type of the cached value.</typeparam>
        void CacheValue<TValue>(string key, TValue cachedValue) where TValue : notnull;
        
        /// <summary>
        ///     Caches a value.
        /// </summary>
        /// <param name="key">The key of the value that will be cached.</param>
        /// <param name="cachedValue">The value that will be cached.</param>
        /// <param name="slidingExpirationOverwrite">How long a cache entry can be inactive (e.g. not accessed) before it will be removed.</param>
        /// <param name="absoluteExpirationOverwrite">The absolute expiration time, relative to now.</param>
        /// <typeparam name="TValue">The type of the cached value.</typeparam>
        void CacheValue<TValue>(string key, TValue cachedValue, TimeSpan slidingExpirationOverwrite, TimeSpan absoluteExpirationOverwrite) where TValue : notnull;
    }
}