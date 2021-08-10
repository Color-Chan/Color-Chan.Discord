using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Commands.Models.Results
{
    /// <summary>
    ///     An error result describing an error thrown by a <see cref="SlashCommandRequirementAttribute" />.
    /// </summary>
    public record SlashCommandRequirementErrorResult : ErrorResult
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandRequirementErrorResult" />.
        /// </summary>
        /// <param name="errorMessage">The message of the error.</param>
        public SlashCommandRequirementErrorResult(string errorMessage) : base(errorMessage)
        {
        }
    }
}