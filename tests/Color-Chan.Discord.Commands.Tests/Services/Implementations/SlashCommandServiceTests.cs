using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Models.Contexts;
using Color_Chan.Discord.Commands.Services;
using Color_Chan.Discord.Commands.Services.Builders;
using Color_Chan.Discord.Commands.Services.Implementations;
using Color_Chan.Discord.Commands.Services.Implementations.Builders;
using Color_Chan.Discord.Commands.Tests.Valid;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;
using Color_Chan.Discord.Rest.Models.Interaction;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Color_Chan.Discord.Commands.Tests.Services.Implementations
{
    [TestFixture]
    public class SlashCommandServiceTests
    {
        private static readonly Assembly ValidAssembly = typeof(ValidMockCommandModule1).Assembly;
        private readonly Mock<ILogger<SlashCommandBuildService>> _buildServiceLoggerMock = new();
        private readonly Mock<ILogger<SlashCommandRequirementBuildService>> _requirementBuildServiceLoggerMock = new();
        private readonly Mock<ILogger<SlashCommandService>> _commandServiceLoggerMock = new();
        private readonly Mock<ILogger<SlashCommandRequirementService>> _requirementServiceLoggerMock = new();
        private readonly Mock<ILogger<SlashCommandOptionBuildService>> _optionBuilderServiceLoggerMock = new();

        [TestCase("Command1", "CommandMethod1Async")]
        [TestCase("Command2", "CommandMethod2Async")]
        [TestCase("Command3", "CommandMethod3Async")]
        [TestCase("Command4", "CommandMethod4Async")]
        [TestCase("Command5", "CommandMethod5Async")]
        [TestCase("Command6", "CommandMethod6Async")]
        [TestCase("Command7", "CommandMethod7Async")]
        public void Should_search_interaction_command(string commandName, string methodName)
        {
            // Arrange
            var requirementBuilderMock = new Mock<ISlashCommandRequirementBuildService>();
            var requirementServiceMock = new Mock<ISlashCommandRequirementService>();
            var guildBuilderMock = new Mock<ISlashCommandGuildBuildService>();
            var optionBuilderMock = new Mock<ISlashCommandOptionBuildService>();
            var buildService = new SlashCommandBuildService(requirementBuilderMock.Object, guildBuilderMock.Object, _buildServiceLoggerMock.Object, optionBuilderMock.Object);
            var autoSyncMock = new Mock<ISlashCommandAutoSyncService>();
            var commandService = new SlashCommandService(_commandServiceLoggerMock.Object, buildService, requirementServiceMock.Object, autoSyncMock.Object);

            // Act
            commandService.AddInteractionCommandsAsync(ValidAssembly);
            var command = commandService.SearchSlashCommand(commandName);

            // Assert
            command.Should().NotBeNull();
            command!.CommandMethod!.Name.Should().Be(methodName);
        }

        [TestCase("add", "role", "Command18")]
        public void Should_search_interaction_command_with_options(string commandGroupName, string subCommandGroupName, string commandName)
        {
            // Arrange
            var requirementBuilderMock = new Mock<ISlashCommandRequirementBuildService>();
            var requirementServiceMock = new Mock<ISlashCommandRequirementService>();
            var guildBuilderMock = new Mock<ISlashCommandGuildBuildService>();
            var optionBuilder = new SlashCommandOptionBuildService(_optionBuilderServiceLoggerMock.Object);
            var buildService = new SlashCommandBuildService(requirementBuilderMock.Object, guildBuilderMock.Object, _buildServiceLoggerMock.Object, optionBuilder);
            var autoSyncMock = new Mock<ISlashCommandAutoSyncService>();
            var commandService = new SlashCommandService(_commandServiceLoggerMock.Object, buildService, requirementServiceMock.Object, autoSyncMock.Object);

            // Act
            commandService.AddInteractionCommandsAsync(ValidAssembly);
            var command = commandService.SearchSlashCommand(commandGroupName, subCommandGroupName, commandName);

            // Assert
            command.Should().NotBeNull();
            command!.CommandOptions!.Count.Should().Be(2);
        }

        [TestCase("CommandWithError1", "Command error 1")]
        [TestCase("CommandWithError2", "Command error 2")]
        public async Task Should_execute_and_fail_interaction_command(string commandName, string errorReason)
        {
            // Arrange
            var requirementServiceMock = new Mock<ISlashCommandRequirementService>();
            requirementServiceMock
                .Setup(x => x.ExecuteSlashCommandRequirementsAsync(It.IsAny<IEnumerable<SlashCommandRequirementAttribute>>(), It.IsAny<ISlashCommandContext>(), It.IsAny<IServiceProvider>()))
                .ReturnsAsync(Result.FromSuccess);
            var requirementBuilderMock = new Mock<ISlashCommandRequirementBuildService>();
            var guildBuilderMock = new Mock<ISlashCommandGuildBuildService>();
            var optionBuilderMock = new Mock<ISlashCommandOptionBuildService>();
            var buildService = new SlashCommandBuildService(requirementBuilderMock.Object, guildBuilderMock.Object, _buildServiceLoggerMock.Object, optionBuilderMock.Object);
            var autoSyncMock = new Mock<ISlashCommandAutoSyncService>();
            var commandService = new SlashCommandService(_commandServiceLoggerMock.Object, buildService, requirementServiceMock.Object, autoSyncMock.Object);
            var mockContext = new SlashCommandContext
            {
                SlashCommandName = new[] { commandName }
            };

            // Act
            await commandService.AddInteractionCommandsAsync(ValidAssembly).ConfigureAwait(false);
            var result = await commandService.ExecuteSlashCommandAsync(mockContext);

            // Assert
            result.IsSuccessful.Should().BeFalse();
            result.ErrorResult!.ErrorMessage.Should().Be(errorReason);
            (result.ErrorResult as ExceptionResult)!.Exception.GetType().Should().Be(typeof(Exception));
        }

        [TestCase("Command10")]
        [TestCase("Command11")]
        [TestCase("Command12")]
        [TestCase("Command13")]
        public async Task Should_execute_interaction_command_with_dependency(string commandName)
        {
            // Arrange
            var requirementServiceMock = new Mock<ISlashCommandRequirementService>();
            requirementServiceMock
                .Setup(x => x.ExecuteSlashCommandRequirementsAsync(It.IsAny<IEnumerable<SlashCommandRequirementAttribute>>(), It.IsAny<ISlashCommandContext>(), It.IsAny<IServiceProvider>()))
                .ReturnsAsync(Result.FromSuccess);
            var requirementBuilderMock = new Mock<ISlashCommandRequirementBuildService>();
            var guildBuilderMock = new Mock<ISlashCommandGuildBuildService>();
            var optionBuilderMock = new Mock<ISlashCommandOptionBuildService>();
            var buildService = new SlashCommandBuildService(requirementBuilderMock.Object, guildBuilderMock.Object, _buildServiceLoggerMock.Object, optionBuilderMock.Object);
            var autoSyncMock = new Mock<ISlashCommandAutoSyncService>();
            var commandService = new SlashCommandService(_commandServiceLoggerMock.Object, buildService, requirementServiceMock.Object, autoSyncMock.Object);
            var mockContext = new SlashCommandContext
            {
                SlashCommandName = new[] { commandName }
            };
            var serviceProvider = new ServiceCollection()
                                  .AddLogging()
                                  .BuildServiceProvider();

            // Act
            await commandService.AddInteractionCommandsAsync(ValidAssembly).ConfigureAwait(false);
            var result = await commandService.ExecuteSlashCommandAsync(mockContext, null, serviceProvider);

            // Assert
            result.IsSuccessful.Should().BeTrue();
            result.Entity!.Type.Should().Be(DiscordInteractionResponseType.ChannelMessageWithSource);
        }

        [TestCase("Command14")]
        [TestCase("Command15")]
        [TestCase("Command16")]
        [TestCase("Command17")]
        public async Task Should_execute_interaction_command_with_requirement(string commandName)
        {
            // Arrange
            var requirementBuilder = new SlashCommandRequirementBuildService(_requirementBuildServiceLoggerMock.Object);
            var guildBuilderMock = new Mock<ISlashCommandGuildBuildService>();
            var optionBuilderMock = new Mock<ISlashCommandOptionBuildService>();
            var buildService = new SlashCommandBuildService(requirementBuilder, guildBuilderMock.Object, _buildServiceLoggerMock.Object, optionBuilderMock.Object);
            var requirementService = new SlashCommandRequirementService(_requirementServiceLoggerMock.Object);
            var autoSyncMock = new Mock<ISlashCommandAutoSyncService>();
            var commandService = new SlashCommandService(_commandServiceLoggerMock.Object, buildService, requirementService, autoSyncMock.Object);
            var mockContext = new SlashCommandContext
            {
                SlashCommandName = new[] { commandName }
            };

            // Act
            await commandService.AddInteractionCommandsAsync(ValidAssembly).ConfigureAwait(false);
            var result = await commandService.ExecuteSlashCommandAsync(mockContext);

            // Assert
            result.IsSuccessful.Should().BeTrue();
            result.Entity!.Type.Should().Be(DiscordInteractionResponseType.ChannelMessageWithSource);
        }

        [TestCase("Command14", "CommandMethod14Async", 7)]
        [TestCase("Command15", "CommandMethod15Async", 6)]
        [TestCase("Command16", "CommandMethod16Async", 8)]
        [TestCase("Command17", "CommandMethod17Async", 6)]
        public void Should_search_interaction_command_with_requirement(string commandName, string methodName, int requirementAmount)
        {
            // Arrange
            var requirementBuilder = new SlashCommandRequirementBuildService(_requirementBuildServiceLoggerMock.Object);
            var guildBuilderMock = new Mock<ISlashCommandGuildBuildService>();
            var optionBuilderMock = new Mock<ISlashCommandOptionBuildService>();
            var buildService = new SlashCommandBuildService(requirementBuilder, guildBuilderMock.Object, _buildServiceLoggerMock.Object, optionBuilderMock.Object);
            var requirementService = new SlashCommandRequirementService(_requirementServiceLoggerMock.Object);
            var autoSyncMock = new Mock<ISlashCommandAutoSyncService>();
            var commandService = new SlashCommandService(_commandServiceLoggerMock.Object, buildService, requirementService, autoSyncMock.Object);

            // Act
            commandService.AddInteractionCommandsAsync(ValidAssembly);
            var command = commandService.SearchSlashCommand(commandName);

            // Assert
            command.Should().NotBeNull();
            command!.CommandMethod!.Name.Should().Be(methodName);
            command.Requirements?.Count().Should().Be(requirementAmount);
        }

        [TestCase("add", "role", "Command18")]
        public async Task Should_execute_interaction_command(string commandGroupName, string subCommandGroupName, string commandName)
        {
            // Arrange
            var requirementServiceMock = new Mock<ISlashCommandRequirementService>();
            requirementServiceMock
                .Setup(x => x.ExecuteSlashCommandRequirementsAsync(It.IsAny<IEnumerable<SlashCommandRequirementAttribute>>(), It.IsAny<ISlashCommandContext>(), It.IsAny<IServiceProvider>()))
                .ReturnsAsync(Result.FromSuccess);
            var requirementBuilderMock = new Mock<ISlashCommandRequirementBuildService>();
            var guildBuilderMock = new Mock<ISlashCommandGuildBuildService>();
            var optionBuilder = new SlashCommandOptionBuildService(_optionBuilderServiceLoggerMock.Object);
            var buildService = new SlashCommandBuildService(requirementBuilderMock.Object, guildBuilderMock.Object, _buildServiceLoggerMock.Object, optionBuilder);
            var autoSyncMock = new Mock<ISlashCommandAutoSyncService>();
            var commandService = new SlashCommandService(_commandServiceLoggerMock.Object, buildService, requirementServiceMock.Object, autoSyncMock.Object);
            var mockContext = new Mock<ISlashCommandContext>();

            var jsonInput = @"{""stringValue"": ""test json role value"",""intValue"": 12345678}";

            using JsonDocument doc = JsonDocument.Parse(jsonInput);

            var options = new List<IDiscordInteractionOption>
            {
                new DiscordInteractionOption(new DiscordInteractionOptionData
                {
                    Name = "Role name",
                    Type = DiscordApplicationCommandOptionType.String,
                    JsonValue = doc.RootElement.GetProperty("stringValue")
                }),
                new DiscordInteractionOption(new DiscordInteractionOptionData
                {
                    Name = "Number",
                    Type = DiscordApplicationCommandOptionType.Integer,
                    JsonValue = doc.RootElement.GetProperty("intValue")
                })
            };

            // Act
            await commandService.AddInteractionCommandsAsync(ValidAssembly).ConfigureAwait(false);
            var command = commandService.SearchSlashCommand(commandGroupName, subCommandGroupName, commandName);
            var result = await commandService.ExecuteSlashCommandAsync(command!.CommandMethod!, command.CommandOptions, command.Requirements, mockContext.Object, options);

            // Assert
            result.IsSuccessful.Should().BeTrue();
            result.Entity!.Data!.Content.Should().Contain("roleName:test json role value");
            result.Entity!.Data!.Content.Should().Contain("number:12345678");
        }

        [TestCase("role", "Command19")]
        public async Task Should_execute_interaction_command_with_role_option(string commandGroupName, string commandName)
        {
            // Arrange
            var requirementServiceMock = new Mock<ISlashCommandRequirementService>();
            requirementServiceMock
                .Setup(x => x.ExecuteSlashCommandRequirementsAsync(It.IsAny<IEnumerable<SlashCommandRequirementAttribute>>(), It.IsAny<ISlashCommandContext>(), It.IsAny<IServiceProvider>()))
                .ReturnsAsync(Result.FromSuccess);
            var requirementBuilderMock = new Mock<ISlashCommandRequirementBuildService>();
            var guildBuilderMock = new Mock<ISlashCommandGuildBuildService>();
            var optionBuilder = new SlashCommandOptionBuildService(_optionBuilderServiceLoggerMock.Object);
            var buildService = new SlashCommandBuildService(requirementBuilderMock.Object, guildBuilderMock.Object, _buildServiceLoggerMock.Object, optionBuilder);
            var autoSyncMock = new Mock<ISlashCommandAutoSyncService>();
            var commandService = new SlashCommandService(_commandServiceLoggerMock.Object, buildService, requirementServiceMock.Object, autoSyncMock.Object);
            var context = new SlashCommandContext
            {
                Data = new DiscordInteractionRequest(new DiscordInteractionRequestData
                {
                    Resolved = new DiscordInteractionResolvedData
                    {
                        Roles = new Dictionary<ulong, DiscordGuildRoleData>
                        {
                            {
                                865723094761078804,
                                new DiscordGuildRoleData
                                {
                                    Id = 865723094761078804,
                                    Color = Color.Red,
                                    Managed = false,
                                    Mentionable = true,
                                    Name = "red",
                                    Permissions = DiscordPermission.None,
                                    Position = 0,
                                    IsHoisted = false
                                }
                            }
                        }
                    }
                })
            };

            const string jsonInput = @"{""roleId"": ""865723094761078804""}";

            using JsonDocument doc = JsonDocument.Parse(jsonInput);

            var options = new List<IDiscordInteractionOption>
            {
                new DiscordInteractionOption(new DiscordInteractionOptionData
                {
                    Name = "role",
                    Type = DiscordApplicationCommandOptionType.Role,
                    JsonValue = doc.RootElement.GetProperty("roleId")
                })
            };

            // Act
            await commandService.AddInteractionCommandsAsync(ValidAssembly).ConfigureAwait(false);
            var command = commandService.SearchSlashCommand(commandGroupName, commandName);
            var result = await commandService.ExecuteSlashCommandAsync(command!.CommandMethod!, command.CommandOptions, command.Requirements, context, options);

            // Assert
            result.IsSuccessful.Should().BeTrue();
            result.Entity!.Data!.Content.Should().Be("865723094761078804");
        }
    }
}