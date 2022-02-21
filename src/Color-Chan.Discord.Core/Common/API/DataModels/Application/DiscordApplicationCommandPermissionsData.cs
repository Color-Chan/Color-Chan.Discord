using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.Models.Application;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Application
{
    /// <inheritdoc cref="IDiscordApplicationCommandPermissions"/>
    public class DiscordApplicationCommandPermissionsData
    {
        /// <inheritdoc cref="IDiscordApplicationCommandPermissions.Id"/>
        [JsonPropertyName("id")]
        public ulong Id { get; init; }

        /// <inheritdoc cref="IDiscordApplicationCommandPermissions.Type"/>
        [JsonPropertyName("type")]
        public DiscordApplicationCommandPermissionsType Type { get; init; }

        /// <inheritdoc cref="IDiscordApplicationCommandPermissions.Allow"/>
        [JsonPropertyName("permission")]
        public bool Allow { get; init; }
    }
}