using System;

namespace Color_Chan.Discord.Commands
{
    internal class DefaultServiceProvider : IServiceProvider
    {
        /// <summary>
        ///     Get a default implementation for <see cref="IServiceProvider" />.
        /// </summary>
        internal static readonly DefaultServiceProvider Instance = new();

        /// <inheritdoc />
        public object? GetService(Type serviceType)
        {
            return null;
        }
    }
}