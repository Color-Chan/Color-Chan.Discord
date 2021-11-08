using System;
using Color_Chan.Discord.Caching.Configurations;
using Color_Chan.Discord.Commands.Configurations;
using Color_Chan.Discord.Commands.Extensions;
using Color_Chan.Discord.Commands.Services;
using Color_Chan.Discord.Commands.Services.Implementations;
using Color_Chan.Discord.Configurations;
using Color_Chan.Discord.Core;
using Color_Chan.Discord.Rest.Extensions;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Color_Chan.Discord.Extensions
{
    /// <summary>
    ///     Contains all the extension methods for <see cref="IServiceCollection" />.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Add the dependencies for Color-Chan.Discord to the <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" />.</param>
        /// <param name="botToken">
        ///     The bot token of your application.
        ///     This can be found at https://discord.com/developers/applications/APPLICATION_ID/bot
        /// </param>
        /// <param name="publicBotToken">
        ///     The public token of your application.
        ///     This can be found at https://discord.com/developers/applications/APPLICATION_ID/information
        /// </param>
        /// <param name="applicationId">
        ///     The ID of your application.
        ///     This can be found at https://discord.com/developers/applications/APPLICATION_ID/information
        /// </param>
        /// <param name="interactionConfigs">The configurations for all the interactions.</param>
        /// <param name="slashCommandConfigs">The configuration options for slash commands.</param>
        /// <param name="defaultCacheConfig">
        ///     The default cache configurations.
        ///     Leave this null to use the default expiration values.
        /// </param>
        /// <param name="redisCacheOptions">
        ///     The cache options for the redis cache.
        ///     Leave this null if you want to use a local cache.
        /// </param>
        /// <param name="componentInteractionConfig">The configurations needed for the component interactions.</param>
        /// <returns>
        ///     The updated <see cref="IServiceCollection" />.
        /// </returns>
        public static IServiceCollection AddColorChanDiscord(this IServiceCollection services,
                                                             string botToken,
                                                             string publicBotToken,
                                                             ulong applicationId,
                                                             Action<InteractionsConfiguration>? interactionConfigs = null,
                                                             Action<SlashCommandConfiguration>? slashCommandConfigs = null,
                                                             Action<CacheConfiguration>? defaultCacheConfig = null,
                                                             Action<RedisCacheOptions>? redisCacheOptions = null,
                                                             Action<ComponentInteractionConfiguration>? componentInteractionConfig = null)
        {
            if (botToken == null) throw new ArgumentNullException(nameof(botToken));

            services.AddColorChanDiscordRest(botToken, defaultCacheConfig, redisCacheOptions);
            services.AddColorChanDiscordCommand(slashCommandConfigs, defaultCacheConfig, redisCacheOptions, componentInteractionConfig);

            services.AddSingleton(_ => new DiscordTokens(botToken, publicBotToken, applicationId));
            services.AddSingleton<IDiscordSlashCommandHandler, DiscordSlashCommandHandler>();
            services.TryAddTransient<IDiscordInteractionAuthService, DiscordInteractionAuthService>();

            interactionConfigs ??= configuration => { configuration.VerifyInteractions = true; };

            services.Configure(interactionConfigs);

            return services;
        }

        /// <summary>
        ///     Add the dependencies for Color-Chan.Discord to the <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" />.</param>
        /// <param name="botToken">
        ///     The bot token of your application.
        ///     This can be found at https://discord.com/developers/applications/APPLICATION_ID/bot
        /// </param>
        /// <param name="publicBotToken">
        ///     The public token of your application.
        ///     This can be found at https://discord.com/developers/applications/APPLICATION_ID/information
        /// </param>
        /// <param name="applicationId">
        ///     The ID of your application.
        ///     This can be found at https://discord.com/developers/applications/APPLICATION_ID/information
        /// </param>
        /// <param name="configurations">All the configuration options for Color-Chan.Discord.</param>
        public static IServiceCollection AddColorChanDiscord(this IServiceCollection services, string botToken, string publicBotToken, ulong applicationId, ColorChanConfigurations configurations)
        {
            return services.AddColorChanDiscord(botToken,
                                                publicBotToken,
                                                applicationId,
                                                configurations.InteractionConfigs,
                                                configurations.SlashCommandConfigs,
                                                configurations.DefaultCacheConfig,
                                                configurations.RedisCacheOptions,
                                                configurations.ComponentInteractionConfig);
        }
    }
}