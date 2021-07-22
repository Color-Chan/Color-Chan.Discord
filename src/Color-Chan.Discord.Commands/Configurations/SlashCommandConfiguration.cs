namespace Color_Chan.Discord.Commands.Configurations
{
    public record SlashCommandConfiguration
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandConfiguration" />.
        /// </summary>
        /// <param name="enableAutoSync">Whether or not auto sync for slash commands is enabled.</param>
        public SlashCommandConfiguration(bool enableAutoSync)
        {
            EnableAutoSync = enableAutoSync;
        }

        /// <summary>
        ///     Configurations for the slash commands auto sync feature.
        /// </summary>
        public bool EnableAutoSync { get; set; }

        public static SlashCommandConfiguration Default()
        {
            return new(false);
        }
    }
}