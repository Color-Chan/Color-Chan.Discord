using System;

namespace Color_Chan.Discord.Core.Results
{
    public interface IExceptionResult : IErrorResult
    {
        /// <summary>
        ///     Contains the exception that was thrown.
        /// </summary>
        Exception Exception { get; init; }
    }
}