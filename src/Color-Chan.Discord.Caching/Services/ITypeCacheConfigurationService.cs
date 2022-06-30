using System;
using Color_Chan.Discord.Caching.Configurations;
using Color_Chan.Discord.Caching.Services.Implementations;

namespace Color_Chan.Discord.Caching.Services;

/// <summary>
///     Manages and contains all the <see cref="CacheConfiguration" /> for all kind of value types.
/// </summary>
public interface ITypeCacheConfigurationService
{
    /// <summary>
    ///     Adds a <see cref="CacheConfiguration" /> for a specific type.
    /// </summary>
    /// <param name="slidingExpiration">The absolute expiration time, relative to now.</param>
    /// <param name="absoluteExpiration">How long a cache entry can be inactive (e.g. not accessed) before it will be removed.</param>
    /// <typeparam name="TValueType">The type of the value.</typeparam>
    /// <returns>
    ///     The <see cref="TypeCacheConfigurationService" /> containing the added type cache config.
    /// </returns>
    TypeCacheConfigurationService AddCacheConfig<TValueType>(TimeSpan slidingExpiration, TimeSpan absoluteExpiration);

    /// <summary>
    ///     Get the cache config for a specific value type.
    /// </summary>
    /// <typeparam name="TValueType">The value type.</typeparam>
    /// <returns>
    ///     The <see cref="CacheConfiguration" /> for the specified type if one was found.
    ///     Return the default <see cref="CacheConfiguration" /> if none were found.
    /// </returns>
    CacheConfiguration GetCacheConfig<TValueType>();
}