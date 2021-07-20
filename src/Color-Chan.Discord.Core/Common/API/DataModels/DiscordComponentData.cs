using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels
{
    public record DiscordComponentData
    {
        /// <summary>
        ///     The component type.
        /// </summary>
        [JsonPropertyName("type")]
        public DiscordComponentType Type { get; init; }

        /// <summary>
        ///     The style the button.
        /// </summary>
        [JsonPropertyName("style")]
        public DiscordButtonStyle? ButtonStyle { get; init; }

        /// <summary>
        ///     Text that appears on the button, max 80 characters.
        /// </summary>
        [JsonPropertyName("label")]
        public string? Label { get; init; }

        /// <summary>
        ///     Partial emoji data. Name, id, and animated.
        /// </summary>
        [JsonPropertyName("emoji")]
        public DiscordEmojiData? Emoji { get; init; }

        /// <summary>
        ///     A developer-defined identifier for the button, max 100 characters.
        /// </summary>
        [JsonPropertyName("custom_id")]
        public string? CustomId { get; init; }

        /// <summary>
        ///     Url for link-style buttons.
        /// </summary>
        [JsonPropertyName("url")]
        public string? Url { get; init; }

        /// <summary>
        ///     Whether the button is disabled, default false
        /// </summary>
        [JsonPropertyName("disabled")]
        public bool? Disabled { get; init; }

        /// <summary>
        ///     A list of child components.
        /// </summary>
        [JsonPropertyName("components")]
        public IEnumerable<DiscordComponentData>? ChildComponents { get; init; }
    }
}