using System.Collections.Generic;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;

namespace Color_Chan.Discord.Core.Common.API.Params.Application
{
    public class DiscordBatchEditApplicationCommandPermissions
    {
        /// <summary>
        ///     The ID of the command.
        /// </summary>
        [JsonPropertyName("id")]
        public ulong CommandId { get; set; }

        /// <summary>
        ///     The permissions for the command in the guild.
        /// </summary>
        [JsonPropertyName("permissions")]
        public IEnumerable<DiscordApplicationCommandPermissionsData> Permissions { get; set; } = new List<DiscordApplicationCommandPermissionsData>();
    }
}