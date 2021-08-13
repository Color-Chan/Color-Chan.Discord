using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Color_Chan.Discord.Caching.Services;
using Color_Chan.Discord.Commands.Attributes.ProvidedRequirements;
using Color_Chan.Discord.Commands.Models;
using Color_Chan.Discord.Commands.Models.Contexts;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Results;
using Color_Chan.Discord.Rest.Models.Interaction;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace Color_Chan.Discord.Commands.Tests.Attributes
{
    [TestFixture]
    public class SlashCommandGuildRateLimitAttributeTests
    {
        [Test]
        public async Task Should_ignore_dm()
        {
            // Arrange
            var rateLimitAttribute = new SlashCommandGuildRateLimitAttribute(20, 60);
            var context = new SlashCommandContext
            {
                GuildId = null,
                CommandRequest = new DiscordInteractionCommand(new DiscordInteractionRequestData
                {
                    Id = 456
                }),
                MethodName = nameof(Should_detect_rate_limit)
            };
            var cacheMock = new Mock<ICacheService>();
            cacheMock.Setup(service => service.GetValueAsync<RateLimitInfo>(It.IsAny<string>()))
                     .ReturnsAsync(Result<RateLimitInfo>.FromSuccess(new RateLimitInfo()));

            var serviceProvider = new ServiceCollection()
                                  .AddSingleton(cacheMock.Object)
                                  .BuildServiceProvider();

            // Act
            var result = await rateLimitAttribute.CheckRequirementAsync(context, serviceProvider);

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [TestCaseSource(nameof(GetRateLimitServers))]
        public async Task Should_detect_rate_limit(Tuple<RateLimitInfo, bool> tuple)
        {
            // Arrange
            var (rateLimitServer, shouldBeRateLimited) = tuple;
            var rateLimitAttribute = new SlashCommandGuildRateLimitAttribute(rateLimitServer.Remaining, 60);
            var context = new SlashCommandContext
            {
                GuildId = ulong.MaxValue,
                CommandRequest = new DiscordInteractionCommand(new DiscordInteractionRequestData
                {
                    Id = 456
                }),
                MethodName = nameof(Should_detect_rate_limit)
            };
            var cacheMock = new Mock<ICacheService>();
            cacheMock.Setup(service => service.GetValueAsync<RateLimitInfo>(It.IsAny<string>()))
                     .ReturnsAsync(Result<RateLimitInfo>.FromSuccess(rateLimitServer));

            var serviceProvider = new ServiceCollection()
                                  .AddSingleton(cacheMock.Object)
                                  .BuildServiceProvider();

            // Act
            var result = await rateLimitAttribute.CheckRequirementAsync(context, serviceProvider);

            // Assert
            result.IsSuccessful.Should().Be(!shouldBeRateLimited);
        }

        protected static IEnumerable<Tuple<RateLimitInfo, bool>> GetRateLimitServers()
        {
            for (var i = 5 - 1; i >= 0; i--)
            {
                yield return new Tuple<RateLimitInfo, bool>(new RateLimitInfo
                {
                    Expiration = DateTimeOffset.UtcNow.AddSeconds(20),
                    Remaining = i
                }, i == 0);
            }
        }

        [Test]
        public async Task Should_detect_new_rate_limit()
        {
            // Arrange
            var rateLimitAttribute = new SlashCommandGuildRateLimitAttribute(2, 60);
            var context = new SlashCommandContext
            {
                GuildId = ulong.MaxValue,
                CommandRequest = new DiscordInteractionCommand(new DiscordInteractionRequestData
                {
                    Id = 456
                }),
                MethodName = nameof(Should_detect_rate_limit)
            };
            var cacheMock = new Mock<ICacheService>();
            cacheMock.Setup(service => service.GetValueAsync<RateLimitInfo>(It.IsAny<string>()))
                     .ReturnsAsync(Result<RateLimitInfo>.FromError(default, new ErrorResult("error message")));

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