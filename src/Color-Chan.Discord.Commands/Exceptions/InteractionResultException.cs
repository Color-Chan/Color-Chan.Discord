using System;

namespace Color_Chan.Discord.Commands.Exceptions;

/// <summary>
///     An exception that should be thrown when a command failed to execute and has not properly been handled.
/// </summary>
public class InteractionResultException : Exception
{
    /// <summary>
    ///     Initializes a new instance of <see cref="InteractionResultException" />.
    /// </summary>
    /// <param name="errorMessage">The error message of the exception.</param>
    public InteractionResultException(string errorMessage) : base(errorMessage)
    {
    }
}