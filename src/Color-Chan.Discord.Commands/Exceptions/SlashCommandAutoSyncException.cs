using System;

namespace Color_Chan.Discord.Commands.Exceptions
{
    public class SlashCommandAutoSyncException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandAutoSyncException" />.
        /// </summary>
        /// <param name="message">The error message of the exception.</param>
        public SlashCommandAutoSyncException(string message) : base(message)
        {
        }
    }
}