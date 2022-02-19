using System.Collections.Generic;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Guild
{
    /// <summary>
    ///     Represents a discord Guild Application Command Permissions Structure API model.
    ///     Docs: https://discord.com/developers/docs/interactions/slash-commands#application-command-permissions-object-guild-application-command-permissions-structure
    /// </summary>
    public class DiscordGuildApplicationCommandPermissionsData
    {
        /// <summary>
        ///     Unique id of the command.
        /// </summary>
        [JsonPropertyName("id")]
        public ulong CommandId { get; init; }

        /// <summary>
        ///     The id of the application the command belongs to.
        /// </summary>
        [JsonPropertyName("application_id")]
        public ulong ApplicationId { get; init; }

        /// <summary>
        ///     The id of the guild.
        /// </summary>
        [JsonPropertyName("guild_id")]
        public ulong GuildId { get; init; }

        /// <summary>
        ///     The permissions for the command in the guild.
        /// </summary>
        [JsonPropertyName("permissions")]
        public IEnumerable<DiscordApplicationCommandPermissionsData> Permissions { get; init; } = new List<DiscordApplicationCommandPermissionsData>();
    }
}