using System;
using Color_Chan.Discord.Commands.Extensions;
using Color_Chan.Discord.Rest.Extensions;
using Color_Chan.Discord.Services;
using Color_Chan.Discord.Services.Implementations;
using Color_Chan.Discord.Tokens;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Color_Chan.Discord.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Add the dependencies for Color-Chan.Discord to the <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" />.</param>
        /// <param name="botToken">
        ///     The bot token of your application.
        ///     This can be found at https://discord.com/developers/applications/{APPLICATION_ID}/bot
        /// </param>
        /// <param name="publicBotToken">
        ///     The public token of your application.
        ///     This can be found at https://discord.com/developers/applications/{APPLICATION_ID}/information
        /// </param>
        /// <returns>
        ///     The updated <see cref="IServiceCollection" />.
        /// </returns>
        public static IServiceCollection AddColorChanDiscord(this IServiceCollection services, string botToken, string publicBotToken)
        {
            if (botToken == null) throw new ArgumentNullException(nameof(botToken));

            services.AddColorChanDiscordRest(botToken);
            services.AddColorChanDiscordCommand();

            services.AddSingleton<IPublicDiscordToken>(serviceProvider => new PublicDiscordToken(publicBotToken));
            services.TryAddTransient<IDiscordInteractionAuthService, DiscordInteractionAuthService>();
            
            return services;
        }
    }
}