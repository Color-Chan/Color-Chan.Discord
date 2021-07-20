using System;
using Polly;

namespace Color_Chan.Discord.Rest.Extensions
{
    public static class ContextExtensions
    {
        /// <summary>
        ///     Get the endpoint value from the <see cref="Context" />.
        /// </summary>
        /// <param name="context">The <see cref="Context" />.</param>
        /// <returns>
        ///     The endpoint.
        /// </returns>
        /// <exception cref="InvalidOperationException">Thrown when no endpoint is found.</exception>
        public static string GetEndpoint(this Context context)
        {
            if (context.TryGetValue("endpoint", out var endpointObject))
                if (endpointObject is string endpoint)
                    return endpoint;

            throw new InvalidOperationException("Failed to find `endpoint`");
        }
    }
}