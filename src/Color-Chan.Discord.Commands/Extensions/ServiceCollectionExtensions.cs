using Color_Chan.Discord.Caching.Extensions;
using Color_Chan.Discord.Commands.Services;
using Color_Chan.Discord.Commands.Services.Builders;
using Color_Chan.Discord.Commands.Services.Implementations;
using Color_Chan.Discord.Commands.Services.Implementations.Builders;
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

            // Call this on your own if you want to supply configurations.
            services.AddColorChanCache();
            
            return services;
        }
    }
}