using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.Params.Channel;

/// <summary>
///     Represents a discord Bulk Delete Messages API request model.
///     Docs: https://discord.com/developers/docs/resources/channel#bulk-delete-messages-json-params
/// </summary>
public class DiscordBulkDeleteMessages
{
    /// <summary>
    ///     an array of message ids to delete (2-100)
    /// </summary>
    [JsonPropertyName("messages")]
    public IReadOnlyCollection<ulong>? MessageIds { get; set; }
}