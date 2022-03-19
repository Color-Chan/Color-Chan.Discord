using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.API.DataModels.Embed;
using Color_Chan.Discord.Core.Common.API.DataModels.Message;

namespace Color_Chan.Discord.Core.Common.API.Params.Channel
{    /// <summary>
    ///     Represents a discord Create Message API request model.
    ///     Docs: https://discord.com/developers/docs/resources/channel#embed-limits-jsonform-params
    /// </summary>
    public class DiscordCreateChannelMessage
    {
        /// <summary>
        ///     The message contents (up to 2000 characters).
        /// </summary>
        /// <remarks>
        ///     Required when no file, embed or sticker is set.
        /// </remarks>
        [JsonPropertyName("content")]
        public string? Content { get; set; }

        /// <summary>
        ///     True if this is a TTS message.
        /// </summary>
        [JsonPropertyName("tts")]
        public bool? Tts { get; set; }

        // Todo: implemented file content support.
        // [JsonPropertyName("file")]
        // public file file { get; set; }

        /// <summary>
        ///     Embedded rich content (up to 6000 characters).
        /// </summary>
        /// <remarks>
        ///     Required when no file, content or sticker is set.
        /// </remarks>
        [JsonPropertyName("embeds")]
        public IEnumerable<DiscordEmbedData>? Embeds { get; set; }

        /// <summary>
        ///     Embedded rich content.
        /// </summary>
        [JsonPropertyName("embed ")]
        [Obsolete("deprecated in favor of embeds")]
        public IEnumerable<DiscordEmbedData>? Embed { get; set; }

        /// <summary>
        ///     Allowed mentions for the message.
        /// </summary>
        [JsonPropertyName("allowed_mentions")]
        public IEnumerable<DiscordAllowedMentionsData>? AllowedMentions { get; set; }

        /// <summary>
        ///     Include to make your message a reply.
        /// </summary>
        [JsonPropertyName("message_reference")]
        public DiscordMessageReferenceData? MessageReference { get; set; }

        /// <summary>
        ///     The components to include with the message.
        /// </summary>
        [JsonPropertyName("components")]
        public IEnumerable<DiscordComponentData>? Components { get; set; }

        /// <summary>
        ///     IDs of up to 3 stickers in the server to send in the message
        /// </summary>
        /// <remarks>
        ///     Required when no file, content or embeds are set.
        /// </remarks>
        [JsonPropertyName("sticker_ids")]
        public IEnumerable<ulong>? StickerIds { get; set; }
    }
}