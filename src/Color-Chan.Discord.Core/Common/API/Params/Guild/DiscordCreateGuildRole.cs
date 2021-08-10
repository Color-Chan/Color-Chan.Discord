using System.Drawing;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.API.DataModels;

namespace Color_Chan.Discord.Core.Common.API.Params.Guild
{
    public record DiscordCreateGuildRole
    {
        /// <summary>
        ///     The name of the role. Default is "new role"
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; init; } = null!;

        /// <summary>
        ///     The bitwise value of the enabled/disabled permissions. Default is @everyone permissions in guild.
        /// </summary>
        [JsonPropertyName("permissions")]
        public DiscordPermission Permissions { get; init; }

        /// <summary>
        ///     The RGB color value. Default: 0;
        /// </summary>
        [JsonPropertyName("color")]
        public Color Color { get; set; }

        /// <summary>
        ///     Whether the role should be displayed separately in the sidebar. Default: false;
        /// </summary>
        [JsonPropertyName("hoist")]
        public bool IsHoisted { get; set; }

        /// <summary>
        ///     Whether the role should be mentionable. Default: false;
        /// </summary>
        [JsonPropertyName("mentionable")]
        public bool Mentionable { get; set; }
    }
}