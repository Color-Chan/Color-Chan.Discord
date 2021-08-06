using System;
using Color_Chan.Discord.Commands.Extensions;
using Color_Chan.Discord.Commands.Services;
using Color_Chan.Discord.Commands.Services.Implementations;
using Color_Chan.Discord.Core;
using Color_Chan.Discord.Rest.Extensions;
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
        /// <returns>
        ///     The updated <see cref="IServiceCollection" />.
        /// </returns>
        public static IServiceCollection AddColorChanDiscord(this IServiceCollection services, string botToken, string publicBotToken, ulong applicationId)
        {
            if (botToken == null) throw new ArgumentNullException(nameof(botToken));

            services.AddColorChanDiscordRest(botToken);
            services.AddColorChanDiscordCommand();

            services.AddSingleton(_ => new DiscordTokens(botToken, publicBotToken, applicationId));
            services.AddSingleton<IDiscordSlashCommandHandler, DiscordSlashCommandHandler>();
            services.TryAddTransient<IDiscordInteractionAuthService, DiscordInteractionAuthService>();

            return services;
        }
    }
}