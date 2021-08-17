using System;

namespace Color_Chan.Discord.Commands.Exceptions
{
    /// <summary>
    ///     An exception that should be thrown when a component interaction failed to execute and has not properly been
    ///     handled.
    /// </summary>
    public class ComponentInteractionResultException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="ComponentInteractionResultException" />.
        /// </summary>
        /// <param name="errorMessage">The error message of the exception.</param>
        public ComponentInteractionResultException(string errorMessage) : base(errorMessage)
        {
        }
    }
}