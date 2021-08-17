using System;
using Color_Chan.Discord.Caching.Configurations;
using Color_Chan.Discord.Commands.Configurations;
using Microsoft.Extensions.Caching.StackExchangeRedis;

namespace Color_Chan.Discord.Configurations
{
    /// <summary>
    ///     The configurations class holding all the configurations for Color-Chan.Discord.
    /// </summary>
    public class ColorChanConfigurations
    {
        /// <summary>
        ///     The configurations for all the interactions.
        /// </summary>
        public Action<InteractionsConfiguration>? InteractionConfigs { get; set; }

        /// <summary>
        ///     The configuration options for slash commands.
        /// </summary>
        public Action<SlashCommandConfiguration>? SlashCommandConfigs { get; set; }

        /// <summary>
        ///     The default cache configurations.
        ///     Leave this null to use the default expiration values.
        /// </summary>
        public Action<CacheConfiguration>? DefaultCacheConfig { get; set; }

        /// <summary>
        ///     The cache options for the redis cache.
        ///     Leave this null if you want to use a local cache.
        /// </summary>
        public Action<RedisCacheOptions>? RedisCacheOptions { get; set; }

        /// <summary>
        ///     The configurations needed for the component interactions.
        /// </summary>
        public Action<ComponentInteractionConfiguration>? ComponentInteractionConfig { get; set; }
    }
}