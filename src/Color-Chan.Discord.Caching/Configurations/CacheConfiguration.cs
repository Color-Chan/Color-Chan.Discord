using System;

namespace Color_Chan.Discord.Caching.Configurations;

/// <summary>
///     Holds the configurations for a cached value.
/// </summary>
public class CacheConfiguration
{
    /// <summary>
    ///     Gets or sets an absolute expiration time, relative to now. Default is 30 seconds.
    /// </summary>
    public TimeSpan AbsoluteExpiration { get; set; } = TimeSpan.FromSeconds(30);

    /// <summary>
    ///     Gets or sets how long a cache entry can be inactive (e.g. not accessed) before it will be removed.
    ///     This will not extend the entry lifetime beyond the absolute expiration (if set). Default is 15 seconds.
    /// </summary>
    public TimeSpan SlidingExpiration { get; set; } = TimeSpan.FromSeconds(15);
}