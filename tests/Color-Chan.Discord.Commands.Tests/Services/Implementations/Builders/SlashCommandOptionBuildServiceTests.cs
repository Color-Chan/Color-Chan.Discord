using System.Linq;
using Color_Chan.Discord.Commands.Services.Implementations.Builders;
using Color_Chan.Discord.Commands.Tests.Valid;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Color_Chan.Discord.Commands.Tests.Services.Implementations.Builders
{
    [TestFixture]
    public class SlashCommandOptionBuildServiceTests
    {
        [Test]
        public void GetCommandOptions_should_get_2_options()
        {
            // Arrange
            var buildServiceLoggerMock = new Mock<ILogger<SlashCommandOptionBuildService>>();
            var buildService = new SlashCommandOptionBuildService(buildServiceLoggerMock.Object);

            // Act
            var commandOptions = buildService.GetCommandOptions(typeof(ValidMockCommandModule1).GetMethods().First()).ToList();

            // Assert
            commandOptions.Count.Should().Be(2);
            var first = commandOptions.First();
            first.Type.Should().Be(DiscordApplicationCommandOptionType.String);
            first.Name.Should().Be("RoleName");
            first.Description.Should().Be("A RoleName.");
            var last = commandOptions.Last();
            last.Type.Should().Be(DiscordApplicationCommandOptionType.Integer);
            last.Name.Should().Be("Number");
            last.IsRequired.Should().BeFalse();
            last.Description.Should().Be("A random number.");
        }

        [Test]
        public void GetCommandOptions_should_get_2_options_with_choices()
        {
            // Arrange
            var buildServiceLoggerMock = new Mock<ILogger<SlashCommandOptionBuildService>>();
            var buildService = new SlashCommandOptionBuildService(buildServiceLoggerMock.Object);

            // Act
            var commandOptions = buildService.GetCommandOptions(typeof(ValidMockCommandModule1).GetMethods()[1]).ToList();

            // Assert
            commandOptions.Count.Should().Be(2);
            var first = commandOptions.First();

            var index = 1;
            foreach (var choice in first.Choices!)
            {
                choice.Key.Should().Be($"RoleName {index}");
                choice.Value.Should().Be($"RoleName{index}");
                index++;
            }

            var last = commandOptions.Last();
            index = 1;
            foreach (var choice in last.Choices!)
            {
                choice.Key.Should().Be($"Value name {index}");
                choice.Value.Should().Be($"{index}");
                index++;
            }
        }
    }
}