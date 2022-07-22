using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels;

/// <summary>
///     Represents a discord Gateway bot API model.
///     Docs: https://discord.com/developers/docs/topics/gateway#stage-instance-delete-json-response
/// </summary>
public record DiscordGatewayBotData
{
    /// <summary>
    ///     The WSS URL that can be used for connecting to the gateway.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }

    /// <summary>
    ///     The recommended number of shards to use when connecting.
    /// </summary>
    [JsonPropertyName("shards")]
    public int Shards { get; init; }

    /// <summary>
    ///     Information on the current session start limit.
    /// </summary>
    [JsonPropertyName("session_start_limit")]

    public DiscordSessionStartLimitData? SessionStartLimit { get; init; }
}