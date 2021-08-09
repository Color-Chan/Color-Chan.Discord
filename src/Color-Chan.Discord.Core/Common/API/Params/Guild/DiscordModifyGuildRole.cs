using System.Drawing;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;

namespace Color_Chan.Discord.Core.Common.API.Params.Guild
{
    public record DiscordModifyGuildRole
    {
        /// <summary>
        ///     The name of the role.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; init; }

        /// <summary>
        ///     The bitwise value of the enabled/disabled permissions.
        /// </summary>
        [JsonPropertyName("permissions")]
        public DiscordPermission? Permissions { get; init; }

        /// <summary>
        ///     The RGB color value.
        /// </summary>
        [JsonPropertyName("color")]
        public Color? Color { get; set; }

        /// <summary>
        ///     Whether the role should be displayed separately in the sidebar.
        /// </summary>
        [JsonPropertyName("hoist")]
        public bool? IsHoisted { get; set; }

        /// <summary>
        ///     Whether the role should be mentionable.
        /// </summary>
        [JsonPropertyName("mentionable")]
        public bool? Mentionable { get; set; }
    }
}