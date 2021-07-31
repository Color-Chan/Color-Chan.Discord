using System;

namespace Color_Chan.Discord.Commands.Exceptions
{
    /// <summary>
    ///     An exception that should be thrown when an error occured during the syncing process of the slash commands.
    /// </summary>
    public class UpdateSlashCommandException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="UpdateSlashCommandException" />.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public UpdateSlashCommandException(string message) : base(message)
        {
        }
    }
}