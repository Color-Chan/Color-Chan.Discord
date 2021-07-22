using System;

namespace Color_Chan.Discord.Commands.Exceptions
{
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