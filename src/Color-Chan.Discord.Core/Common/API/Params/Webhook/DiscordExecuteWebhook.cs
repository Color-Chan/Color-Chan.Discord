using System.Collections.Generic;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.API.DataModels.Embed;

namespace Color_Chan.Discord.Core.Common.API.Params.Webhook
{
    public record DiscordExecuteWebhook
    {
        /// <summary>
        ///     The message contents (up to 2000 characters).
        /// </summary>
        /// <remarks>
        ///     Required when <see cref="Embeds" /> is not provided.
        /// </remarks>
        [JsonPropertyName("content")]
        public string? Content { get; set; }

        /// <summary>
        ///     Override the default username of the webhook.
        /// </summary>
        [JsonPropertyName("username")]
        public string? Username { get; set; }

        /// <summary>
        ///     Override the default avatar of the webhook.
        /// </summary>
        [JsonPropertyName("avatar_url")]
        public string? AvatarUrl { get; set; }

        /// <summary>
        ///     True if this is a TTS message.
        /// </summary>
        [JsonPropertyName("tts")]
        public bool? UseTts { get; set; }

        /// <summary>
        ///     Embedded rich content.
        /// </summary>
        /// <remarks>
        ///     Required when <see cref="Content" /> is not provided.
        /// </remarks>
        [JsonPropertyName("embeds")]
        public IEnumerable<DiscordEmbedData>? Embeds { get; set; }

        /// <summary>
        ///     Allowed mentions for the message.
        /// </summary>
        [JsonPropertyName("allowed_mentions")]
        public DiscordAllowedMentionsData? AllowedMentions { get; init; }

        /// <summary>
        ///     The components to include with the message.
        /// </summary>
        /// <remarks>
        ///     Requires an application-owned webhook.
        /// </remarks>
        [JsonPropertyName("components")]
        public IEnumerable<DiscordComponentData>? Components { get; set; }
    }
}