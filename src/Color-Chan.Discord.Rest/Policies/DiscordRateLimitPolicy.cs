using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Color_Chan.Discord.Caching.Services;
using Color_Chan.Discord.Rest.Exceptions;
using Color_Chan.Discord.Rest.Extensions;
using Color_Chan.Discord.Rest.Models;
using Microsoft.Extensions.Logging;
using Polly;

namespace Color_Chan.Discord.Rest.Policies
{
    internal class DiscordRateLimitPolicy : AsyncPolicy<HttpResponseMessage>
    {
        private readonly ICacheService _cacheService;
        private readonly ILogger<DiscordRateLimitPolicy> _logger;

        /// <summary>
        ///     Initializes a new instance of <see cref="DiscordRateLimitPolicy" />.
        /// </summary>
        // ReSharper disable once UnusedMember.Global used in DI
        public DiscordRateLimitPolicy(ICacheService cacheService, ILogger<DiscordRateLimitPolicy> logger)
        {
            _cacheService = cacheService;
            _logger = logger;
            
            _logger.LogWarning("Initializing global rate limit bucket");
            var globalBucket = new DiscordRateLimitBucket(true, int.MaxValue, int.MaxValue, DateTimeOffset.UtcNow.AddYears(1), TimeSpan.FromDays(1), DiscordRateLimitBucket.GlobalBucketId);
            cacheService.CacheValue(DiscordRateLimitBucket.GlobalBucketId, globalBucket, null, (TimeSpan?) null);
        }

        /// <inheritdoc />
        protected override async Task<HttpResponseMessage> ImplementationAsync(Func<Context, CancellationToken, Task<HttpResponseMessage>> action, Context context,
                                                                               CancellationToken cancellationToken, bool continueOnCapturedContext)
        {
            var endpoint = $"{context.GetMethod()}:{context.GetEndpoint()}";
            
            // Check if the endpoint already has a bucket assigned to it.
            var bucketResult = await _cacheService.GetValueAsync<DiscordRateLimitBucket>(endpoint).ConfigureAwait(false);
            if (!bucketResult.IsSuccessful)
            {
                // Assume the endpoint belongs to the global rate limit.
                _logger.LogDebug("No existing bucket found for endpoint {Endpoint}, using global bucket", endpoint);
                bucketResult = await _cacheService.GetValueAsync<DiscordRateLimitBucket>(DiscordRateLimitBucket.GlobalBucketId).ConfigureAwait(false);
                if (!bucketResult.IsSuccessful)
                {
                    throw new UnknownBucketException($"Failed to fallback on the global rate limit bucket");
                }
            }
            else
            {
                _logger.LogDebug("Existing bucket {Bucket}, found for endpoint {Endpoint}", bucketResult.Entity?.Id, endpoint);
            }

            var rateLimitBucket = bucketResult.Entity ?? throw new NullReferenceException($"{nameof(bucketResult)} can not be null.");
            
            // Check if the bucket is available for the next request.
            var currentTimeOffset = DateTimeOffset.UtcNow;
            if (!await rateLimitBucket.IsAvailableAsync().ConfigureAwait(false))
            {
                // Rate limit request prevented!
                LogRateLimit(rateLimitBucket, endpoint, true);
                var rateLimitedResponse = new HttpResponseMessage(HttpStatusCode.TooManyRequests);
                var delay = rateLimitBucket.ResetsAt.Subtract(currentTimeOffset);
                rateLimitedResponse.Headers.RetryAfter = new RetryConditionHeaderValue(delay);
                
                return rateLimitedResponse;
            }

            // Execute the request.
            var response = await action(context, cancellationToken).ConfigureAwait(continueOnCapturedContext);
            if (!DiscordRateLimitBucket.TryParse(response.Headers, out var updatedBucket))
            {
                // Assume the endpoint belongs to the global rate limit and has not reached the limit.
                return response;
            }

            if (response.StatusCode is HttpStatusCode.TooManyRequests)
            {
                // Rate limit reached!
                LogRateLimit(updatedBucket, endpoint, false);
                if (updatedBucket.IsGlobal)
                {
                    await _cacheService.CacheValueAsync(DiscordRateLimitBucket.GlobalBucketId, updatedBucket, null, updatedBucket.ResetsAfter);
                    return response;
                }
            }
            
            // An non rate limited global request will never get here!!!!!!
            
            // Update the existing bucket.
            await _cacheService.CacheValueAsync(endpoint, updatedBucket, null, updatedBucket.ResetsAfter);
            return response;
        }

        private void LogRateLimit(DiscordRateLimitBucket bucket, string endpoint, bool prevented)
        {
            _logger.LogWarning("A rate limit has been {Type}, Bucket: {Bucket}, Endpoint: {Endpoint}, Resets after: {Time}s",
                               prevented ? "prevented" : "hit",
                               bucket.Id,
                               endpoint,
                               bucket.ResetsAfter.TotalSeconds.ToString(CultureInfo.InvariantCulture));
        }
    }
}