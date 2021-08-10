namespace Color_Chan.Discord.Configurations
{
    /// <summary>
    ///     Holds the configurations for interactions.
    /// </summary>
    public class InteractionsConfiguration
    {
        /// <summary>
        ///     Whether or not to response automatically that the interaction command will be handled. Default: true.
        /// </summary>
        /// <remarks>
        ///     This can be useful when some of your commands run for longer then 3 seconds.
        /// </remarks>
        public bool AcknowledgeInteractions { get; set; } = true;
    }
}