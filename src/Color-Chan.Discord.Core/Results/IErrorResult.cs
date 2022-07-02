namespace Color_Chan.Discord.Core.Results;

/// <summary>
///     An error result.
/// </summary>
public interface IErrorResult
{
    /// <summary>
    ///     The message of the error.
    /// </summary>
    public string ErrorMessage { get; set; }
}