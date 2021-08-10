using Color_Chan.Discord.Commands.Attributes.ProvidedRequirements;

namespace Color_Chan.Discord.Commands.Models.Results
{
    /// <summary>
    ///     An error describing when a requirement failed for the <see cref="SlashCommandRequireDmAttribute"/>.
    /// </summary>
    public record SlashCommandRequireDmErrorResult : SlashCommandRequirementErrorResult
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandRequireDmErrorResult" />.
        /// </summary>
        /// <param name="errorMessage">The message of the error.</param>
        public SlashCommandRequireDmErrorResult(string errorMessage) : base(errorMessage)
        {
        }
    }
}