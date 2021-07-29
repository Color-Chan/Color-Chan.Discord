using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Color_Chan.Discord.Extensions
{
    /// <summary>
    ///     Contains all the extension methods for <see cref="IApplicationBuilder" />.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        ///     Adds configurations needed for Color-Chan.Discord.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder" />.</param>
        /// <returns>
        ///     The updated <see cref="IApplicationBuilder" />.
        /// </returns>
        public static IApplicationBuilder UseColorChanDiscord(this IApplicationBuilder app)
        {
            // Enables the ability to read the raw body data in a api controller.
            // This is needed for the authentication.
            app.Use((context, next) =>
            {
                context.Request.EnableBuffering();
                return next();
            });

            return app;
        }
    }
}