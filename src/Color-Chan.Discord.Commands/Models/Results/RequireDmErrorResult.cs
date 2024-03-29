using Color_Chan.Discord.Commands.Attributes.ProvidedRequirements;

namespace Color_Chan.Discord.Commands.Models.Results;

/// <summary>
///     An error describing when a requirement failed for the <see cref="RequireDmAttribute" />.
/// </summary>
public record RequireDmErrorResult : InteractionRequirementErrorResult
{
    /// <summary>
    ///     Initializes a new instance of <see cref="RequireDmErrorResult" />.
    /// </summary>
    /// <param name="errorMessage">The message of the error.</param>
    public RequireDmErrorResult(string errorMessage) : base(errorMessage)
    {
    }
}