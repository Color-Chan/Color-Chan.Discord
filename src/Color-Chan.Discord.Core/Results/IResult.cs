namespace Color_Chan.Discord.Core.Results;

/// <summary>
///     A result object.
/// </summary>
public interface IResult
{
    /// <summary>
    ///     Whether or not the <see cref="IResult" /> is successful.
    /// </summary>
    bool IsSuccessful { get; }

    /// <summary>
    ///     The error of the <see cref="IResult" />.
    /// </summary>
    /// <remarks>
    ///     Null if the result doesn't have an inner result.
    /// </remarks>
    IErrorResult? ErrorResult { get; }

    /// <summary>
    ///     The inner <see cref="IResult" />.
    /// </summary>
    /// <remarks>
    ///     Null if the <see cref="IResult" /> doesn't have an inner <see cref="IResult" />.
    /// </remarks>
    IResult? InnerResult { get; }
}