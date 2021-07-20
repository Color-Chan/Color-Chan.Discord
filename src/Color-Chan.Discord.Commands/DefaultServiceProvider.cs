using System;

namespace Color_Chan.Discord.Commands
{
    public class DefaultServiceProvider : IServiceProvider
    {
        /// <summary>
        ///     Get a default implementation for <see cref="IServiceProvider" />.
        /// </summary>
        public static readonly DefaultServiceProvider Instance = new();

        /// <inheritdoc />
        public object? GetService(Type serviceType)
        {
            return null;
        }
    }
}