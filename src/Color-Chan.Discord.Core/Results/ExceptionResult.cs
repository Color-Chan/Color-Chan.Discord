using System;

namespace Color_Chan.Discord.Core.Results;

/// <inheritdoc cref="IExceptionResult" />
public record ExceptionResult : ErrorResult, IExceptionResult
{
    /// <summary>
    ///     Initializes a new instance of <see cref="ExceptionResult" />.
    /// </summary>
    /// <param name="error">The <see cref="System.Exception" />.</param>
    public ExceptionResult(Exception error) : base(error.Message)
    {
        Exception = error;
    }

    /// <inheritdoc />
    public Exception Exception { get; init; }
}