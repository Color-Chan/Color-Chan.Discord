using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Core.Common.API.DataModels.Message;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Interaction;

/// <summary>
///     An interaction is the base "thing" that is sent when a user invokes a command,
///     and is the same for Slash Commands and other future interaction types (such as Message Components).
/// </summary>
public record DiscordInteractionData
{
    /// <summary>
    ///     Id of the interaction.
    /// </summary>
    [JsonPropertyName("id")]
    public ulong Id { get; init; }

    /// <summary>
    ///     Id of the application this interaction is for.
    /// </summary>
    [JsonPropertyName("application_id")]
    public ulong ApplicationId { get; init; }

    /// <summary>
    ///     The type of interaction.
    /// </summary>
    [JsonPropertyName("type")]
    public DiscordInteractionRequestType RequestType { get; init; }

    /// <summary>
    ///     The command data payload.
    /// </summary>
    /// <remarks>
    ///     This is always present on application command interaction types.
    ///     It is optional for future-proofing against new interaction types.
    /// </remarks>
    [JsonPropertyName("data")]
    public DiscordInteractionRequestData? Data { get; init; }

    /// <summary>
    ///     The guild it was sent from.
    /// </summary>
    [JsonPropertyName("guild_id")]
    public ulong? GuildId { get; init; }

    /// <summary>
    ///     The channel it was sent from.
    /// </summary>
    [JsonPropertyName("channel_id")]
    public ulong? ChannelId { get; init; }

    /// <summary>
    ///     Guild member data for the invoking user, including permissions.
    /// </summary>
    [JsonPropertyName("member")]
    public DiscordGuildMemberData? GuildMember { get; init; }

    /// <summary>
    ///     User object for the invoking user, if invoked in a DM.
    /// </summary>
    [JsonPropertyName("user")]
    public DiscordUserData? User { get; init; }

    /// <summary>
    ///     A continuation token for responding to the interaction.
    /// </summary>
    [JsonPropertyName("token")]
    public string Token { get; set; } = null!;

    /// <summary>
    ///     Read-only property, always 1
    /// </summary>
    [JsonPropertyName("version")]
    public int Versions { get; init; }

    /// <summary>
    ///     For components, the message they were attached to.
    /// </summary>
    [JsonPropertyName("message")]
    public DiscordMessageData? Message { get; init; }
    
    /// <summary>
    ///     Permissions the app or bot has within the channel the interaction was sent from.
    /// </summary>
    [JsonPropertyName("app_permissions")]
    public DiscordPermission? Permissions { get; init; }
}