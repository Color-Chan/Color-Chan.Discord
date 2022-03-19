using System.Collections.Generic;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;
using Color_Chan.Discord.Core.Common.Models.Guild;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Guild
{
    /// <inheritdoc cref="IDiscordGuildApplicationCommandPermissions"/>
    public class DiscordGuildApplicationCommandPermissionsData
    {
        /// <inheritdoc cref="IDiscordGuildApplicationCommandPermissions.CommandId"/>
        [JsonPropertyName("id")]
        public ulong CommandId { get; init; }

        /// <inheritdoc cref="IDiscordGuildApplicationCommandPermissions.ApplicationId"/>
        [JsonPropertyName("application_id")]
        public ulong ApplicationId { get; init; }

        /// <inheritdoc cref="IDiscordGuildApplicationCommandPermissions.GuildId"/>
        [JsonPropertyName("guild_id")]
        public ulong GuildId { get; init; }

        /// <inheritdoc cref="IDiscordGuildApplicationCommandPermissions.Permissions"/>
        [JsonPropertyName("permissions")]
        public IEnumerable<DiscordApplicationCommandPermissionsData> Permissions { get; init; } = new List<DiscordApplicationCommandPermissionsData>();
    }
}