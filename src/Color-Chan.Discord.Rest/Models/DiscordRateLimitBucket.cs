using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Color_Chan.Discord.Rest.Extensions;

namespace Color_Chan.Discord.Rest.Models;

internal class DiscordRateLimitBucket
{
    internal const string GlobalBucketId = "discord_global_rate_limit_bucket";
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    /// <summary>
    ///     Do not use this constructor, it should ony be used by the DistributedCacheService.
    /// </summary>
    [Obsolete("Do not use this constructor, it should ony be used by the DistributedCacheService.")]
    // ReSharper disable once UnusedMember.Global
    public DiscordRateLimitBucket()
    {
    }

    /// <summary>
    ///     Initializes a new instance of <see cref="DiscordRateLimitBucket" />.
    /// </summary>
    /// <param name="isGlobal">Whether or not the rate limit was a global rate limit.</param>
    /// <param name="limit">The number of requests that can be made.</param>
    /// <param name="remaining">The number of remaining requests that can be made.</param>
    /// <param name="resets">The time at where the rate limit will reset.</param>
    /// <param name="resetsAfter">Total time (in seconds) of when the current rate limit bucket will reset.</param>
    /// <param name="id">The unique string denoting the rate limit being encountered.</param>
    private DiscordRateLimitBucket(bool isGlobal, int limit, int remaining, DateTimeOffset resets, TimeSpan resetsAfter, string id)
    {
        _semaphore = new SemaphoreSlim(1, 1);

        IsGlobal = isGlobal;
        Limit = limit;
        Remaining = remaining;
        ResetsAt = resets;
        ResetsAfter = resetsAfter;
        Id = id;
    }

    /// <summary>
    ///     Whether or not the rate limit was a global rate limit.
    /// </summary>
    /// <remarks>
    ///     Returned only on a HTTP 429 response if the rate limit headers returned are of the global rate limit (not
    ///     per-route).
    /// </remarks>
    public bool IsGlobal { get; set; }

    /// <summary>
    ///     The number of requests that can be made.
    /// </summary>
    public int Limit { get; set; }

    /// <summary>
    ///     The number of remaining requests that can be made.
    /// </summary>
    public int Remaining { get; set; }

    /// <summary>
    ///     Epoch time (seconds since 00:00:00 UTC on January 1, 1970) at which the rate limit resets.
    /// </summary>
    public DateTimeOffset ResetsAt { get; set; }

    /// <summary>
    ///     Total time (in seconds) of when the current rate limit bucket will reset. Can have decimals to match previous
    ///     millisecond rate limit precision.
    /// </summary>
    public TimeSpan ResetsAfter { get; set; }

    /// <summary>
    ///     A unique string denoting the rate limit being encountered (non-inclusive of major parameters in the route path).
    /// </summary>
    public string Id { get; set; } = null!;

    /// <summary>
    ///     Converts the <see cref="HttpResponseHeaders" /> containing the rate limit headers to its
    ///     <see cref="DiscordRateLimitBucket" /> equivalent.
    /// </summary>
    /// <param name="headers">The <see cref="HttpResponseHeaders" /> of the API call.</param>
    /// <param name="bucket">The <see cref="DiscordRateLimitBucket" /> equivalent.</param>
    /// <returns>
    ///     True when the <see cref="DiscordRateLimitBucket" /> was successfully parsed.
    ///     False when it failed to parse the <see cref="HttpResponseHeaders" /> to a <see cref="DiscordRateLimitBucket" />.
    /// </returns>
    public static bool TryParse(HttpResponseHeaders headers, [NotNullWhen(true)] out DiscordRateLimitBucket? bucket)
    {
        bucket = null;

        var isGlobal = headers.Contains("x-rateLimit-global");
        if (isGlobal)
        {
            var retryAfter = headers.TryGetValues("Retry-After", out var temp) &&
                             int.TryParse(temp.SingleOrDefault(), out var retryAfterTemp)
                ? TimeSpan.FromSeconds(retryAfterTemp)
                : TimeSpan.FromSeconds(600);

            bucket = new DiscordRateLimitBucket(isGlobal, 0, 0, DateTimeOffset.UtcNow + retryAfter, retryAfter, GlobalBucketId);
            return true;
        }

        if (!headers.TryGetValues("x-rateLimit-limit", out var rawLimit) || !int.TryParse(rawLimit.SingleOrDefault(), out var limit)) return false;

        if (!headers.TryGetValues("x-rateLimit-remaining", out var rawRemaining) || !int.TryParse(rawRemaining.SingleOrDefault(), out var remaining)) return false;

        if (!headers.TryGetValues("x-rateLimit-reset", out var rawReset) ||
            !double.TryParse(rawReset.SingleOrDefault(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var resetsAtEpoch)) return false;

        if (!headers.TryGetValues("x-rateLimit-reset-after", out var rawResetAfter) ||
            !double.TryParse(rawResetAfter.SingleOrDefault(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var resetsAtTemp)) return false;

        if (!headers.TryParseBucketId(out var id)) return false;

        var resetsAt = DateTimeOffset.UnixEpoch + TimeSpan.FromSeconds(resetsAtEpoch);

        bucket = new DiscordRateLimitBucket(isGlobal, limit, remaining, resetsAt, TimeSpan.FromSeconds(resetsAtTemp), id);
        return true;
    }

    /// <summary>
    ///     Checks whether or not the bucket is available for a new request.
    /// </summary>
    /// <returns>
    ///     Whether or not the bucket is available for a new request..
    /// </returns>
    public async Task<bool> IsAvailableAsync()
    {
        try
        {
            await _semaphore.WaitAsync().ConfigureAwait(false);

            if (Remaining <= 0)
                // Check if the ResetsAt time has already passed.
                return ResetsAt < DateTimeOffset.UtcNow;

            Remaining -= 1;
            return true;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    /// <summary>
    ///     Get a default instance for a global rate limit bucket.
    /// </summary>
    /// <returns>
    ///     A default instance for a global rate limit bucket.
    /// </returns>
    internal static DiscordRateLimitBucket GetDefaultGlobalBucket()
    {
        return new DiscordRateLimitBucket(true, int.MaxValue, int.MaxValue, DateTimeOffset.UtcNow.AddYears(1), TimeSpan.FromDays(1), GlobalBucketId);
    }

    /// <summary>
    ///     Deconstructs the <see cref="DiscordRateLimitBucket" /> so it can be printed out to the console.
    /// </summary>
    public override string ToString()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append('{');

        stringBuilder.Append("isGlobal: ");
        stringBuilder.Append(IsGlobal);
        AddSpace(stringBuilder);

        stringBuilder.Append("limit: ");
        stringBuilder.Append(Limit);
        AddSpace(stringBuilder);

        stringBuilder.Append("remaining: ");
        stringBuilder.Append(Remaining);
        AddSpace(stringBuilder);

        stringBuilder.Append("isGlobal: ");
        stringBuilder.Append(IsGlobal);
        AddSpace(stringBuilder);

        stringBuilder.Append("resetAt: ");
        stringBuilder.Append(ResetsAt.ToString());
        AddSpace(stringBuilder);

        stringBuilder.Append("resetAfter: ");
        stringBuilder.Append(ResetsAfter.ToString());
        AddSpace(stringBuilder);

        stringBuilder.Append("id: ");
        stringBuilder.Append(Id);

        stringBuilder.Append('}');

        static void AddSpace(StringBuilder builder)
        {
            builder.Append(", ");
        }

        return stringBuilder.ToString();
    }
}