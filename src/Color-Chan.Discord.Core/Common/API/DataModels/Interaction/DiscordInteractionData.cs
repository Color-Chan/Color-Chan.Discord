using System.Collections.Generic;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Core.Common.API.DataModels.Message;
using Color_Chan.Discord.Core.Common.Models.Interaction;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Interaction;

/// <inheritdoc cref="IDiscordInteraction" />
public record DiscordInteractionData
{
    /// <inheritdoc cref="IDiscordInteraction.Id" />
    [JsonPropertyName("id")]
    public ulong Id { get; init; }

    /// <inheritdoc cref="IDiscordInteraction.ApplicationId" />
    [JsonPropertyName("application_id")]
    public ulong ApplicationId { get; init; }

    /// <inheritdoc cref="IDiscordInteraction.RequestType" />
    [JsonPropertyName("type")]
    public DiscordInteractionRequestType RequestType { get; init; }

    /// <inheritdoc cref="IDiscordInteraction.Data" />
    [JsonPropertyName("data")]
    public DiscordInteractionRequestData? Data { get; init; }

    /// <inheritdoc cref="IDiscordInteraction.GuildId" />
    [JsonPropertyName("guild_id")]
    public ulong? GuildId { get; init; }

    /// <inheritdoc cref="IDiscordInteraction.ChannelId" />
    [JsonPropertyName("channel_id")]
    public ulong? ChannelId { get; init; }

    /// <inheritdoc cref="IDiscordInteraction.GuildMember" />
    [JsonPropertyName("member")]
    public DiscordGuildMemberData? GuildMember { get; init; }

    /// <inheritdoc cref="IDiscordInteraction.User" />
    [JsonPropertyName("user")]
    public DiscordUserData? User { get; init; }

    /// <inheritdoc cref="IDiscordInteraction.Token" />
    [JsonPropertyName("token")]
    public string Token { get; set; } = null!;

    /// <inheritdoc cref="IDiscordInteraction.Versions" />
    [JsonPropertyName("version")]
    public int Versions { get; init; }

    /// <inheritdoc cref="IDiscordInteraction.Message" />
    [JsonPropertyName("message")]
    public DiscordMessageData? Message { get; init; }

    /// <inheritdoc cref="IDiscordInteraction.Permissions" />
    [JsonPropertyName("app_permissions")]
    public DiscordPermission? Permissions { get; init; }
    
    /// <inheritdoc cref="IDiscordInteraction.EntitlementSkuIds" />
    [JsonPropertyName("entitlement_sku_ids")]
    public IEnumerable<ulong> EntitlementSkuIds { get; init; } = new List<ulong>();
}