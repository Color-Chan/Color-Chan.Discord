using System;
using Polly;

namespace Color_Chan.Discord.Rest.Extensions
{
    internal static class ContextExtensions
    {
        /// <summary>
        ///     Get the endpoint value from the <see cref="Context" />.
        /// </summary>
        /// <param name="context">The <see cref="Context" />.</param>
        /// <returns>
        ///     The endpoint.
        /// </returns>
        /// <exception cref="InvalidOperationException">Thrown when no endpoint is found.</exception>
        internal static string GetEndpoint(this Context context)
        {
            if (context.TryGetValue("endpoint", out var endpointObject))
                if (endpointObject is string endpoint)
                    return endpoint;

            throw new InvalidOperationException("Failed to find `endpoint`");
        }
        
        /// <summary>
        ///     Get the method value from the <see cref="Context" />.
        /// </summary>
        /// <param name="context">The <see cref="Context" />.</param>
        /// <returns>
        ///     The method.
        /// </returns>
        /// <exception cref="InvalidOperationException">Thrown when no method is found.</exception>
        internal static string GetMethod(this Context context)
        {
            if (context.TryGetValue("method", out var endpointObject))
                if (endpointObject is string endpoint)
                    return endpoint;

            throw new InvalidOperationException("Failed to find `method`");
        }
    }
}