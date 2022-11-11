using Color_Chan.Discord.Commands.Attributes.ProvidedRequirements;

namespace Color_Chan.Discord.Commands.Models.Results;

/// <summary>
///     An error describing when a requirement failed for the <see cref="RequireGuildOwnerAttribute" />.
/// </summary>
public record RequireGuildOwnerErrorResult : InteractionRequirementErrorResult
{
    /// <summary>
    ///     Initializes a new instance of <see cref="RequireGuildOwnerErrorResult" />.
    /// </summary>
    /// <param name="errorMessage">The message of the error.</param>
    public RequireGuildOwnerErrorResult(string errorMessage) : base(errorMessage)
    {
    }
}