using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Configurations;
using Color_Chan.Discord.Commands.Extensions;
using Color_Chan.Discord.Commands.InteractionHandlers;
using Color_Chan.Discord.Commands.Models.Contexts;
using Color_Chan.Discord.Commands.Models.Info;
using Color_Chan.Discord.Commands.Services;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Common.API.Rest;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;
using Color_Chan.Discord.Rest.Models.Interaction;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace Color_Chan.Discord.Commands.Tests.InteractionHandlers;

[TestFixture]
public class DiscordSlashCommandHandlerTests
{
    [SetUp]
    public void SetUp()
    {
        _restGuildMock = new Mock<IDiscordRestGuild>();
        _restChannelMock = new Mock<IDiscordRestChannel>();
        _restApplicationMock = new Mock<IDiscordRestApplication>();
        _handlerLoggerMock = new Mock<ILogger<ApplicationCommandRequestHandler>>();
        _options = new OptionsWrapper<SlashCommandConfiguration>(new SlashCommandConfiguration());
        _commandServiceMock = new Mock<ISlashCommandService>();
        _orderTestMessage = string.Empty;

        _commandServiceMock.Setup(x => x.SearchSlashCommand(It.IsAny<string>())).Returns(new SlashCommandInfo("", "", true, null!, null!));
        _commandServiceMock.Setup(x => x.SearchSlashCommand(It.IsAny<string>(), It.IsAny<string>())).Returns(new SlashCommandOptionInfo("", "", true, null!, null!));
        _commandServiceMock.Setup(x => x.SearchSlashCommand(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new SlashCommandOptionInfo("", "", true, null!, null!));

        _commandServiceMock.Setup(x => x.ExecuteSlashCommandAsync(
                    It.IsAny<ISlashCommandInfo>(),
                    It.IsAny<ISlashCommandContext>(),
                    It.IsAny<List<IDiscordInteractionOption>>(),
                    It.IsAny<IServiceProvider>()
                )
            )
            .ReturnsAsync(Result<IDiscordInteractionResponse>.FromSuccess(new DiscordInteractionResponse()))
            .Callback(FakeSlashCommandCall);

        _commandServiceMock.Setup(x => x.ExecuteSlashCommandAsync(
                    It.IsAny<SlashCommandOptionInfo>(),
                    It.IsAny<ISlashCommandContext>(),
                    It.IsAny<List<IDiscordInteractionOption>>(),
                    It.IsAny<IServiceProvider>()
                )
            )
            .ReturnsAsync(Result<IDiscordInteractionResponse>.FromSuccess(new DiscordInteractionResponse()))
            .Callback(FakeSlashCommandCall);

        void FakeSlashCommandCall()
        {
            _orderTestMessage += "command";
        }
    }

    private Mock<ILogger<ApplicationCommandRequestHandler>> _handlerLoggerMock = null!;
    private OptionsWrapper<SlashCommandConfiguration> _options = null!;
    private Mock<ISlashCommandService> _commandServiceMock = null!;
    private Mock<IDiscordRestChannel> _restChannelMock = null!;
    private Mock<IDiscordRestApplication> _restApplicationMock = null!;
    private Mock<IDiscordRestGuild> _restGuildMock = null!;
    private static string _orderTestMessage = "";

    [Test]
    public void Should_throw_when_no_data()
    {
        // Arrange
        var serviceProviderMock = new Mock<IServiceProvider>();
        var handler = new ApplicationCommandRequestHandler(
            _handlerLoggerMock.Object,
            _options,
            _restGuildMock.Object,
            _restChannelMock.Object,
            _restApplicationMock.Object,
            _commandServiceMock.Object,
            serviceProviderMock.Object
        );
        var interaction = new DiscordInteraction(new DiscordInteractionData());

        // Act & Assert
        Assert.ThrowsAsync<ArgumentNullException>(() => handler.HandleInteractionAsync(interaction));
    }

    [Test]
    public async Task Should_execute_top_level_command_with_no_pipelines()
    {
        // Arrange
        var serviceProvider = new ServiceCollection().BuildServiceProvider();
        var handler = new ApplicationCommandRequestHandler(
            _handlerLoggerMock.Object,
            _options,
            _restGuildMock.Object,
            _restChannelMock.Object,
            _restApplicationMock.Object,
            _commandServiceMock.Object,
            serviceProvider
        );
        var interaction = new DiscordInteraction(
            new DiscordInteractionData
            {
                Data = new DiscordInteractionRequestData
                {
                    Id = ulong.MaxValue,
                    Name = "command"
                },
                User = new DiscordUserData
                {
                    Id = 1
                },
                ApplicationId = 2,
                ChannelId = 3,
                Token = "token"
            }
        );

        // Act
        var result = await handler.HandleInteractionAsync(interaction);

        // Assert
        result.Should().NotBeNull();
        _orderTestMessage.Should().Be("command");
    }

    [Test]
    public async Task Should_execute_top_level_command_with_resolved_and_pipelines()
    {
        // Arrange
        var serviceProvider = new ServiceCollection()
            .AddInteractionPipeline<ResolvedPipeline>()
            .BuildServiceProvider();
        var handler = new ApplicationCommandRequestHandler(
            _handlerLoggerMock.Object,
            _options,
            _restGuildMock.Object,
            _restChannelMock.Object,
            _restApplicationMock.Object,
            _commandServiceMock.Object,
            serviceProvider
        );
        var interaction = new DiscordInteraction(
            new DiscordInteractionData
            {
                Data = new DiscordInteractionRequestData
                {
                    Id = ulong.MaxValue,
                    Name = "command",
                    Resolved = new DiscordInteractionResolvedData
                    {
                        Roles = new Dictionary<ulong, DiscordGuildRoleData>
                        {
                            {
                                865723094761078804, new DiscordGuildRoleData
                                {
                                    Id = 865723094761078804,
                                    Color = Color.Red,
                                    Managed = false,
                                    Name = "red",
                                    Mentionable = true,
                                    Permissions = DiscordPermission.None,
                                    Position = 12,
                                    IsHoisted = false
                                }
                            }
                        }
                    }
                },
                User = new DiscordUserData
                {
                    Id = 1
                },
                ApplicationId = 2,
                ChannelId = 3,
                Token = "token"
            }
        );

        // Act
        var result = await handler.HandleInteractionAsync(interaction);

        // Assert
        result.Should().NotBeNull();
        _orderTestMessage.Should().Be("red_command_red");
    }

    [Test]
    public async Task Should_execute_top_level_command_with_pipelines()
    {
        // Arrange
        var serviceProvider = new ServiceCollection()
            .AddInteractionPipeline<InnerPipeline>()
            .AddInteractionPipeline<OuterPipeline>()
            .BuildServiceProvider();
        var handler = new ApplicationCommandRequestHandler(
            _handlerLoggerMock.Object,
            _options,
            _restGuildMock.Object,
            _restChannelMock.Object,
            _restApplicationMock.Object,
            _commandServiceMock.Object,
            serviceProvider
        );
        var interaction = new DiscordInteraction(
            new DiscordInteractionData
            {
                Data = new DiscordInteractionRequestData
                {
                    Id = ulong.MaxValue,
                    Name = "command"
                },
                User = new DiscordUserData
                {
                    Id = 1
                },
                ApplicationId = 2,
                ChannelId = 3,
                Token = "token"
            }
        );

        // Act
        var result = await handler.HandleInteractionAsync(interaction);

        // Assert
        result.Should().NotBeNull();
        _orderTestMessage.Should().Be("outer_inner_command_inner_outer");
    }

    [Test]
    public async Task Should_execute_sub_command_with_pipelines()
    {
        // Arrange
        var serviceProvider = new ServiceCollection()
            .AddInteractionPipeline<InnerPipeline>()
            .AddInteractionPipeline<OuterPipeline>()
            .BuildServiceProvider();
        var handler = new ApplicationCommandRequestHandler(
            _handlerLoggerMock.Object,
            _options,
            _restGuildMock.Object,
            _restChannelMock.Object,
            _restApplicationMock.Object,
            _commandServiceMock.Object,
            serviceProvider
        );
        var interaction = new DiscordInteraction(
            new DiscordInteractionData
            {
                Data = new DiscordInteractionRequestData
                {
                    Id = ulong.MaxValue,
                    Name = "sub",
                    Options = new List<DiscordInteractionOptionData>
                    {
                        new()
                        {
                            Type = DiscordApplicationCommandOptionType.SubCommand,
                            Name = "command"
                        }
                    }
                },
                User = new DiscordUserData
                {
                    Id = 1
                },
                ApplicationId = 2,
                ChannelId = 3,
                Token = "token"
            }
        );

        // Act
        var result = await handler.HandleInteractionAsync(interaction);

        // Assert
        result.Should().NotBeNull();
        _orderTestMessage.Should().Be("outer_inner_command_inner_outer");
    }

    [Test]
    public async Task Should_execute_sub_sub_command_with_pipelines()
    {
        // Arrange
        var serviceProvider = new ServiceCollection()
            .AddInteractionPipeline<InnerPipeline>()
            .AddInteractionPipeline<OuterPipeline>()
            .BuildServiceProvider();
        var handler = new ApplicationCommandRequestHandler(
            _handlerLoggerMock.Object,
            _options,
            _restGuildMock.Object,
            _restChannelMock.Object,
            _restApplicationMock.Object,
            _commandServiceMock.Object,
            serviceProvider
        );
        var interaction = new DiscordInteraction(
            new DiscordInteractionData
            {
                Data = new DiscordInteractionRequestData
                {
                    Id = ulong.MaxValue,
                    Name = "sub",
                    Options = new List<DiscordInteractionOptionData>
                    {
                        new()
                        {
                            Type = DiscordApplicationCommandOptionType.SubCommandGroup,
                            Name = "command",
                            SubOptions = new List<DiscordInteractionOptionData>
                            {
                                new()
                                {
                                    Type = DiscordApplicationCommandOptionType.SubCommand,
                                    Name = "group"
                                }
                            }
                        }
                    }
                },
                User = new DiscordUserData
                {
                    Id = 1
                },
                ApplicationId = 2,
                ChannelId = 3,
                Token = "token"
            }
        );

        // Act
        var result = await handler.HandleInteractionAsync(interaction);

        // Assert
        result.Should().NotBeNull();
        _orderTestMessage.Should().Be("outer_inner_command_inner_outer");
    }

    private class OuterPipeline : IInteractionPipeline
    {
        public async Task<Result<IDiscordInteractionResponse>> HandleAsync(IInteractionContext context, InteractionHandlerDelegate next)
        {
            var slashContext = context as ISlashCommandContext;
            slashContext!.SlashCommandName.Should().NotBeNull();

            _orderTestMessage += "outer_";
            var result = await next();
            _orderTestMessage += "_outer";
            return result;
        }
    }

    private class InnerPipeline : IInteractionPipeline
    {
        public async Task<Result<IDiscordInteractionResponse>> HandleAsync(IInteractionContext context, InteractionHandlerDelegate next)
        {
            var slashContext = context as ISlashCommandContext;
            slashContext!.SlashCommandName.Should().NotBeNull();

            _orderTestMessage += "inner_";
            var result = await next();
            _orderTestMessage += "_inner";

            return result;
        }
    }

    private class ResolvedPipeline : IInteractionPipeline
    {
        public async Task<Result<IDiscordInteractionResponse>> HandleAsync(IInteractionContext context, InteractionHandlerDelegate next)
        {
            var role = context.Data.Resolved?.Roles?.FirstOrDefault(x => x.Key == 865723094761078804).Value;

            _orderTestMessage += $"{role?.Name}_";
            var result = await next();
            _orderTestMessage += $"_{role?.Name}";

            return result;
        }
    }
}