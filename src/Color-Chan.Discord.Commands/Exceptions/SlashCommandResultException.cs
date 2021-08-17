using System;

namespace Color_Chan.Discord.Commands.Exceptions
{
    /// <summary>
    ///     An exception that should be thrown when a command failed to execute and has not properly been handled.
    /// </summary>
    public class SlashCommandResultException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandResultException" />.
        /// </summary>
        /// <param name="errorMessage">The error message of the exception.</param>
        public SlashCommandResultException(string errorMessage) : base(errorMessage)
        {
        }
    }
}