using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;

namespace Color_Chan.Discord.Core.Extensions
{
    /// <summary>
    ///     Contains all the extensions methods for <see cref="IServiceCollection" />.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Add the dependencies for Color-Chan.Discord.Core to the <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" />.</param>
        /// <returns>
        ///     The updated <see cref="IServiceCollection" />.
        /// </returns>
        public static IServiceCollection AddColorChanDiscordCore(this IServiceCollection services)
        {
            services.Configure<JsonSerializerOptions>(options => options.RegisterJsonOptions());

            return services;
        }
    }
}