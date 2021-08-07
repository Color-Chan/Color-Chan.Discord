using System;
using Color_Chan.Discord.Caching.Configurations;
using Color_Chan.Discord.Caching.Services;
using Color_Chan.Discord.Caching.Services.Implementations;
using Color_Chan.Discord.Core.Extensions;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection;

namespace Color_Chan.Discord.Caching.Extensions
{
    /// <summary>
    ///     Contains all the extension methods for <see cref="IServiceCollection" />.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Add the dependencies for Color-Chan.Discord.Caching to the <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" />.</param>
        /// <param name="defaultCacheConfig">
        ///     The default cache configurations.
        ///     Leave this null to use the default expiration values.
        /// </param>
        /// <param name="redisCacheOptions">
        ///     The cache options for the redis cache.
        ///     Leave this null if you want to use a local cache.
        /// </param>
        /// <returns>
        ///     The updated <see cref="IServiceCollection" />.
        /// </returns>
        public static IServiceCollection AddColorChanCache(this IServiceCollection services, Action<CacheConfiguration>? defaultCacheConfig = null, Action<RedisCacheOptions>? redisCacheOptions = null)
        {
            // Set the default config if none provided.
            defaultCacheConfig ??= configuration =>
            {
                configuration.AbsoluteExpiration = TimeSpan.FromSeconds(30);
                configuration.SlidingExpiration = TimeSpan.FromSeconds(15);
            };

            services.Configure(defaultCacheConfig);

            if (redisCacheOptions is not null)
            {
                services.AddStackExchangeRedisCache(redisCacheOptions);
                services.AddSingleton<ICacheService, DistributedCacheService>();
            }
            else
            {
                services.AddMemoryCache();
                services.AddSingleton<ICacheService, LocalCacheService>();
            }

            services.AddSingleton<ITypeCacheConfigurationService, TypeCacheConfigurationService>();
            services.AddColorChanDiscordCore();

            return services;
        }
    }
}