using System.Collections.Generic;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.API.DataModels.Embed;

namespace Color_Chan.Discord.Core.Common.API.Params.Webhook
{
    /// <summary>
    ///     Represents a discord Edit Webhook Message API request model.
    ///     Docs: https://discord.com/developers/docs/resources/webhook#webhook-object-jsonform-params
    /// </summary>
    public record DiscordEditWebhookMessage
    {
        /// <summary>
        ///     Contents of the message.
        /// </summary>
        [JsonPropertyName("content")]
        public string? Content { get; set; }

        /// <summary>
        ///     Any embedded content.
        /// </summary>
        [JsonPropertyName("embeds")]
        public IEnumerable<DiscordEmbedData>? Embeds { get; set; }

        /// <summary>
        ///     Allowed mentions object.
        /// </summary>
        [JsonPropertyName("allowed_mentions")]
        public DiscordAllowedMentionsData? AllowedMentions { get; init; }

        /// <summary>
        ///     Any attached files.
        /// </summary>
        [JsonPropertyName("attachments")]
        public IEnumerable<DiscordAttachmentData>? Attachments { get; set; }

        /// <summary>
        ///     Sent if the message contains components like buttons, action rows, or other interactive components.
        /// </summary>
        [JsonPropertyName("components")]
        public IEnumerable<DiscordComponentData>? Components { get; set; }
    }
}