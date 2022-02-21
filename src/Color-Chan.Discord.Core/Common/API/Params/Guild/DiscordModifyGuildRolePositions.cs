using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.Params.Guild
{  
    /// <summary>
    ///     Represents a discord Modify Guild Role Positions API request model.
    ///     Docs: https://discord.com/developers/docs/resources/guild#modify-guild-role-positions
    /// </summary>
    public class DiscordModifyGuildRolePositions
    {
        /// <summary>
        ///     The role id.
        /// </summary>
        [JsonPropertyName("id")]
        public ulong Id { get; set; }

        /// <summary>
        ///     Sorting position of the role.
        /// </summary>
        [JsonPropertyName("position")]
        public int? Position { get; set; }
    }
}