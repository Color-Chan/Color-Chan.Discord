using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Color_Chan.Discord.Caching.Results;
using Color_Chan.Discord.Core.Results;
using Microsoft.Extensions.Caching.Memory;

namespace Color_Chan.Discord.Caching.Services.Implementations;

/// <inheritdoc />
public class LocalCacheService : ICacheService
{
    private readonly ITypeCacheConfigurationService _configurationService;
    private readonly ConcurrentDictionary<object, SemaphoreSlim> _locks = new();
    private readonly IMemoryCache _memoryCache;

    /// <summary>
    ///     Initializes a new instance of <see cref="LocalCacheService" />.
    /// </summary>
    /// <param name="memoryCache">The <see cref="IMemoryCache" /> that will contain all the cached values.</param>
    /// <param name="configurationService">
    ///     The <see cref="ITypeCacheConfigurationService" /> that contains all the cache config
    ///     for the value types.
    /// </param>
    public LocalCacheService(IMemoryCache memoryCache, ITypeCacheConfigurationService configurationService)
    {
        _memoryCache = memoryCache;
        _configurationService = configurationService;
    }

    /// <inheritdoc />
    public async Task<Result<TValue>> GetOrCreateValueAsync<TValue>(string key, Func<Task<TValue>> getValue) where TValue : notnull
    {
        var cacheConfig = _configurationService.GetCacheConfig<TValue>();

        var cacheLock = _locks.GetOrAdd(key, _ => new SemaphoreSlim(1, 1));

        await cacheLock.WaitAsync().ConfigureAwait(false);
        var data = await _memoryCache.GetOrCreateAsync(key, test =>
        {
            test.SetSlidingExpiration(cacheConfig.SlidingExpiration);
            test.SetAbsoluteExpiration(cacheConfig.AbsoluteExpiration);
            return getValue();
        }).ConfigureAwait(false);
        cacheLock.Release();

        return data is null
            ? Result<TValue>.FromError(default, new CacheErrorResult(key))
            : Result<TValue>.FromSuccess(data);
    }

    /// <inheritdoc />
    public Task<Result<TValue>> GetValueAsync<TValue>(string key) where TValue : notnull
    {
        if (_memoryCache.TryGetValue(key, out TValue? cachedValue) && cachedValue is not null)
        {
            return Task.FromResult(Result<TValue>.FromSuccess(cachedValue));
        }

        return Task.FromResult(Result<TValue>.FromError(default, new CacheErrorResult(key)));
    }

    /// <inheritdoc />
    public async Task RemoveValueAsync(string key)
    {
        var cacheLock = _locks.GetOrAdd(key, _ => new SemaphoreSlim(1, 1));

        await cacheLock.WaitAsync().ConfigureAwait(false);
        _memoryCache.Remove(key);
        cacheLock.Release();
    }

    /// <inheritdoc />
    public async Task CacheValueAsync<TValue>(string key, TValue cachedValue) where TValue : notnull
    {
        // Set the cached value.
        var cacheLock = _locks.GetOrAdd(key, _ => new SemaphoreSlim(1, 1));
        await cacheLock.WaitAsync().ConfigureAwait(false);
        _memoryCache.Set(key, cachedValue, GetCacheConfig<TValue>());
        cacheLock.Release();
    }

    /// <inheritdoc />
    public async Task CacheValueAsync<TValue>(string key, TValue cachedValue, TimeSpan? slidingExpirationOverwrite, TimeSpan? absoluteExpirationRelativeToNow) where TValue : notnull
    {
        var cacheConfig = new MemoryCacheEntryOptions
        {
            SlidingExpiration = slidingExpirationOverwrite,
            AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow
        };

        // Set the cached value.
        var cacheLock = _locks.GetOrAdd(key, _ => new SemaphoreSlim(1, 1));
        await cacheLock.WaitAsync().ConfigureAwait(false);
        _memoryCache.Set(key, cachedValue, cacheConfig);
        cacheLock.Release();
    }

    /// <inheritdoc />
    public async Task CacheValueAsync<TValue>(string key, TValue cachedValue, TimeSpan? slidingExpirationOverwrite, DateTimeOffset? absoluteExpiration) where TValue : notnull
    {
        var cacheConfig = new MemoryCacheEntryOptions
        {
            SlidingExpiration = slidingExpirationOverwrite,
            AbsoluteExpiration = absoluteExpiration
        };

        // Set the cached value.
        var cacheLock = _locks.GetOrAdd(key, _ => new SemaphoreSlim(1, 1));
        await cacheLock.WaitAsync().ConfigureAwait(false);
        _memoryCache.Set(key, cachedValue, cacheConfig);
        cacheLock.Release();
    }

    /// <inheritdoc />
    public void CacheValue<TValue>(string key, TValue cachedValue) where TValue : notnull
    {
        // Set the cached value.
        _memoryCache.Set(key, cachedValue, GetCacheConfig<TValue>());
    }

    /// <inheritdoc />
    public void CacheValue<TValue>(string key, TValue cachedValue, TimeSpan? slidingExpirationOverwrite, TimeSpan? absoluteExpirationRelativeToNow) where TValue : notnull
    {
        var cacheConfig = new MemoryCacheEntryOptions
        {
            SlidingExpiration = slidingExpirationOverwrite,
            AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow
        };

        // Set the cached value.
        _memoryCache.Set(key, cachedValue, cacheConfig);
    }

    /// <inheritdoc />
    public void CacheValue<TValue>(string key, TValue cachedValue, TimeSpan? slidingExpirationOverwrite, DateTimeOffset? absoluteExpiration) where TValue : notnull
    {
        var cacheConfig = new MemoryCacheEntryOptions
        {
            SlidingExpiration = slidingExpirationOverwrite,
            AbsoluteExpiration = absoluteExpiration
        };

        // Set the cached value.
        _memoryCache.Set(key, cachedValue, cacheConfig);
    }

    private MemoryCacheEntryOptions GetCacheConfig<TValue>() where TValue : notnull
    {
        var cacheConfig = _configurationService.GetCacheConfig<TValue>();
        return new MemoryCacheEntryOptions
        {
            SlidingExpiration = cacheConfig.SlidingExpiration,
            AbsoluteExpirationRelativeToNow = cacheConfig.AbsoluteExpiration
        };
    }
}