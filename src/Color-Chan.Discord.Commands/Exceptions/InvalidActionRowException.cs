using System;

namespace Color_Chan.Discord.Commands.Exceptions;

/// <summary>
///     An exception that should be thrown when an action row contains invalid data.
/// </summary>
public class InvalidActionRowException : Exception
{
    /// <summary>
    ///     Initializes a new instance of <see cref="InvalidActionRowException" />.
    /// </summary>
    /// <param name="message">The exception message.</param>
    public InvalidActionRowException(string message) : base(message)
    {
    }
}