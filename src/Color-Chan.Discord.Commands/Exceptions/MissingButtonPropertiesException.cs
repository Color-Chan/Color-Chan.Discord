using System;

namespace Color_Chan.Discord.Commands.Exceptions;

/// <summary>
///     An exception that should be thrown when a button is missing certain properties.
/// </summary>
public class MissingButtonPropertiesException : Exception
{
    /// <summary>
    ///     Initializes a new instance of <see cref="MissingButtonPropertiesException" />.
    /// </summary>
    /// <param name="message">The exception message.</param>
    public MissingButtonPropertiesException(string message) : base(message)
    {
    }
}