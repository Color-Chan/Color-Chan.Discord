using System.Linq;
using Color_Chan.Discord.Commands.Services.Implementations;
using Color_Chan.Discord.Commands.Services.Implementations.Builders;
using Color_Chan.Discord.Commands.Tests.Valid;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Color_Chan.Discord.Commands.Tests.Services.Implementations
{
    [TestFixture]
    public class SlashCommandGuildBuildServiceTests
    {
        [Test]
        public void GetCommandRequirements_should_get_2_guildAttributes()
        {
            // Arrange
            var buildServiceLoggerMock = new Mock<ILogger<SlashCommandGuildBuildService>>();
            var buildService = new SlashCommandGuildBuildService(buildServiceLoggerMock.Object);

            // Act
            var guildAttributes = buildService.GetCommandGuilds(typeof(ValidMockCommandModule1).GetMethods().First());

            // Assert
            guildAttributes.Count().Should().Be(2);
        }

        [Test]
        public void GetCommandRequirements_should_get_2_guildAttributes_with_values()
        {
            // Arrange
            var buildServiceLoggerMock = new Mock<ILogger<SlashCommandGuildBuildService>>();
            var buildService = new SlashCommandGuildBuildService(buildServiceLoggerMock.Object);

            // Act
            var guildAttributes = buildService.GetCommandGuilds(typeof(ValidMockCommandModule1).GetMethods().First()).ToList();

            // Assert
            guildAttributes.Count.Should().Be(2);
            guildAttributes.First().GuildId.Should().Be(ulong.MinValue);
            guildAttributes.Last().GuildId.Should().Be(ulong.MaxValue);
        }
    }
}