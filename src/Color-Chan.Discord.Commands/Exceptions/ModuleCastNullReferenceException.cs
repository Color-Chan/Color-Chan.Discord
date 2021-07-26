using System;

namespace Color_Chan.Discord.Commands.Exceptions
{
    public class ModuleCastNullReferenceException : NullReferenceException
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="ModuleCastNullReferenceException" />.
        /// </summary>
        /// <param name="message">The error message of the exception.</param>
        public ModuleCastNullReferenceException(string message) : base(message)
        {
        }
    }
}