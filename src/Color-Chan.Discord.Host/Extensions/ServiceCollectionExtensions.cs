using Color_Chan.Discord.Extensions;
using Color_Chan.Discord.Host.Services;
using Color_Chan.Discord.Host.Services.Implementations;
using Color_Chan.Discord.Host.Tokens;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Color_Chan.Discord.Host.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Add the dependencies for Color-Chan.Discord.Host to the <see cref="IServiceCollection" />.
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
        public static IServiceCollection AddColorChanDiscordHost(this IServiceCollection services, string botToken, string publicBotToken)
        {
            services.AddColorChanDiscord(botToken);
            services.AddSingleton<IPublicDiscordToken>(serviceProvider => new PublicDiscordToken(publicBotToken));
            services.TryAddTransient<IDiscordInteractionAuthService, DiscordInteractionAuthService>();
            return services;
        }
    }
}