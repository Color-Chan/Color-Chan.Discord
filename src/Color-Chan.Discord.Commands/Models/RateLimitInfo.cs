using System;

namespace Color_Chan.Discord.Commands.Models;

/// <summary>
///     A record containing the data to detect a rate limit.
/// </summary>
public record RateLimitInfo
{
    /// <summary>
    ///     The amount of requests remaining.
    /// </summary>
    public int Remaining { get; set; }

    /// <summary>
    ///     When this rate limit object is going to expire in the cache.
    /// </summary>
    public DateTimeOffset Expiration { get; set; }
}