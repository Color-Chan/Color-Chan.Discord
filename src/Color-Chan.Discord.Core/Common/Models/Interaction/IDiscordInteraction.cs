using System.Collections.Generic;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Common.Models.Guild;
using Color_Chan.Discord.Core.Common.Models.Message;

namespace Color_Chan.Discord.Core.Common.Models.Interaction;

/// <summary>
///     An interaction is the base "thing" that is sent when a user invokes a command,
///     and is the same for Slash Commands and other future interaction types (such as Message Components).
///     Represents a discord Interaction Structure API model.
///     Docs:
///     https://discord.com/developers/docs/interactions/receiving-and-responding#interaction-object-interaction-structure
/// </summary>
public interface IDiscordInteraction
{
    /// <summary>
    ///     The Discord provided snowflake id.
    /// </summary>
    ulong Id { get; init; }

    /// <summary>
    ///     Id of the application this interaction is for.
    /// </summary>
    ulong ApplicationId { get; init; }

    /// <summary>
    ///     The type of interaction.
    /// </summary>
    DiscordInteractionRequestType RequestType { get; init; }

    /// <summary>
    ///     The command data payload.
    /// </summary>
    /// <remarks>
    ///     This is always present on application command interaction types.
    ///     It is optional for future-proofing against new interaction types.
    /// </remarks>
    IDiscordInteractionRequest? Data { get; init; }

    /// <summary>
    ///     The guild it was sent from.
    /// </summary>
    ulong? GuildId { get; init; }

    /// <summary>
    ///     The channel it was sent from.
    /// </summary>
    ulong? ChannelId { get; init; }

    /// <summary>
    ///     Guild member data for the invoking user, including permissions.
    /// </summary>
    IDiscordGuildMember? GuildMember { get; init; }

    /// <summary>
    ///     User object for the invoking user, if invoked in a DM.
    /// </summary>
    IDiscordUser? User { get; init; }

    /// <summary>
    ///     A continuation token for responding to the interaction.
    /// </summary>
    string Token { get; init; }

    /// <summary>
    ///     Read-only property, always 1
    /// </summary>
    int Versions { get; init; }

    /// <summary>
    ///     For components, the message they were attached to.
    /// </summary>
    IDiscordMessage? Message { get; init; }

    /// <summary>
    ///     Permissions the app or bot has within the channel the interaction was sent from.
    /// </summary>
    DiscordPermission? Permissions { get; init; }

    /// <summary>
    ///     The ids of the entitlements SKUs attached to the interaction.
    /// </summary>
    IEnumerable<ulong> EntitlementSkuIds { get; init; }

    /// <summary>
    ///    The entitlements attached to the interaction.
    /// </summary>
    IEnumerable<DiscordInteractionData> Entitlements { get; init; }

    /// <summary>
    ///     Checks whether or not the interaction <see cref="RequestType" /> is
    ///     <see cref="DiscordInteractionRequestType.Ping" />.
    /// </summary>
    /// <returns>
    ///     True or false depending on the value of <see cref="RequestType" />.
    /// </returns>
    bool IsPingInteraction();
}