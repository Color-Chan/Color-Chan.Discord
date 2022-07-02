using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Message;

public record DiscordMessageReferenceData
{
    /// <summary>
    ///     Id of the originating message.
    /// </summary>
    [JsonPropertyName("message_id")]
    public ulong? MessageId { get; init; }

    /// <summary>
    ///     Id of the originating message's channel.
    /// </summary>
    /// <remarks>
    ///     channel_id is optional when creating a reply,
    ///     but will always be present when receiving an event/response that includes this data model.
    /// </remarks>
    [JsonPropertyName("channel_id")]
    public ulong? ChannelId { get; init; }

    /// <summary>
    ///     Id of the originating message's guild.
    /// </summary>
    [JsonPropertyName("guild_id")]
    public ulong? GuildId { get; init; }

    /// <summary>
    ///     When sending, whether to error if the referenced message doesn't exist instead of sending as a normal (non-reply)
    ///     message, default true.
    /// </summary>
    [JsonPropertyName("fail_if_not_exists")]
    public ulong? FailIfNotExists { get; init; }
}