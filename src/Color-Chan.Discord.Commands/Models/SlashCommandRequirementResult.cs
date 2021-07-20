namespace Color_Chan.Discord.Commands.Models
{
    public class SlashCommandRequirementResult
    {
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