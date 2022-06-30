using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels;

public record DiscordSessionStartLimitData
{
    /// <summary>
    ///     The total number of session starts the current user is allowed.
    /// </summary>
    [JsonPropertyName("total")]
    public int Total { get; init; }

    /// <summary>
    ///     The remaining number of session starts the current user is allowed.
    /// </summary>
    [JsonPropertyName("remaining")]
    public int Remaining { get; init; }

    /// <summary>
    ///     The number of milliseconds after which the limit resets.
    /// </summary>
    [JsonPropertyName("reset_after")]
    public long ResetAfter { get; init; }

    /// <summary>
    ///     The number of identify requests allowed per 5 seconds.
    /// </summary>
    [JsonPropertyName("max_concurrency")]
    public long MaxConcurrency { get; init; }
}