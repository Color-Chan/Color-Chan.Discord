using System.Threading.Tasks;
using Color_Chan.Discord.Caching.Services;
using Color_Chan.Discord.Commands.Attributes.ProvidedRequirements;
using Color_Chan.Discord.Commands.Commands;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Results;
using Color_Chan.Discord.Rest.Models;
using Color_Chan.Discord.Rest.Models.Interaction;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace Color_Chan.Discord.Commands.Tests.Attributes
{
    [TestFixture]
    public class SlashCommandRateLimitAttributeTests
    {
        /// <param name="remaining">remaining calls.</param>
        /// <param name="expected">whether or not it should have been rate limited.</param>
        [TestCase(2, false)]
        [TestCase(1, false)]
        [TestCase(0, true)]
        public async Task Should_detect_rate_limit(int remaining, bool expected)
        {
            // Arrange
            var rateLimitAttribute = new SlashCommandRateLimitAttribute(remaining, 60);
            var context = new SlashCommandContext
            {
                User = new DiscordUser(new DiscordUserData
                {
                    Id = 123
                }),
                CommandRequest = new DiscordInteractionCommand(new DiscordInteractionCommandData
                {
                    Id = 456
                }),
                MethodName = nameof(Should_detect_rate_limit)
            };
            var cacheMock = new Mock<ICacheService>();
            cacheMock.Setup(service => service.GetValueAsync<int>(It.IsAny<string>())).ReturnsAsync(Result<int>.FromSuccess(remaining));

            var serviceProvider = new ServiceCollection()
                                  .AddSingleton(cacheMock.Object)
                                  .BuildServiceProvider();

            // Act
            var result = await rateLimitAttribute.CheckRequirementAsync(context, serviceProvider);

            // Assert
            result.IsSuccessful.Should().Be(!expected);
        }

        [Test]
        public async Task Should_detect_new_rate_limit()
        {
            // Arrange
            var rateLimitAttribute = new SlashCommandRateLimitAttribute(2, 60);
            var context = new SlashCommandContext
            {
                User = new DiscordUser(new DiscordUserData
                {
                    Id = 123
                }),
                CommandRequest = new DiscordInteractionCommand(new DiscordInteractionCommandData
                {
                    Id = 456
                }),
                MethodName = nameof(Should_detect_rate_limit)
            };
            var cacheMock = new Mock<ICacheService>();
            cacheMock.Setup(service => service.GetValueAsync<int>(It.IsAny<string>())).ReturnsAsync(Result<int>.FromError(default, new ErrorResult("error message")));

            var serviceProvider = new ServiceCollection()
                                  .AddSingleton(cacheMock.Object)
                                  .BuildServiceProvider();

            // Act
            var result = await rateLimitAttribute.CheckRequirementAsync(context, serviceProvider);

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }
    }
}