using System.Linq;
using System.Reflection;
using Color_Chan.Discord.Commands.Services.Builders;
using Color_Chan.Discord.Commands.Services.Implementations.Builders;
using Color_Chan.Discord.Commands.Tests.Invalid;
using Color_Chan.Discord.Commands.Tests.Valid;
using Color_Chan.Discord.Commands.Tests.Valid2;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Color_Chan.Discord.Commands.Tests.Services.Implementations.Builders;

[TestFixture]
public class SlashCommandBuildServiceTests
{
    private static readonly Assembly ValidAssembly1 = typeof(ValidMockCommandModule1).Assembly;
    private static readonly Assembly ValidAssembly2 = typeof(ValidMockCommandModule0).Assembly;
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
        var types = buildService.GetSlashCommandModules(ValidAssembly1);

        // Assert
        types.Count().Should().Be(8);
    }
    
    [Test]
    public void Should_get_interaction_commands_with_multiple_assemblies()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<SlashCommandBuildService>>();
        var requirementBuilderMock = new Mock<ISlashCommandRequirementBuildService>();
        var guildBuilderMock = new Mock<ISlashCommandGuildBuildService>();
        var optionBuilderMock = new Mock<ISlashCommandOptionBuildService>();
        var buildService = new SlashCommandBuildService(requirementBuilderMock.Object, guildBuilderMock.Object, loggerMock.Object, optionBuilderMock.Object);

        // Act
        var commands = buildService.BuildSlashCommandInfos(ValidAssembly1, ValidAssembly2);

        // Assert
        commands.Count.Should().Be(21);
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
        var commands = buildService.BuildSlashCommandInfos(ValidAssembly1);

        // Assert
        commands.Count.Should().Be(20);
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