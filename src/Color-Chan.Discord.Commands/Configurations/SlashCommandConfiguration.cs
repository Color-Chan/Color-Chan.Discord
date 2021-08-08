using Color_Chan.Discord.Commands.Contexts;

namespace Color_Chan.Discord.Commands.Configurations
{
    /// <summary>
    ///     This holds all the configurations for the slash commands.
    /// </summary>
    public class SlashCommandConfiguration
    {
        /// <summary>
        ///     Whether or not the slash commands auto sync feature is enabled. Default: false.
        /// </summary>
        public bool EnableAutoSync { get; set; }
        
        /// <summary>
        ///     Whether or not <see cref="ISlashCommandContext.Guild"/> should be auto loaded on command requests.
        /// </summary>
        public bool EnableAutoGetGuild { get; set; }
        
        /// <summary>
        ///     Whether or not <see cref="ISlashCommandContext.Channel"/> should be auto loaded on command requests.
        /// </summary>
        public bool EnableAutoGetChannel { get; set; }
    }
}