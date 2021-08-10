using Color_Chan.Discord.Commands.Attributes.ProvidedRequirements;

namespace Color_Chan.Discord.Commands.Models.Results
{
    /// <summary>
    ///     An error describing when a requirement failed for the <see cref="SlashCommandRequireGuildAttribute"/>.
    /// </summary>
    public record SlashCommandRequireGuildErrorResult : SlashCommandRequirementErrorResult
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandRequireGuildErrorResult" />.
        /// </summary>
        /// <param name="errorMessage">The message of the error.</param>
        public SlashCommandRequireGuildErrorResult(string errorMessage) : base(errorMessage)
        {
        }
    }
}