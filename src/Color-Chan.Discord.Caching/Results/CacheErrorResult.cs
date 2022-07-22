using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Caching.Results;

/// <summary>
///     A cache error result.
/// </summary>
public record CacheErrorResult : ErrorResult
{
    /// <summary>
    ///     Initializes a new instance of <see cref="CacheErrorResult" />.
    /// </summary>
    /// <param name="key">The key of the cached value.</param>
    public CacheErrorResult(string key) : base($"{key} does not exist in the cache")
    {
    }
}