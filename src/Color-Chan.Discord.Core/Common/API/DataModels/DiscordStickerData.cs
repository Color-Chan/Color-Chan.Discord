using System;
using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels;

/// <summary>
///     Represents a discord Sticker Structure API model.
///     Docs: https://discord.com/developers/docs/resources/sticker#sticker-object-sticker-structure
/// </summary>
public record DiscordStickerData
{
    /// <summary>
    ///     Id of the sticker.
    /// </summary>
    [JsonPropertyName("id")]
    public ulong Id { get; init; }

    /// <summary>
    ///     Id of the pack the sticker is from.
    /// </summary>
    [JsonPropertyName("pack_id")]
    public ulong? PackId { get; init; }

    /// <summary>
    ///     Name of the sticker.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; } = null!;

    /// <summary>
    ///     Description of the sticker.
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; init; } = null!;

    /// <summary>
    ///     For guild stickers, a unicode emoji representing the sticker's expression.
    ///     for nitro stickers, a comma-separated list of related expressions..
    /// </summary>
    [JsonPropertyName("tags")]
    public string Tags { get; init; } = null!;

    /// <summary>
    ///     The sticker asset hash.
    /// </summary>
    [Obsolete("Deprecated previously the sticker asset hash, now an empty string")]
    [JsonPropertyName("asset")]
    public string? Asset { get; init; }

    /// <summary>
    ///     Type of sticker.
    /// </summary>
    [JsonPropertyName("type")]
    public DiscordStickerType Type { get; init; }

    /// <summary>
    ///     Type of sticker format.
    /// </summary>
    [JsonPropertyName("format_type")]
    public DiscordStickerFormatType FormatFormatType { get; init; }

    /// <summary>
    ///     Whether or not the sticker is available.
    /// </summary>
    [JsonPropertyName("available")]
    public bool? Available { get; init; }

    /// <summary>
    ///     id of the guild that owns this sticker.
    /// </summary>
    [JsonPropertyName("guild_id")]
    public ulong? GuildId { get; init; }

    /// <summary>
    ///     The user that uploaded the sticker.
    /// </summary>
    [JsonPropertyName("user")]
    public DiscordUserData? User { get; init; }

    /// <summary>
    ///     A sticker's sort order within a pack.
    /// </summary>
    [JsonPropertyName("sort_value")]
    public int? SortValue { get; init; }
}