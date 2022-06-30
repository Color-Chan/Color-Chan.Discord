using System;

namespace Color_Chan.Discord.Commands.Exceptions;

/// <summary>
///     An exception that should be thrown when a command group as no sub command.
/// </summary>
public class InvalidSlashCommandGroupException : Exception
{
    /// <summary>
    ///     Initializes a new instance of <see cref="InvalidSlashCommandGroupException" />.
    /// </summary>
    /// <param name="message">The exception message.</param>
    public InvalidSlashCommandGroupException(string message) : base(message)
    {
    }
}