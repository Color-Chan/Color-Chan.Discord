
using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Color_Chan.Discord.Caching.Extensions;
using Color_Chan.Discord.Caching.Services;
using Color_Chan.Discord.Rest.Policies;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Polly;

namespace Color_Chan.Discord.Rest.Tests.Policies
{
    [TestFixture]
    public class DiscordRateLimitPolicyTests
    {
        [TestCase("234", "5", "645.1", "64.57", "asfd4ytvbnt67ig", "guilds/234123456778")]
        [TestCase("10", "5", "1470173023.123", "64.57", "abcd1234", "guilds/898904567567")]
        [TestCase("4234234", "543", "3456.453", "234.456", "asdgf3w6sdfgsgxcvb", "guilds/6789567456")]
        [TestCase("2342343", "456", "5456776.678", "12323.56", "kgh567dhncvbne4t", "guilds/45674235345")]
        [TestCase("645645", "456", "7892345.345", "2345.2", "abxcvb45y4y3457hkhjljk;cd1234", "guilds/3245234556567567")]
        public async Task Should_successfully_pass_global_rateLimit_policy(string limit, string remaining, string resetAt, string resetAfter, string id, string endpoint)
        {
            // Arrange
            var services = new ServiceCollection()
                           .AddColorChanCache()
                           .AddLogging()
                           .BuildServiceProvider();
            var policy = new DiscordRateLimitPolicy(services.GetRequiredService<ICacheService>(), services.GetRequiredService<ILogger<DiscordRateLimitPolicy>>());
            var context = new Context { { "endpoint", endpoint } };
            var message = new HttpResponseMessage();

            // Act
            var result = await policy.ExecuteAsync((_, _) => Task.FromResult(message), context, new CancellationToken());

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        
        [TestCase("234", "5", "645.1", "64.57", "asfd4ytvbnt67ig", "guilds/234123456778")]
        [TestCase("10", "5", "1470173023.123", "64.57", "abcd1234", "guilds/898904567567")]
        [TestCase("4234234", "543", "3456.453", "234.456", "asdgf3w6sdfgsgxcvb", "guilds/6789567456")]
        [TestCase("2342343", "456", "5456776.678", "12323.56", "kgh567dhncvbne4t", "guilds/45674235345")]
        [TestCase("645645", "456", "7892345.345", "2345.2", "abxcvb45y4y3457hkhjljk;cd1234", "guilds/3245234556567567")]
        public async Task Should_successfully_pass_rateLimit_policy(string limit, string remaining, string resetAt, string resetAfter, string id, string endpoint)
        {
            // Arrange
            var services = new ServiceCollection()
                           .AddColorChanCache()
                           .AddLogging()
                           .BuildServiceProvider();
            var policy = new DiscordRateLimitPolicy(services.GetRequiredService<ICacheService>(), services.GetRequiredService<ILogger<DiscordRateLimitPolicy>>());
            var context = new Context { { "endpoint", endpoint } };
            var message = new HttpResponseMessage();

            var headers = message.Headers;
            headers.Add("X-RateLimit-Limit", limit);
            headers.Add("X-RateLimit-Remaining", remaining);
            headers.Add("X-RateLimit-Reset", resetAt);
            headers.Add("X-RateLimit-Reset-After", resetAfter);
            headers.Add("X-RateLimit-Bucket", id);

            // Act
            var result = await policy.ExecuteAsync((_, _) => Task.FromResult(message), context, new CancellationToken());

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestCase("234", "guilds/5678564567")]
        [TestCase("456456", "guilds/78908906789678")]
        [TestCase("6789", "guilds/89078967567567")]
        [TestCase("3456", "guilds/8678567434")]
        [TestCase("5678978", "guilds/324234456568")]
        public async Task Should_handle_global_rateLimit(string retryAfter, string endpoint)
        {
            // Arrange
            var services = new ServiceCollection()
                           .AddColorChanCache()
                           .AddLogging()
                           .BuildServiceProvider();
            var policy = new DiscordRateLimitPolicy(services.GetRequiredService<ICacheService>(), services.GetRequiredService<ILogger<DiscordRateLimitPolicy>>());
            var context = new Context { { "endpoint", endpoint } };
            var message = new HttpResponseMessage();
            message.StatusCode = HttpStatusCode.TooManyRequests;

            // Time span should always be in the future.
            var headers = message.Headers;
            headers.Add("Retry-After", retryAfter);
            headers.Add("X-RateLimit-Global", "true");

            // Act
            var result = await policy.ExecuteAsync((_, _) => Task.FromResult(message), context, new CancellationToken());

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.TooManyRequests);
        }

        [TestCase("234", "1", "78545.234", "64.57", "asfd4ytvbnt67ig", "guilds/54678568456")]
        [TestCase("10", "1", "34576.123", "64.57", "abcd1234", "guilds/67894563456")]
        [TestCase("4234234", "1", "46.453", "234.456", "asdgf3w6sdfgsgxcvb", "guilds/789567345")]
        [TestCase("2342343", "1", "3456.678", "12323.56", "kgh567dhncvbne4t", "guilds/2345345656786789")]
        [TestCase("645645", "1", "57645.345", "2345.2", "abxcvb45y4y3457hkhjljk;cd1234", "guilds/678945673456233452")]
        public async Task Should_prevent_rateLimit(string limit, string remaining, string resetAt, string resetAfter, string id, string endpoint)
        {
            // Arrange
            var services = new ServiceCollection()
                           .AddColorChanCache()
                           .AddLogging()
                           .BuildServiceProvider();
            var policy = new DiscordRateLimitPolicy(services.GetRequiredService<ICacheService>(), services.GetRequiredService<ILogger<DiscordRateLimitPolicy>>());       
            var context = new Context { { "endpoint", endpoint } };
            var message = new HttpResponseMessage();
            message.StatusCode = HttpStatusCode.OK;

            // Time span should always be in the future.
            var resetAtTimeSpan = DateTimeOffset.UtcNow.AddSeconds(double.Parse(resetAt, CultureInfo.InvariantCulture));

            var headers = message.Headers;
            headers.Add("X-RateLimit-Limit", limit);
            headers.Add("X-RateLimit-Remaining", remaining);
            headers.Add("X-RateLimit-Reset", resetAtTimeSpan.ToUnixTimeSeconds().ToString());
            headers.Add("X-RateLimit-Reset-After", resetAfter);
            headers.Add("X-RateLimit-Bucket", id);

            // Act
            await policy.ExecuteAsync((_, _) => Task.FromResult(message), context, new CancellationToken()); // 2 remaining

            headers.Remove("X-RateLimit-Remaining");
            headers.Add("X-RateLimit-Remaining", (int.Parse(remaining) - 1).ToString());

            await policy.ExecuteAsync((_, _) => Task.FromResult(message), context, new CancellationToken()); // 1 remaining
            var result = await policy.ExecuteAsync((_, _) => Task.FromResult(message), context, new CancellationToken()); // 0 remaining

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.TooManyRequests);
        }
    }
}