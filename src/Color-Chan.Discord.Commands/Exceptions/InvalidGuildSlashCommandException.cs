using System;
using Color_Chan.Discord.Commands.Attributes;

namespace Color_Chan.Discord.Commands.Exceptions
{
    /// <summary>
    ///     An exception that should be thrown when a <see cref="SlashCommandGuildAttribute" /> is incorrectly used.
    /// </summary>
    public class InvalidGuildSlashCommandException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="UpdateSlashCommandException" />.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public InvalidGuildSlashCommandException(string message) : base(message)
        {
        }
    }
}