using System;
using Color_Chan.Discord.Caching.Configurations;
using Color_Chan.Discord.Caching.Extensions;
using Color_Chan.Discord.Commands.Configurations;
using Color_Chan.Discord.Commands.Services;
using Color_Chan.Discord.Commands.Services.Builders;
using Color_Chan.Discord.Commands.Services.Builders.Implementations;
using Color_Chan.Discord.Commands.Services.Implementations;
using Color_Chan.Discord.Commands.Services.InteractionHandlers;
using Color_Chan.Discord.Commands.Services.InteractionHandlers.Implementations;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Color_Chan.Discord.Commands.Extensions;

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
    /// <param name="componentInteractionConfig">The configurations options for the component interactions.</param>
    /// <returns>
    ///     The updated <see cref="IServiceCollection" />.
    /// </returns>
    public static IServiceCollection AddColorChanDiscordCommand(this IServiceCollection services,
                                                                Action<SlashCommandConfiguration>? slashCommandConfigs = null,
                                                                Action<CacheConfiguration>? defaultCacheConfig = null,
                                                                Action<RedisCacheOptions>? redisCacheOptions = null,
                                                                Action<ComponentInteractionConfiguration>? componentInteractionConfig = null)
    {
        services.TryAddTransient<ISlashCommandRequirementBuildService, SlashCommandRequirementBuildService>();
        services.TryAddTransient<ISlashCommandRequirementService, InteractionRequirementService>();
        services.TryAddTransient<ISlashCommandOptionBuildService, SlashCommandOptionBuildService>();
        services.TryAddTransient<ISlashCommandGuildBuildService, SlashCommandGuildBuildService>();
        services.TryAddTransient<ISlashCommandAutoSyncService, SlashCommandAutoSyncService>();
        services.TryAddTransient<ISlashCommandBuildService, SlashCommandBuildService>();
        services.TryAddTransient<IDiscordSlashCommandHandler, DiscordSlashCommandHandler>();

        services.TryAddTransient<IComponentInteractionHandler, ComponentInteractionHandler>();
        services.TryAddTransient<IComponentBuildService, ComponentBuildService>();

        services.TryAddSingleton<IComponentService, ComponentService>();
        services.TryAddSingleton<ISlashCommandService, SlashCommandService>();

        slashCommandConfigs ??= configuration =>
        {
            configuration.EnableAutoSync = true;
            configuration.EnableAutoGetChannel = false;
            configuration.EnableAutoGetGuild = false;
        };
        services.Configure(slashCommandConfigs);

        componentInteractionConfig ??= configuration =>
        {
            configuration.SendDefaultErrorMessage = false;
            configuration.EnableAutoGetChannel = false;
            configuration.EnableAutoGetGuild = false;
        };
        services.Configure(componentInteractionConfig);

        services.AddColorChanCache(defaultCacheConfig, redisCacheOptions);

        return services;
    }

    /// <summary>
    ///     Adds a <see cref="IInteractionPipeline" /> to the <paramref name="services" />.
    /// </summary>
    /// <param name="services">
    ///     The <see cref="IServiceCollection" /> where the <typeparamref name="TPipeline" /> will be added
    ///     to.
    /// </param>
    /// <typeparam name="TPipeline">The new <see cref="IInteractionPipeline" />.</typeparam>
    /// <returns>
    ///     The updated <see cref="IServiceCollection" />.
    /// </returns>
    public static IServiceCollection AddInteractionPipeline<TPipeline>(this IServiceCollection services) where TPipeline : class, IInteractionPipeline
    {
        services.AddTransient<IInteractionPipeline, TPipeline>();
        return services;
    }
}