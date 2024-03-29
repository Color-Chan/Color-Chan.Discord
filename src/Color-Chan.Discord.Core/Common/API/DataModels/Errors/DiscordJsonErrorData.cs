﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Errors;

/// <summary>
///     Represents a discord Json error response.
/// </summary>
public record DiscordJsonErrorData
{
    /// <summary>
    ///     The <see cref="DiscordJsonError" /> describing the error.
    /// </summary>
    [JsonPropertyName("code")]
    public DiscordJsonError ErrorType { get; init; }

    /// <summary>
    ///     The sub errors.
    /// </summary>
    [JsonPropertyName("errors")]
    public IReadOnlyDictionary<string, PropertyErrorData>? Errors { get; init; }

    /// <summary>
    ///     The error message.
    /// </summary>
    [JsonPropertyName("message")]
    public string Message { get; init; } = null!;
}