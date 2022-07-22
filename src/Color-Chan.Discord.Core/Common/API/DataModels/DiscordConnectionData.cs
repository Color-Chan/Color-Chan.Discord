using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.Models;

namespace Color_Chan.Discord.Core.Common.API.DataModels;

/// <inheritdoc cref="IDiscordConnection" />
public class DiscordConnectionData
{
    /// <inheritdoc cref="IDiscordConnection.Id" />
    [JsonPropertyName("id")]
    public ulong Id { get; set; }

    /// <inheritdoc cref="IDiscordConnection.Name" />
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    /// <inheritdoc cref="IDiscordConnection.Type" />
    [JsonPropertyName("type")]
    public string Type { get; set; } = null!;

    /// <inheritdoc cref="IDiscordConnection.Revoked" />
    [JsonPropertyName("revoked")]
    public bool? Revoked { get; set; }

    // Todo: Add partial integrations.
    // [JsonPropertyName("integrations")]
    // public IReadOnlyList<> integrations { get; set; }

    /// <inheritdoc cref="IDiscordConnection.Verified" />
    [JsonPropertyName("verified")]
    public bool Verified { get; set; }

    /// <inheritdoc cref="IDiscordConnection.FriendSync" />
    [JsonPropertyName("friend_sync")]
    public bool FriendSync { get; set; }

    /// <inheritdoc cref="IDiscordConnection.ShowActivity" />
    [JsonPropertyName("show_activity")]
    public bool ShowActivity { get; set; }

    /// <inheritdoc cref="IDiscordConnection.Visibility" />
    [JsonPropertyName("visibility")]
    public int Visibility { get; set; }
}