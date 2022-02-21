using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.Models;

namespace Color_Chan.Discord.Core.Common.API.DataModels
{
    /// <inheritdoc cref="IDiscordUser"/>
    public record DiscordUserData
    {
        /// <inheritdoc cref="IDiscordUser.Id"/>
        [JsonPropertyName("id")]
        public ulong Id { get; init; }

        /// <inheritdoc cref="IDiscordUser.Username"/>
        [JsonPropertyName("username")]
        public string Username { get; init; } = null!;

        /// <inheritdoc cref="IDiscordUser.Discriminator"/>
        [JsonPropertyName("discriminator")]
        public string Discriminator { get; init; } = null!;

        /// <inheritdoc cref="IDiscordUser.Avatar"/>
        [JsonPropertyName("avatar")]
        public string? Avatar { get; init; }

        /// <inheritdoc cref="IDiscordUser.IsBot"/>
        [JsonPropertyName("bot")]
        public bool? IsBot { get; init; }

        /// <inheritdoc cref="IDiscordUser.IsSystemUser"/>
        [JsonPropertyName("system")]
        public bool? IsSystemUser { get; init; }

        /// <inheritdoc cref="IDiscordUser.HasMfaEnabled"/>
        [JsonPropertyName("mfa_enabled")]
        public bool? HasMfaEnabled { get; init; }

        /// <inheritdoc cref="IDiscordUser.Locale"/>
        [JsonPropertyName("locale")]
        public string? Locale { get; init; }

        /// <inheritdoc cref="IDiscordUser.Verified"/>
        [JsonPropertyName("verified")]
        public bool? Verified { get; init; }

        /// <inheritdoc cref="IDiscordUser.Email"/>
        [JsonPropertyName("email")]
        public string? Email { get; init; }

        /// <inheritdoc cref="IDiscordUser.PrivateFlags"/>
        [JsonPropertyName("flags")]
        public DiscordUserFlags? PrivateFlags { get; init; }

        /// <inheritdoc cref="IDiscordUser.PremiumType"/>
        [JsonPropertyName("premium_type")]
        public DiscordPremiumType? PremiumType { get; init; }

        /// <inheritdoc cref="IDiscordUser.PublicFlags"/>
        [JsonPropertyName("public_flags")]
        public DiscordUserFlags? PublicFlags { get; init; }
    }
}