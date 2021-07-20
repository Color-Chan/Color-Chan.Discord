using System;

namespace Color_Chan.Discord.Commands.Exceptions
{
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