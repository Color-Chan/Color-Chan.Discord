using Color_Chan.Discord.Commands.Attributes.ProvidedRequirements;

namespace Color_Chan.Discord.Commands.Models.Results
{
    /// <summary>
    ///     An error describing when a requirement failed for the <see cref="RequireChannelAttribute" />.
    /// </summary>
    public record RequireChannelErrorResult : InteractionRequirementErrorResult
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="RequireChannelErrorResult" />.
        /// </summary>
        /// <param name="errorMessage">The message of the error.</param>
        public RequireChannelErrorResult(string errorMessage) : base(errorMessage)
        {
        }
    }
}