using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Color_Chan.Discord.Caching.Results;
using Color_Chan.Discord.Core.Results;
using Microsoft.Extensions.Caching.Memory;

namespace Color_Chan.Discord.Caching.Services.Implementations
{
    /// <inheritdoc />
    public class LocalCacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ITypeCacheConfigurationService _configurationService;
        private readonly ConcurrentDictionary<object, SemaphoreSlim> _locks = new();

        /// <summary>
        ///     Initializes a new instance of <see cref="LocalCacheService" />.
        /// </summary>
        /// <param name="memoryCache">The <see cref="IMemoryCache"/> that will contain all the cached values.</param>
        /// <param name="configurationService">The <see cref="ITypeCacheConfigurationService"/> that contains all the cache config for the value types.</param>
        public LocalCacheService(IMemoryCache memoryCache, ITypeCacheConfigurationService configurationService)
        {
            _memoryCache = memoryCache;
            _configurationService = configurationService;
        }

        /// <inheritdoc />
        public async Task<Result<TValue>> GetOrCreateValueAsync<TValue>(string key, Task<TValue> getValue) where TValue : class
        {
            var cacheConfig = _configurationService.GetCacheConfig<TValue>();

            SemaphoreSlim cacheLock = _locks.GetOrAdd(key, _ => new SemaphoreSlim(1, 1));
            
            await cacheLock.WaitAsync();
            var data = await _memoryCache.GetOrCreateAsync(key, test =>
            {
                test.SetSlidingExpiration(cacheConfig.SlidingExpiration);
                test.SetAbsoluteExpiration(cacheConfig.AbsoluteExpiration);
                return getValue;
            }).ConfigureAwait(false);
            cacheLock.Release();

            return data is null 
                ? Result<TValue>.FromError(default, new CacheErrorResult(key)) 
                : Result<TValue>.FromSuccess(data);
        }

        /// <inheritdoc />
        public Task<Result<TValue>> GetValueAsync<TValue>(string key) where TValue : class
        {
            if (_memoryCache.TryGetValue(key, out TValue cachedValue))
            {
                return Task.FromResult(Result<TValue>.FromSuccess(cachedValue));
            }

            return Task.FromResult(Result<TValue>.FromError(default, new CacheErrorResult(key)));
        }
        
        /// <inheritdoc />
        public async Task RemoveValueAsync(string key)
        {
            SemaphoreSlim cacheLock = _locks.GetOrAdd(key, _ => new SemaphoreSlim(1, 1));
            
            await cacheLock.WaitAsync();
            _memoryCache.Remove(key);
            cacheLock.Release();
        }
        
        /// <inheritdoc />
        public async Task CacheValueAsync<TValue>(string key, TValue cachedValue) where TValue : class
        {
            // Set the cached value.
            SemaphoreSlim cacheLock = _locks.GetOrAdd(key, _ => new SemaphoreSlim(1, 1));
            await cacheLock.WaitAsync();
            _memoryCache.Set(key, cachedValue, GetCacheConfig<TValue>());
            cacheLock.Release();
        }

        /// <inheritdoc />
        public void CacheValue<TValue>(string key, TValue cachedValue) where TValue : class
        {
            // Set the cached value.
            _memoryCache.Set(key, cachedValue, GetCacheConfig<TValue>());
        }
        
        private MemoryCacheEntryOptions GetCacheConfig<TValue>() where TValue : class
        {
            var cacheConfig = _configurationService.GetCacheConfig<TValue>();
            var redisCacheConfig = new MemoryCacheEntryOptions
            {
                SlidingExpiration = cacheConfig.SlidingExpiration,
                AbsoluteExpirationRelativeToNow = cacheConfig.AbsoluteExpiration
            };

            return redisCacheConfig;
        }
    }
}