using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;

namespace Color_Chan.Discord.Core.Extensions
{
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