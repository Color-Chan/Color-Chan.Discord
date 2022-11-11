using System;
using System.Globalization;
using System.Net.Http;
using Color_Chan.Discord.Rest.Models;
using FluentAssertions;
using NUnit.Framework;

namespace Color_Chan.Discord.Rest.Tests.API;

[TestFixture]
public class DiscordRateLimitBucketTests
{
    [TestCase("234", "5", "1626633100", "64.57", "asfd4ytvbnt67ig")]
    [TestCase("10", "5", "1470173023.123", "64.57", "abcd1234")]
    [TestCase("4234234", "0", "3456.453", "234.456", "asdgf3w6sdfgsgxcvb")]
    [TestCase("2342343", "456", "5456776.678", "12323.56", "kgh567dhncvbne4t")]
    [TestCase("645645", "456", "7892345.345", "2345.2", "abxcvb45y4y3457hkhjljk;cd1234")]
    public void Should_parse_normal_request(string limit, string remaining, string resetAt, string resetAfter, string id)
    {
        // Arrange
        var message = new HttpResponseMessage();
        var headers = message.Headers;
        headers.Add("X-RateLimit-Limit", limit);
        headers.Add("X-RateLimit-Remaining", remaining);
        headers.Add("X-RateLimit-Reset", resetAt);
        headers.Add("X-RateLimit-Reset-After", resetAfter);
        headers.Add("X-RateLimit-Bucket", id);

        // Act
        DiscordRateLimitBucket.TryParse(headers, out var bucket);

        // Assert
        bucket.Should().NotBeNull();
        bucket!.Limit.Should().Be(int.Parse(limit));
        bucket.Remaining.Should().Be(int.Parse(remaining));
        bucket.ResetsAt.Should().Be(DateTimeOffset.UnixEpoch + TimeSpan.FromSeconds(double.Parse(resetAt, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture)));
        bucket.ResetsAfter.Should().Be(TimeSpan.FromMilliseconds(double.Parse(resetAfter, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture) * 1000));
        bucket.Id.Should().Be(id);
    }

    [Test]
    public void Should_fail_to_parse_successful_global_request()
    {
        // Arrange
        var message = new HttpResponseMessage();
        var headers = message.Headers;

        // Act
        DiscordRateLimitBucket.TryParse(headers, out var bucket);

        // Assert
        bucket.Should().BeNull();
    }

    [TestCase("34")]
    [TestCase("123")]
    [TestCase("34253")]
    [TestCase("643456")]
    [TestCase("567657567")]
    public void Should_fail_to_parse_rateLimited_global_request(string resetAtString)
    {
        // Arrange
        var message = new HttpResponseMessage();
        var headers = message.Headers;
        headers.Add("Retry-After", resetAtString);
        headers.Add("X-RateLimit-Global", "true");

        // Act
        DiscordRateLimitBucket.TryParse(headers, out var bucket);

        // Assert
        bucket.Should().NotBeNull();
        bucket!.Limit.Should().Be(0);
        bucket.Remaining.Should().Be(0);
        bucket.ResetsAfter.Should().Be(TimeSpan.FromMilliseconds(double.Parse(resetAtString, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture) * 1000));
        bucket.Id.Should().Be(DiscordRateLimitBucket.GlobalBucketId);

        var resetAt = DateTimeOffset.UtcNow + TimeSpan.FromSeconds(int.Parse(resetAtString));
        bucket.ResetsAt.Year.Should().Be(resetAt.Year);
        bucket.ResetsAt.Month.Should().Be(resetAt.Month);
        bucket.ResetsAt.Day.Should().Be(resetAt.Day);
        bucket.ResetsAt.Hour.Should().Be(resetAt.Hour);
        bucket.ResetsAt.Minute.Should().Be(resetAt.Minute);

        // prevent unit test from failing when the test ran slow.
        // This is still accurate enough because it's using epoch time.
        bucket.ResetsAt.Second.Should().BeGreaterOrEqualTo(resetAt.Second - 3);
        bucket.ResetsAt.Second.Should().BeLessOrEqualTo(resetAt.Second + 3);
    }
}