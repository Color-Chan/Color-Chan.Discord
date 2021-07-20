using Color_Chan.Discord.Commands.Models;

namespace Color_Chan.Discord.Commands.Configurations
{
    public record SlashCommandConfiguration
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandConfiguration" />.
        /// </summary>
        /// <param name="slashCommandsAutoSync">The settings that will be used for auto sync</param>
        public SlashCommandConfiguration(SlashCommandsAutoSync slashCommandsAutoSync)
        {
            SlashCommandsAutoSync = slashCommandsAutoSync;
        }

        /// <summary>
        ///     Configurations for the slash commands auto sync feature.
        /// </summary>
        public SlashCommandsAutoSync SlashCommandsAutoSync { get; set; }
    }
}