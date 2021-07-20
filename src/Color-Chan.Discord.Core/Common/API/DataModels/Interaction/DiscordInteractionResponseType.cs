namespace Color_Chan.Discord.Core.Common.API.DataModels.Interaction
{
    public enum DiscordInteractionResponseType
    {
        /// <summary>
        ///     ACK a Ping.
        /// </summary>
        Pong = 1,

        /// <summary>
        ///     Respond to an interaction with a message.
        /// </summary>
        ChannelMessageWithSource = 4,

        /// <summary>
        ///     ACK an interaction and edit a response later, the user sees a loading state.
        /// </summary>
        DeferredChannelMessageWithSource = 5,

        /// <summary>
        ///     For components, ACK an interaction and edit the original message later; the user does not see a loading state.
        /// </summary>
        DeferredUpdateMessage = 6,

        /// <summary>
        ///     For components, edit the message the component was attached to.
        /// </summary>
        UpdateMessage = 7
    }
}