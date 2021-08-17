using Color_Chan.Discord.Commands.Models.Contexts;

namespace Color_Chan.Discord.Commands.Configurations
{
    /// <summary>
    ///     This holds all the configurations for the slash commands.
    /// </summary>
    public class SlashCommandConfiguration
    {
        /// <summary>
        ///     Whether or not the slash commands auto sync feature is enabled. Default: true.
        /// </summary>
        public bool EnableAutoSync { get; set; } = true;

        /// <summary>
        ///     Whether or not <see cref="IInteractionContext.Guild" /> should be auto loaded on command requests. Default: false.
        /// </summary>
        public bool EnableAutoGetGuild { get; set; } = false;

        /// <summary>
        ///     Whether or not <see cref="IInteractionContext.Channel" /> should be auto loaded on command requests. Default:
        ///     false.
        /// </summary>
        public bool EnableAutoGetChannel { get; set; } = false;

        /// <summary>
        ///     Whether or not the command handler should send a default error message when a slash command returned
        ///     unsuccessfully. Default: false.
        /// </summary>
        public bool SendDefaultErrorMessage { get; set; } = false;
    }
}