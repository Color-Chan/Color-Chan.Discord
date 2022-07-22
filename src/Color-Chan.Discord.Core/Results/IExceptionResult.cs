using System;

namespace Color_Chan.Discord.Core.Results;

/// <summary>
///     An exception error result.
/// </summary>
public interface IExceptionResult : IErrorResult
{
    /// <summary>
    ///     Contains the exception that was thrown.
    /// </summary>
    Exception Exception { get; init; }
}