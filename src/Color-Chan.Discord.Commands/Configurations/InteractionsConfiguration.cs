namespace Color_Chan.Discord.Commands.Configurations
{
    /// <summary>
    ///     This holds all the configurations for the slash commands.
    /// </summary>
    public class InteractionsConfiguration
    {
        /// <summary>
        ///     Whether or not the slash commands auto sync feature is enabled. Default: false.
        /// </summary>
        public bool EnableAutoSync { get; set; }
        
        /// <summary>
        ///     Whether or not to response automatically that the interaction command will be handled. Default: false.
        /// </summary>
        /// <remarks>
        ///     This can be useful when some of your commands run for longer then 3 seconds.
        /// </remarks>
        public bool AcknowledgeInteractions  { get; set; }
    }
}