using System;
using Color_Chan.Discord.Commands.Attributes;

namespace Color_Chan.Discord.Commands.Exceptions
{
    /// <summary>
    ///     An exception that should be thrown when a parameter for a command is missing the
    ///     <see cref="SlashCommandOptionAttribute" />.
    /// </summary>
    public class NoOptionAttributeArgumentException : ArgumentException
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="NoOptionAttributeArgumentException" />.
        /// </summary>
        /// <param name="errorMessage">The error message of the exception.</param>
        public NoOptionAttributeArgumentException(string errorMessage) : base(errorMessage)
        {
        }
    }
}