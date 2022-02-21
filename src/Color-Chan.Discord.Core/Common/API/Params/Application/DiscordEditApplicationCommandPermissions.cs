using System.Collections.Generic;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.Models.Application;

namespace Color_Chan.Discord.Core.Common.API.Params.Application
{   
    /// <summary>
    ///     Represents a discord Edit Application Command Permissions API request model.
    ///     Docs: https://discord.com/developers/docs/interactions/application-commands#endpoints-json-params
    /// </summary>
    public class DiscordEditApplicationCommandPermissions
    {
        /// <summary>
        ///     The permissions for the command in the guild.
        /// </summary>
        [JsonPropertyName("permissions")]
        public IEnumerable<IDiscordApplicationCommandPermissions> Permissions { get; set; } = new List<IDiscordApplicationCommandPermissions>();
    }
}