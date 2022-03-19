using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.Params.Guild
{
    public class DiscordModifyCurrentUserNick
    {
        /// <summary>
        ///     Value to set users nickname to.
        /// </summary>
        /// <remarks>
        ///     Requires MANAGE_NICKNAMES permission
        /// </remarks>
        [JsonPropertyName("nick")]
        public string? Nick { get; set; } = null!;
    }
}