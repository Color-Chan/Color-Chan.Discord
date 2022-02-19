using System.Collections.Generic;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.API.DataModels.Embed;
using Color_Chan.Discord.Core.Common.API.DataModels.Message;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Interaction
{
    public record DiscordInteractionCallbackData
    {
        /// <summary>
        ///     Whether or not the response is TTS.
        /// </summary>
        [JsonPropertyName("tts")]
        public bool? IsTts { get; init; }

        /// <summary>
        ///     The message content.
        /// </summary>
        [JsonPropertyName("content")]
        public string? Content { get; init; }

        /// <summary>
        ///     A list of embed that will be added tot he response.
        /// </summary>
        /// <remarks>
        ///     Supports up to 10 embeds.
        /// </remarks>
        [JsonPropertyName("embeds")]
        public IEnumerable<DiscordEmbedData>? Embeds { get; init; }

        /// <summary>
        ///     Allowed mentions object.
        /// </summary>
        [JsonPropertyName("allowed_mentions")]
        public DiscordAllowedMentionsData? AllowedMentions { get; init; }

        /// <summary>
        ///     Interaction application command callback data flags
        /// </summary>
        /// <remarks>
        ///     Only <see cref="DiscordMessageFlags.SuppressEmbeds"/> and <see cref="DiscordMessageFlags.Ephemeral"/> can be set.
        /// </remarks>
        [JsonPropertyName("flags")]
        public DiscordMessageFlags? Flags { get; init; }

        /// <summary>
        ///     Message components.
        /// </summary>
        [JsonPropertyName("components")]
        public IEnumerable<DiscordComponentData>? Components { get; init; }
    }
}