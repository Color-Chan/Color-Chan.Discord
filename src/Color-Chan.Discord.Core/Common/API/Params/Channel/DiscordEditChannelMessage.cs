using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.API.DataModels.Embed;
using Color_Chan.Discord.Core.Common.API.DataModels.Message;

namespace Color_Chan.Discord.Core.Common.API.Params.Channel;

/// <summary>
///     Represents a discord Edit message API request model.
///     Docs: https://discord.com/developers/docs/resources/channel#embed-limits-jsonform-params
/// </summary>
public class DiscordEditChannelMessage
{
    /// <summary>
    ///     The message contents (up to 2000 characters).
    /// </summary>
    [JsonPropertyName("content")]
    public string? Content { get; set; }

    // Todo: implemented file content support.
    // [JsonPropertyName("file")]
    // public file file { get; set; }

    /// <summary>
    ///     Embedded rich content (up to 6000 characters).
    /// </summary>
    [JsonPropertyName("embeds")]
    public IEnumerable<DiscordEmbedData>? Embeds { get; set; }

    /// <summary>
    ///     Embedded rich content.
    /// </summary>
    [JsonPropertyName("embed ")]
    [Obsolete("deprecated in favor of Embeds")]
    public IEnumerable<DiscordEmbedData>? Embed { get; set; }

    /// <summary>
    ///     Edit the flags of a message (only SUPPRESS_EMBEDS can currently be set/unset).
    /// </summary>
    [JsonPropertyName("flags ")]
    public DiscordMessageFlags? Flags { get; set; }

    /// <summary>
    ///     Allowed mentions for the message.
    /// </summary>
    [JsonPropertyName("allowed_mentions")]
    public IEnumerable<DiscordAllowedMentionsData>? AllowedMentions { get; set; }

    /// <summary>
    ///     Attached files to keep.
    /// </summary>
    [JsonPropertyName("attachments")]
    public IEnumerable<DiscordAttachmentData>? Attachments { get; set; }

    /// <summary>
    ///     The components to include with the message.
    /// </summary>
    [JsonPropertyName("components")]
    public IEnumerable<DiscordComponentData>? Components { get; set; }
}