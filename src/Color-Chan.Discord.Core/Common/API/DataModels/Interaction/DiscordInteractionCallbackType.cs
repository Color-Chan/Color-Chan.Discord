namespace Color_Chan.Discord.Core.Common.API.DataModels.Interaction;

/// <summary>
///     Represents a discord Interaction Callback types API model.
///     Docs:
///     https://discord.com/developers/docs/interactions/receiving-and-responding#interaction-response-object-interaction-callback-type
/// </summary>
public enum DiscordInteractionCallbackType
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
    /// <remarks>
    ///     Only valid for component-based interactions.
    /// </remarks>
    DeferredUpdateMessage = 6,

    /// <summary>
    ///     For components, edit the message the component was attached to.
    /// </summary>
    /// <remarks>
    ///     Only valid for component-based interactions.
    /// </remarks>
    UpdateMessage = 7
}