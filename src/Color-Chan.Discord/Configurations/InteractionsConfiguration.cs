using Color_Chan.Discord.Controllers;

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
        /// <remarks>
        ///     Todo: 
        /// </remarks>
        public bool AcknowledgeInteractions { get; set; } = true;

        /// <summary>
        ///     Whether or not the <see cref="DiscordInteractionController" /> should verify the incoming interactions.
        ///     This could be used to test interaction locally. It should never be turned off when the API is public.
        /// </summary>
        public bool VerifyInteractions { get; set; } = true;
    }
}