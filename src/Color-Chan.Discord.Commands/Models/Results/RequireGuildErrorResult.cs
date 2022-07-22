using Color_Chan.Discord.Commands.Attributes.ProvidedRequirements;

namespace Color_Chan.Discord.Commands.Models.Results;

/// <summary>
///     An error describing when a requirement failed for the <see cref="RequireGuildAttribute" />.
/// </summary>
public record RequireGuildErrorResult : InteractionRequirementErrorResult
{
    /// <summary>
    ///     Initializes a new instance of <see cref="RequireGuildErrorResult" />.
    /// </summary>
    /// <param name="errorMessage">The message of the error.</param>
    public RequireGuildErrorResult(string errorMessage) : base(errorMessage)
    {
    }
}