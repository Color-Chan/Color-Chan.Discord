using System.Collections.Generic;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.API.DataModels.Select;
using Color_Chan.Discord.Core.Common.Models;

namespace Color_Chan.Discord.Core.Common.API.DataModels;

/// <inheritdoc cref="IDiscordComponent" />
public record DiscordComponentData
{
    /// <inheritdoc cref="IDiscordComponent.Id" />
    [JsonPropertyName("id")]
    public int? Id { get; init; }
    
    /// <inheritdoc cref="IDiscordComponent.Type" />
    [JsonPropertyName("type")]
    public DiscordComponentType Type { get; init; }

    /// <inheritdoc cref="IDiscordComponent.CustomId" />
    [JsonPropertyName("custom_id")]
    public string? CustomId { get; init; }

    /// <inheritdoc cref="IDiscordComponent.Disabled" />
    [JsonPropertyName("disabled")]
    public bool? Disabled { get; init; }

    /// <inheritdoc cref="IDiscordComponent.ButtonStyle" />
    [JsonPropertyName("style")]
    public DiscordButtonStyle? ButtonStyle { get; init; }

    /// <inheritdoc cref="IDiscordComponent.Label" />
    [JsonPropertyName("label")]
    public string? Label { get; init; }

    /// <inheritdoc cref="IDiscordComponent.Emoji" />
    [JsonPropertyName("emoji")]
    public DiscordEmojiData? Emoji { get; init; }

    /// <inheritdoc cref="IDiscordComponent.Url" />
    [JsonPropertyName("url")]
    public string? Url { get; init; }

    /// <inheritdoc cref="IDiscordComponent.SelectOptions" />
    [JsonPropertyName("options")]
    public IEnumerable<DiscordSelectOptionData>? SelectOptions { get; init; }

    /// <inheritdoc cref="IDiscordComponent.Placeholder" />
    [JsonPropertyName("placeholder")]
    public string? Placeholder { get; init; }

    /// <inheritdoc cref="IDiscordComponent.MinValues" />
    [JsonPropertyName("min_values")]
    public int? MinValues { get; init; }

    /// <inheritdoc cref="IDiscordComponent.MaxValues" />
    [JsonPropertyName("max_values")]
    public int? MaxValues { get; init; }
    
    /// <inheritdoc cref="IDiscordComponent.SkuId" />
    [JsonPropertyName("sku_id")]
    public ulong? SkuId { get; init; }
    
    /// <inheritdoc cref="IDiscordComponent.Content" />
    [JsonPropertyName("content")]
    public string? Content { get; init; }
    
    /// <inheritdoc cref="IDiscordComponent.Description" />
    [JsonPropertyName("description")]
    public string? Description { get; init; }
    
    /// <inheritdoc cref="IDiscordComponent.Spoiler" />
    [JsonPropertyName("spoiler")]
    public bool? Spoiler { get; init; }
    
    /// <inheritdoc cref="IDiscordComponent.Items" />
    [JsonPropertyName("items")]
    public IEnumerable<MediaGalleryItemData>? Items { get; init; }
    
    /// <inheritdoc cref="IDiscordComponent.Media" />
    [JsonPropertyName("media")]
    public DiscordUnfurledMediaItemData? Media { get; init; }
    
    /// <inheritdoc cref="IDiscordComponent.Accessory" />
    [JsonPropertyName("accessory")]
    public DiscordComponentData? Accessory { get; init; }

    /// <inheritdoc cref="IDiscordComponent.ChildComponents" />
    [JsonPropertyName("components")]
    public IEnumerable<DiscordComponentData>? ChildComponents { get; init; }
}