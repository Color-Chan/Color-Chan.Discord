using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;

namespace Color_Chan.Discord.Core.Common.API.Params.Guild;

/// <summary>
///     Represents a discord Create Guild API parameter model.
///     https://discord.com/developers/docs/resources/guild#create-guild-json-params
/// </summary>
public class DiscordCreateGuild
{
    /// <summary>
    ///     Name of the guild (2-100 characters).
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    /// <summary>
    ///     Voice region id (deprecated).
    /// </summary>
    [JsonPropertyName("region")]
    [Obsolete("deprecated")]
    public string? Region { get; set; }

    /// <summary>
    ///     Base64 128x128 image for the guild icon.
    /// </summary>
    [JsonPropertyName("icon")]
    public string? Icon { get; set; }

    /// <summary>
    ///     The <see cref="DiscordGuildVerificationLevel" /> that members will need.
    /// </summary>
    [JsonPropertyName("verification_level")]
    public DiscordGuildVerificationLevel? VerificationLevel { get; set; }

    /// <summary>
    ///     The default message notification level.
    /// </summary>
    [JsonPropertyName("default_message_notifications")]
    public DiscordGuildDefaultMessageNotificationLevel? DefaultMessageNotifications { get; set; }

    /// <summary>
    ///     The explicit content filter level.
    /// </summary>
    [JsonPropertyName("verification_level")]
    public DiscordGuildExplicitContentFilterLevel? ExplicitContentFilter { get; set; }

    /// <summary>
    ///     The new guild roles.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         When using this parameter, the first member of the list is used to change properties of the guild's @everyone
    ///         role.
    ///         If you are trying to bootstrap a guild with additional roles, keep this in mind.
    ///     </para>
    ///     <para>
    ///         When using this parameter, the required id field within each role object is an integer placeholder, and will be
    ///         replaced by the API upon consumption.
    ///         Its purpose is to allow you to overwrite a role's permissions in a channel when also passing in channels with
    ///         the channels array.
    ///     </para>
    /// </remarks>
    [JsonPropertyName("roles")]
    public IEnumerable<DiscordGuildRoleData>? Roles { get; set; }

    /// <summary>
    ///     The new guild's channels.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         When using this parameter, the position field is ignored, and none of the default channels are created.
    ///     </para>
    ///     <para>
    ///         When using this parameter, the id field within each channel object may be set to an integer
    ///         placeholder, and will be replaced by the API upon consumption.
    ///         Its purpose is to allow you to create GUILD_CATEGORY channels by setting the parent_id field on any children to
    ///         the category's id field.
    ///         Category channels must be listed before any children.
    ///     </para>
    /// </remarks>
    [JsonPropertyName("channels")]
    public IEnumerable<DiscordChannelData>? Channels { get; set; }

    /// <summary>
    ///     Id for afk channel.
    /// </summary>
    [JsonPropertyName("afk_channel_id")]
    public ulong AfkChannelId { get; set; }

    /// <summary>
    ///     Afk timeout in seconds.
    /// </summary>
    [JsonPropertyName("afk_timeout")]
    public int AfkTimeout { get; set; }

    /// <summary>
    ///     The id of the channel where guild notices such as welcome messages and boost events are posted.
    /// </summary>
    [JsonPropertyName("system_channel_id")]
    public ulong SystemChannelId { get; set; }

    /// <summary>
    ///     The system channel flags.
    /// </summary>
    [JsonPropertyName("system_channel_flags")]
    public DiscordSystemChannelFlags SystemChannelFlags { get; set; }
}