using Color_Chan.Discord.Commands.Attributes.ProvidedRequirements;

namespace Color_Chan.Discord.Commands.Models.Results
{
    /// <summary>
    ///     An error describing when a requirement failed for the <see cref="SlashCommandRequireChannelAttribute" />.
    /// </summary>
    public record SlashCommandRequireChannelErrorResult : SlashCommandRequirementErrorResult
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandRequireChannelErrorResult" />.
        /// </summary>
        /// <param name="errorMessage">The message of the error.</param>
        public SlashCommandRequireChannelErrorResult(string errorMessage) : base(errorMessage)
        {
        }
    }
}