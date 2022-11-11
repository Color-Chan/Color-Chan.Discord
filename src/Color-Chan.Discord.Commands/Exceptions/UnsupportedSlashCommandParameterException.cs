using System;

namespace Color_Chan.Discord.Commands.Exceptions;

/// <summary>
///     An exception that should be thrown when a slash command contains an unsupported parameter.
/// </summary>
public class UnsupportedSlashCommandParameterException : ArgumentException
{
    /// <summary>
    ///     Initializes a new instance of <see cref="UnsupportedSlashCommandParameterException" />.
    /// </summary>
    /// <param name="message">The exception message.</param>
    public UnsupportedSlashCommandParameterException(string message) : base(message)
    {
    }
}