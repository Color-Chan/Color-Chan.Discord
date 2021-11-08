using Color_Chan.Discord.Rest.Extensions;
using FluentAssertions;
using NUnit.Framework;

namespace Color_Chan.Discord.Rest.Tests.Extensions
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [TestCase("guilds/123456/members/12345", "123456")]
        [TestCase("guilds/123123123/channels", "123123123")]
        [TestCase("guilds/34563456/members/12345/roles/123123", "34563456")]
        [TestCase("channels/23098475/followers", "23098475")]
        [TestCase("channels/2308947374895/pins/12345", "2308947374895")]
        public void GetMayorParameter_should_return_mayor_params(string endpoint, string mayorParam)
        {
            // Act
            var result = endpoint.GetMayorParameter();

            // Assert
            result.Should().Be(mayorParam);
        }

        [TestCase("stickers/123456")]
        [TestCase("voice/regions")]
        [TestCase("users/@me")]
        [TestCase("users/123123123")]
        [TestCase("stickers/123456")]
        public void GetMayorParameter_should_not_return_mayor_params(string endpoint)
        {
            // Act
            var result = endpoint.GetMayorParameter();

            // Assert
            result.Should().BeNull();
        }
    }
}