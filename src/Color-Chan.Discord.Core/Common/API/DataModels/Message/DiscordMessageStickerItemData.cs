using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.Models;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Message;

/// <inheritdoc cref="IDiscordMessageStickerItem" />
public record DiscordMessageStickerItemData
{
    /// <inheritdoc cref="IDiscordMessageStickerItem.Id" />
    [JsonPropertyName("id")]
    public ulong Id { get; init; }

    /// <inheritdoc cref="IDiscordMessageStickerItem.Name" />
    [JsonPropertyName("name")]
    public string Name { get; init; } = null!;

    /// <inheritdoc cref="IDiscordMessageStickerItem.FormatType" />
    [JsonPropertyName("format_type")]
    public ulong FormatType { get; init; }
}