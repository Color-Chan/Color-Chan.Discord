namespace Color_Chan.Discord.Core.Common.API.DataModels.Interaction;

/// <summary>
///     Represents a discord Interaction Types API model.
///     Docs: https://discord.com/developers/docs/interactions/receiving-and-responding#interaction-object-interaction-type
/// </summary>
public enum DiscordInteractionRequestType : byte
{
    /// <summary>
    ///     A Ping interaction request.
    /// </summary>
    Ping = 1,

    /// <summary>
    ///     An application command request.
    /// </summary>
    ApplicationCommand = 2,

    /// <summary>
    ///     A Message component request.
    /// </summary>
    MessageComponent = 3
}