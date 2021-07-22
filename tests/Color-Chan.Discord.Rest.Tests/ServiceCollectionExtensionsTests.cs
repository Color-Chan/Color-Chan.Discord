using System;
using System.Net.Http;
using Color_Chan.Discord.Core.Common.API.Rest;
using Color_Chan.Discord.Rest.Extensions;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Color_Chan.Discord.Rest.Tests
{
    [TestFixture]
    public class ServiceCollectionExtensionsTests
    {
        [SetUp]
        public void SetUp()
        {
            _serviceProvider = new ServiceCollection()
                               .AddColorChanDiscordRest("TOKEN")
                               .BuildServiceProvider();
        }

        private IServiceProvider _serviceProvider = null!;

        [Test]
        public void Should_resolve_new_client()
        {
            // Arrange
            var httpClientFactory = _serviceProvider.GetService<IHttpClientFactory>();

            // Act
            var discordClient1 = httpClientFactory!.CreateClient("Discord");
            var discordClient2 = httpClientFactory!.CreateClient("Discord");

            // Assert.
            discordClient1.Should().NotBeNull();
            discordClient2.Should().NotBeNull();
            discordClient1.BaseAddress.Should().Be(Constants.DiscordApiUrl);
            discordClient2.BaseAddress.Should().Be(Constants.DiscordApiUrl);
            discordClient1.GetHashCode().Should().NotBe(discordClient2.GetHashCode());
        }

        [Test]
        public void Should_resolve()
        {
            // Arrange
            var application = _serviceProvider.GetService<IDiscordRestApplication>();
            application.Should().NotBeNull();
        }
    }
}