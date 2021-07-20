using System;
using Color_Chan.Discord.Commands.Extensions;
using Color_Chan.Discord.Rest.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Color_Chan.Discord.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Add the dependencies for Color-Chan.Discord to the <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" />.</param>
        /// <param name="token">The Discord Bot token.</param>
        /// <returns>
        ///     The updated <see cref="IServiceCollection" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="token" /> is null.</exception>
        public static IServiceCollection AddColorChanDiscord(this IServiceCollection services, string token)
        {
            if (token == null) throw new ArgumentNullException(nameof(token));

            services.AddColorChanDiscordRest(token);
            services.AddColorChanDiscordCommand();

            return services;
        }
    }
}