namespace Color_Chan.Discord.Commands.Models
{
    /// <summary>
    ///     Contains a result for a slash command requirement.
    /// </summary>
    public class SlashCommandRequirementResult
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandRequirementResult" />.
        /// </summary>
        /// <param name="passed">Whether or not the requirement was passed.</param>
        /// <param name="errorMessage">The error message if the requirement failed.</param>
        public SlashCommandRequirementResult(bool passed, string? errorMessage = null)
        {
            Passed = passed;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        ///     The message describing the issue if the requirement has not been met.
        /// </summary>
        /// <remarks>
        ///     Only used when <see cref="Passed" /> returns false.
        /// </remarks>
        public string? ErrorMessage { get; init; }

        /// <summary>
        ///     Whether or not the requirement was passed.
        /// </summary>
        public bool Passed { get; set; }
    }
}