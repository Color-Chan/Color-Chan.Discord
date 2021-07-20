﻿using Color_Chan.Discord.Commands.Services;
using Color_Chan.Discord.Commands.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Color_Chan.Discord.Commands.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Add the dependencies for Color-Chan.Discord.Command to the <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" />.</param>
        /// <returns>
        ///     The updated <see cref="IServiceCollection" />.
        /// </returns>
        public static IServiceCollection AddColorChanDiscordCommand(this IServiceCollection services)
        {
            services.TryAddTransient<ISlashCommandRequirementBuildService, SlashCommandRequirementBuildService>();
            services.TryAddTransient<ISlashCommandRequirementService, SlashCommandRequirementService>();
            services.TryAddTransient<ISlashCommandOptionBuildService, SlashCommandOptionBuildService>();
            services.TryAddTransient<ISlashCommandGuildBuildService, SlashCommandGuildBuildService>();
            services.TryAddTransient<ISlashCommandAutoSyncService, SlashCommandAutoSyncService>();
            services.TryAddTransient<ISlashCommandBuildService, SlashCommandBuildService>();
            services.TryAddSingleton<ISlashCommandService, SlashCommandService>();

            return services;
        }
    }
}