using System.Collections.Generic;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.API.DataModels.Embed;
using Color_Chan.Discord.Core.Common.API.DataModels.Message;
using Color_Chan.Discord.Core.Common.Models.Interaction;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Interaction
{
    /// <inheritdoc cref="IDiscordInteractionCallback"/>
    public record DiscordInteractionCallbackData
    {
        /// <inheritdoc cref="IDiscordInteractionCallback.IsTts"/>
        [JsonPropertyName("tts")]
        public bool? IsTts { get; init; }

        /// <inheritdoc cref="IDiscordInteractionCallback.Content"/>
        [JsonPropertyName("content")]
        public string? Content { get; init; }

        /// <inheritdoc cref="IDiscordInteractionCallback.Embeds"/>
        [JsonPropertyName("embeds")]
        public IEnumerable<DiscordEmbedData>? Embeds { get; init; }

        /// <inheritdoc cref="IDiscordInteractionCallback.AllowedMentions"/>
        [JsonPropertyName("allowed_mentions")]
        public DiscordAllowedMentionsData? AllowedMentions { get; init; }

        /// <inheritdoc cref="IDiscordInteractionCallback.Flags"/>
        [JsonPropertyName("flags")]
        public DiscordMessageFlags? Flags { get; init; }

        /// <inheritdoc cref="IDiscordInteractionCallback.Components"/>
        [JsonPropertyName("components")]
        public IEnumerable<DiscordComponentData>? Components { get; init; }
    }
}