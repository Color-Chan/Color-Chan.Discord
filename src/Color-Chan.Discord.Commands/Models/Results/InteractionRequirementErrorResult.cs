using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Commands.Models.Results;

/// <summary>
///     An error result describing an error thrown by a <see cref="InteractionRequirementAttribute" />.
/// </summary>
public record InteractionRequirementErrorResult : ErrorResult
{
    /// <summary>
    ///     Initializes a new instance of <see cref="InteractionRequirementErrorResult" />.
    /// </summary>
    /// <param name="errorMessage">The message of the error.</param>
    public InteractionRequirementErrorResult(string errorMessage) : base(errorMessage)
    {
    }
}