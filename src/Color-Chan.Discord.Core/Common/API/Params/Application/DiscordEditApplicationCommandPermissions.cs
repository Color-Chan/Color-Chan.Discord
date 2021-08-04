using System.Collections.Generic;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.Models.Application;

namespace Color_Chan.Discord.Core.Common.API.Params.Application
{
    public class DiscordEditApplicationCommandPermissions
    {
        /// <summary>
        ///     The permissions for the command in the guild.
        /// </summary>
        [JsonPropertyName("permissions")]
        public IEnumerable<IDiscordApplicationCommandPermissions> Permissions { get; set; } = new List<IDiscordApplicationCommandPermissions>();
    }
}