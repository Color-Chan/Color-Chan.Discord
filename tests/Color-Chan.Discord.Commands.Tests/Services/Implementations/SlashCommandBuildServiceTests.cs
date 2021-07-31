using System.Linq;
using System.Reflection;
using Color_Chan.Discord.Commands.Services;
using Color_Chan.Discord.Commands.Services.Implementations;
using Color_Chan.Discord.Commands.Tests.Invalid;
using Color_Chan.Discord.Commands.Tests.Valid;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Color_Chan.Discord.Commands.Tests.Services.Implementations
{
    [TestFixture]
    public class SlashCommandBuildServiceTests
    {
        private static readonly Assembly ValidAssembly = typeof(ValidMockCommandModule1).Assembly;
        private static readonly Assembly InValidAssembly = typeof(InValidMockCommandModule1).Assembly;

        [Test]
        public void Should_get_interaction_command_modules()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<SlashCommandBuildService>>();
            var requirementBuilderMock = new Mock<ISlashCommandRequirementBuildService>();
            var guildBuilderMock = new Mock<ISlashCommandGuildBuildService>();
            var optionBuilderMock = new Mock<ISlashCommandOptionBuildService>();
            var buildService = new SlashCommandBuildService(requirementBuilderMock.Object, guildBuilderMock.Object, loggerMock.Object, optionBuilderMock.Object);

            // Act
            var types = buildService.GetSlashCommandModules(ValidAssembly);

            // Assert
            types.Count().Should().Be(6);
        }

        [Test]
        public void Should_get_interaction_commands()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<SlashCommandBuildService>>();
            var requirementBuilderMock = new Mock<ISlashCommandRequirementBuildService>();
            var guildBuilderMock = new Mock<ISlashCommandGuildBuildService>();
            var optionBuilderMock = new Mock<ISlashCommandOptionBuildService>();
            var buildService = new SlashCommandBuildService(requirementBuilderMock.Object, guildBuilderMock.Object, loggerMock.Object, optionBuilderMock.Object);

            // Act
            var commands = buildService.BuildSlashCommandInfos(ValidAssembly);

            // Assert
            commands.Count.Should().Be(18);
        }

        [Test]
        public void Should_get_empty_interaction_command_modules()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<SlashCommandBuildService>>();
            var requirementBuilderMock = new Mock<ISlashCommandRequirementBuildService>();
            var guildBuilderMock = new Mock<ISlashCommandGuildBuildService>();
            var optionBuilderMock = new Mock<ISlashCommandOptionBuildService>();
            var buildService = new SlashCommandBuildService(requirementBuilderMock.Object, guildBuilderMock.Object, loggerMock.Object, optionBuilderMock.Object);

            // Act
            var types = buildService.GetSlashCommandModules(InValidAssembly).ToList();

            // Assert
            types.Count.Should().Be(1);
            var inValidModuleType = types.Any(x => x != typeof(InValidMockCommandModule2));
            inValidModuleType.Should().BeTrue();
        }

        [Test]
        public void Should_not_get_interaction_commands()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<SlashCommandBuildService>>();
            var requirementBuilderMock = new Mock<ISlashCommandRequirementBuildService>();
            var guildBuilderMock = new Mock<ISlashCommandGuildBuildService>();
            var optionBuilderMock = new Mock<ISlashCommandOptionBuildService>();
            var buildService = new SlashCommandBuildService(requirementBuilderMock.Object, guildBuilderMock.Object, loggerMock.Object, optionBuilderMock.Object);

            // Act
            var commands = buildService.BuildSlashCommandInfos(InValidAssembly);

            // Assert
            commands.Count.Should().Be(0);
        }
    }
}