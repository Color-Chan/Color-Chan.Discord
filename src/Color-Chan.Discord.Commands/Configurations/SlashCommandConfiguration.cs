using Color_Chan.Discord.Commands.Models;

namespace Color_Chan.Discord.Commands.Configurations
{
    public record SlashCommandConfiguration
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandConfiguration" />.
        /// </summary>
        public SlashCommandConfiguration()
        {
            SlashCommandsAutoSync = SlashCommandsAutoSync.Disabled;
        }
        
        /// <summary>
        ///     Configurations for the slash commands auto sync feature.
        /// </summary>
        public SlashCommandsAutoSync SlashCommandsAutoSync { get; set; }
    }
}