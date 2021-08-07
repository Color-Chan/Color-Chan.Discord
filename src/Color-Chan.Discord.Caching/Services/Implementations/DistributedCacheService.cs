using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Color_Chan.Discord.Caching.Results;
using Color_Chan.Discord.Core.Results;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace Color_Chan.Discord.Caching.Services.Implementations
{
    /// <inheritdoc />
    public class DistributedCacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly ITypeCacheConfigurationService _configurationService;
        private readonly JsonSerializerOptions _serializerOptions;

        /// <summary>
        ///     Initializes a new instance of <see cref="DistributedCacheService" />.
        /// </summary>
        /// <param name="distributedCache">The <see cref="IDistributedCache"/> that will contain all the cached values.</param>
        /// <param name="serializerOptions">The JSON options used for serialization.</param>
        /// <param name="configurationService">The <see cref="ITypeCacheConfigurationService"/> that contains all the cache config for the value types.</param>
        public DistributedCacheService(IDistributedCache distributedCache, IOptions<JsonSerializerOptions> serializerOptions, ITypeCacheConfigurationService configurationService)
        {
            _distributedCache = distributedCache;
            _configurationService = configurationService;
            _serializerOptions = serializerOptions.Value;
        }
        
        /// <inheritdoc />
        public async Task<Result<TValue>> GetOrCreateValueAsync<TValue>(string key, Func<Task<TValue>> getValue) where TValue : notnull
        {
            var result = await _distributedCache.GetAsync(key);

            if (result is null)
            {
                return await GetUncachedValueAsync();
            }
            
            var stream = new MemoryStream(result);
            var cachedValue = await JsonSerializer.DeserializeAsync<TValue>(stream, _serializerOptions).ConfigureAwait(false);

            if (cachedValue is null)
            {
                return await GetUncachedValueAsync();
            }

            return Result<TValue>.FromSuccess(cachedValue);
            
            async Task<Result<TValue>> GetUncachedValueAsync()
            {
                var uncachedValue = await getValue();
                await CacheValueAsync(key, uncachedValue);
                return Result<TValue>.FromSuccess(uncachedValue);
            }
        }

        /// <inheritdoc />
        public async Task<Result<TValue>> GetValueAsync<TValue>(string key) where TValue : notnull
        {
            var result = await _distributedCache.GetAsync(key);

            if (result is null)
            {
                return Result<TValue>.FromError(default, new CacheErrorResult(key));
            }
            
            var stream = new MemoryStream(result);
            var cachedValue = await JsonSerializer.DeserializeAsync<TValue>(stream, _serializerOptions).ConfigureAwait(false);

            return cachedValue is null 
                ? Result<TValue>.FromError(default, new CacheErrorResult(key)) 
                : Result<TValue>.FromSuccess(cachedValue);
        }

        /// <inheritdoc />
        public async Task RemoveValueAsync(string key)
        {
            await _distributedCache.RemoveAsync(key);
        }

        /// <inheritdoc />
        public async Task CacheValueAsync<TValue>(string key, TValue cachedValue) where TValue : notnull
        {
            // Set the cached value.
            var bytes = JsonSerializer.SerializeToUtf8Bytes(cachedValue, _serializerOptions);
            await _distributedCache.SetAsync(key, bytes, GetCacheConfig<TValue>());
        }

        /// <inheritdoc />
        public async Task CacheValueAsync<TValue>(string key, TValue cachedValue, TimeSpan slidingExpirationOverwrite, TimeSpan absoluteExpirationOverwrite) where TValue : notnull
        {
            var redisCacheConfig = new DistributedCacheEntryOptions
            {
                SlidingExpiration = slidingExpirationOverwrite,
                AbsoluteExpirationRelativeToNow = absoluteExpirationOverwrite
            };
            
            // Set the cached value.
            var bytes = JsonSerializer.SerializeToUtf8Bytes(cachedValue, _serializerOptions);
            await _distributedCache.SetAsync(key, bytes, redisCacheConfig);
        }

        /// <inheritdoc />
        public void CacheValue<TValue>(string key, TValue cachedValue) where TValue : notnull
        {
            // Set the cached value.
            var bytes = JsonSerializer.SerializeToUtf8Bytes(cachedValue, _serializerOptions);
            _distributedCache.Set(key, bytes, GetCacheConfig<TValue>());
        }

        /// <inheritdoc />
        public void CacheValue<TValue>(string key, TValue cachedValue, TimeSpan slidingExpirationOverwrite, TimeSpan absoluteExpirationOverwrite) where TValue : notnull
        {
            var redisCacheConfig = new DistributedCacheEntryOptions
            {
                SlidingExpiration = slidingExpirationOverwrite,
                AbsoluteExpirationRelativeToNow = absoluteExpirationOverwrite
            };
            
            // Set the cached value.
            var bytes = JsonSerializer.SerializeToUtf8Bytes(cachedValue, _serializerOptions);
            _distributedCache.Set(key, bytes, redisCacheConfig);
        }

        private DistributedCacheEntryOptions GetCacheConfig<TValue>() where TValue : notnull
        {
            var cacheConfig = _configurationService.GetCacheConfig<TValue>();
            var redisCacheConfig = new DistributedCacheEntryOptions
            {
                SlidingExpiration = cacheConfig.SlidingExpiration,
                AbsoluteExpirationRelativeToNow = cacheConfig.AbsoluteExpiration
            };

            return redisCacheConfig;
        }
    }
}