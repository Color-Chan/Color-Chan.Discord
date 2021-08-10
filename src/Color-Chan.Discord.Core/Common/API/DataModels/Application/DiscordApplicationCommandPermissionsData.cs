using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Application
{
    /// <summary>
    ///     Represents a discord Application Command Permissions Structure API model.
    ///     https://discord.com/developers/docs/interactions/application-commands#application-command-permissions-object-application-command-permissions-structure
    /// </summary>
    public class DiscordApplicationCommandPermissionsData
    {
        /// <summary>
        ///     The id of the role or user.
        /// </summary>
        [JsonPropertyName("id")]
        public ulong Id { get; init; }

        /// <summary>
        ///     The type of the <see cref="DiscordApplicationCommandPermissionsData" />.
        /// </summary>
        [JsonPropertyName("type")]
        public DiscordApplicationCommandPermissionsType Type { get; init; }

        /// <summary>
        ///     true to allow, false, to disallow.
        /// </summary>
        [JsonPropertyName("permission")]
        public bool Allow { get; init; }
    }
}