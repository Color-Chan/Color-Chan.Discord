using System;
using System.Collections.Generic;
using Color_Chan.Discord.Caching.Configurations;
using Microsoft.Extensions.Options;

namespace Color_Chan.Discord.Caching.Services.Implementations
{
    /// <inheritdoc />
    public class TypeCacheConfigurationService : ITypeCacheConfigurationService
    {
        private readonly CacheConfiguration _defaultCacheConfig;
        private readonly Dictionary<Type, CacheConfiguration> _typeCacheConfigs = new();

        /// <summary>
        ///     Initializes a new instance of <see cref="TypeCacheConfigurationService" />.
        /// </summary>
        /// <param name="cacheConfig">The default cache configurations</param>
        public TypeCacheConfigurationService(IOptions<CacheConfiguration> cacheConfig)
        {
            _defaultCacheConfig = cacheConfig.Value;
        }

        /// <inheritdoc />
        public TypeCacheConfigurationService AddCacheConfig<TValueType>(TimeSpan slidingExpiration, TimeSpan absoluteExpiration)
        {
            if (slidingExpiration > absoluteExpiration)
            {
                throw new ArgumentOutOfRangeException(nameof(slidingExpiration), "The sliding expiration time can not be higher then the absolute expiration time.");
            }

            var config = new CacheConfiguration
            {
                AbsoluteExpiration = absoluteExpiration,
                SlidingExpiration = slidingExpiration
            };

            if (!_typeCacheConfigs.ContainsKey(typeof(TValueType)))
            {
                _typeCacheConfigs.Add(typeof(TValueType), config);
                return this;
            }

            _typeCacheConfigs[typeof(TValueType)] = config;
            return this;
        }

        /// <inheritdoc />
        public CacheConfiguration GetCacheConfig<TValueType>()
        {
            return _typeCacheConfigs.TryGetValue(typeof(TValueType), out var slidingExpiration)
                ? slidingExpiration
                : _defaultCacheConfig;
        }
    }
}