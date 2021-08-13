using Color_Chan.Discord.Commands.Attributes.ProvidedRequirements;

namespace Color_Chan.Discord.Commands.Models.Results
{
    /// <summary>
    ///     An error describing when a requirement failed for the <see cref="SlashCommandRequireGuildOwnerAttribute" />.
    /// </summary>
    public record SlashCommandRequireGuildOwnerErrorResult : SlashCommandRequirementErrorResult
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandRequireGuildOwnerErrorResult" />.
        /// </summary>
        /// <param name="errorMessage">The message of the error.</param>
        public SlashCommandRequireGuildOwnerErrorResult(string errorMessage) : base(errorMessage)
        {
        }
    }
}