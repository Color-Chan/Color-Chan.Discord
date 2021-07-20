using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Color_Chan.Discord.Rest.Extensions;
using Color_Chan.Discord.Rest.Models;
using Polly;

namespace Color_Chan.Discord.Rest.Policies
{
    public class DiscordRateLimitPolicy : AsyncPolicy<HttpResponseMessage>
    {
        private readonly ConcurrentDictionary<string, DiscordRateLimitBucket> _buckets;
        private readonly ConcurrentDictionary<string, string> _endpointBucketIdDictionary;
        private DiscordRateLimitBucket _globalRateLimitBucket;

        /// <summary>
        ///     Initializes a new instance of <see cref="DiscordRateLimitPolicy" />.
        /// </summary>
        public DiscordRateLimitPolicy(int globalLimit, int globalRemaining)
        {
            _endpointBucketIdDictionary = new ConcurrentDictionary<string, string>();
            _buckets = new ConcurrentDictionary<string, DiscordRateLimitBucket>();
            _globalRateLimitBucket = new DiscordRateLimitBucket(true, globalLimit, globalRemaining, DateTimeOffset.UtcNow.AddDays(2), TimeSpan.MinValue, "global");
        }

        /// <inheritdoc />
        protected override async Task<HttpResponseMessage> ImplementationAsync(Func<Context, CancellationToken, Task<HttpResponseMessage>> action, Context context,
            CancellationToken cancellationToken, bool continueOnCapturedContext)
        {
            var endpoint = context.GetEndpoint();

            // Check if the endpoint already has a bucket assigned to it.
            if (!_endpointBucketIdDictionary.TryGetValue(endpoint, out var rateLimitBucketId) || !_buckets.TryGetValue(rateLimitBucketId, out var rateLimitBucket))
                // Assume the endpoint belongs to the global rate limit.
                rateLimitBucket = _globalRateLimitBucket;

            // Check if the bucket is available for the next request.
            var currentTimeOffset = DateTimeOffset.UtcNow;
            if (!await rateLimitBucket.IsAvailableAsync().ConfigureAwait(false))
            {
                // Rate limit request prevented!
                var rateLimitedResponse = new HttpResponseMessage(HttpStatusCode.TooManyRequests);

                var delay = rateLimitBucket.ResetsAt.Subtract(currentTimeOffset);
                rateLimitedResponse.Headers.RetryAfter = new RetryConditionHeaderValue(delay);

                return rateLimitedResponse;
            }

            // Execute the request.
            var response = await action(context, cancellationToken).ConfigureAwait(continueOnCapturedContext);
            if (!DiscordRateLimitBucket.TryParse(response.Headers, out var updatedBucket))
                // Assume the endpoint belongs to the global rate limit and has not reached the limit.
                return response;

            if (response.StatusCode is HttpStatusCode.TooManyRequests)
                // Rate limit reached!
                if (updatedBucket.IsGlobal)
                {
                    _globalRateLimitBucket = updatedBucket;
                    return response;
                }

            // Update the existing bucket.
            _endpointBucketIdDictionary.TryAdd(endpoint, updatedBucket.Id);
            _buckets.AddOrUpdate(updatedBucket.Id, updatedBucket, (_, oldBucket) => oldBucket.ResetsAt < updatedBucket.ResetsAt ? updatedBucket : oldBucket);

            return response;
        }
    }
}