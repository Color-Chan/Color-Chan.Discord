using System;
using Color_Chan.Discord.Caching.Configurations;
using Color_Chan.Discord.Caching.Extensions;
using Color_Chan.Discord.Commands.Configurations;
using Color_Chan.Discord.Commands.Services;
using Color_Chan.Discord.Commands.Services.Builders;
using Color_Chan.Discord.Commands.Services.Implementations;
using Color_Chan.Discord.Commands.Services.Implementations.Builders;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Color_Chan.Discord.Commands.Extensions
{
    /// <summary>
    ///     Contains all the extensions methods for <see cref="IServiceCollection" />.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Add the dependencies for Color-Chan.Discord.Command to the <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" />.</param>
        /// <param name="slashCommandConfigs">The configuration options for slash commands.</param>
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
        public static IServiceCollection AddColorChanDiscordCommand(this IServiceCollection services, 
                                                                    Action<SlashCommandConfiguration>? slashCommandConfigs = null, 
                                                                    Action<CacheConfiguration>? defaultCacheConfig = null, 
                                                                    Action<RedisCacheOptions>? redisCacheOptions = null)
        {
            services.TryAddTransient<ISlashCommandRequirementBuildService, SlashCommandRequirementBuildService>();
            services.TryAddTransient<ISlashCommandRequirementService, SlashCommandRequirementService>();
            services.TryAddTransient<ISlashCommandOptionBuildService, SlashCommandOptionBuildService>();
            services.TryAddTransient<ISlashCommandGuildBuildService, SlashCommandGuildBuildService>();
            services.TryAddTransient<ISlashCommandAutoSyncService, SlashCommandAutoSyncService>();
            services.TryAddTransient<ISlashCommandBuildService, SlashCommandBuildService>();
            services.TryAddSingleton<IDiscordSlashCommandHandler, DiscordSlashCommandHandler>();
            services.TryAddSingleton<ISlashCommandService, SlashCommandService>();

            slashCommandConfigs ??= configuration =>
            {
                configuration.EnableAutoSync = true;
                configuration.EnableAutoGetChannel = false;
                configuration.EnableAutoGetGuild = false;
            };

            services.Configure(slashCommandConfigs);
            
            services.AddColorChanCache(defaultCacheConfig, redisCacheOptions);

            return services;
        }
        
        /// <summary>
        ///     Adds a <see cref="ISlashCommandPipeline"/> to the <paramref name="services"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> where the <typeparamref name="TPipeline"/> will be added to.</param>
        /// <typeparam name="TPipeline">The new <see cref="ISlashCommandPipeline"/>.</typeparam>
        /// <returns>
        ///     The updated <see cref="IServiceCollection" />.
        /// </returns>
        public static IServiceCollection AddSlashCommandPipeline<TPipeline>(this IServiceCollection services) where TPipeline : class, ISlashCommandPipeline
        {
            services.AddTransient<ISlashCommandPipeline, TPipeline>();
            return services;
        }
    }
}