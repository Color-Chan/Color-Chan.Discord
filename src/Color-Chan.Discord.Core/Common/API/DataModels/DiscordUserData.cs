using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels;

public record DiscordUserData
{
    /// <summary>
    ///     The user's id.
    /// </summary>
    [JsonPropertyName("id")]
    public ulong Id { get; init; }

    /// <summary>
    ///     The user's username, not unique across the platform.
    /// </summary>
    [JsonPropertyName("username")]
    public string Username { get; init; } = null!;

    /// <summary>
    ///     The user's 4-digit discord-tag
    /// </summary>
    [JsonPropertyName("discriminator")]
    public string Discriminator { get; init; } = null!;

    /// <summary>
    ///     The user's avatar hash.
    /// </summary>
    [JsonPropertyName("avatar")]
    public string? Avatar { get; init; }

    /// <summary>
    ///     Whether the user belongs to an OAuth2 application.
    /// </summary>
    [JsonPropertyName("bot")]
    public bool? IsBot { get; init; }

    /// <summary>
    ///     Whether the user is an Official Discord System user (part of the urgent message system).
    /// </summary>
    [JsonPropertyName("system")]
    public bool? IsSystemUser { get; init; }

    /// <summary>
    ///     Whether the user has two factor enabled on their account.
    /// </summary>
    [JsonPropertyName("mfa_enabled")]
    public bool? HasMfaEnabled { get; init; }

    /// <summary>
    ///     The user's chosen language option.
    /// </summary>
    [JsonPropertyName("locale")]
    public string? Locale { get; init; }

    /// <summary>
    ///     Whether the email on this account has been verified.
    /// </summary>
    [JsonPropertyName("verified")]
    public bool? Verified { get; init; }

    /// <summary>
    ///     The user's email.
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; init; }

    /// <summary>
    ///     The private flags on a user's account.
    /// </summary>
    [JsonPropertyName("flags")]
    public DiscordUserProperties? PrivateFlags { get; init; }

    /// <summary>
    ///     The type of Nitro subscription on a user's account.
    /// </summary>
    [JsonPropertyName("premium_type")]
    public DiscordPremiumType? PremiumType { get; init; }

    /// <summary>
    ///     The public flags on a user's account.
    /// </summary>
    [JsonPropertyName("public_flags")]
    public DiscordUserProperties? PublicFlags { get; init; }
}