using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Configurations;
using Color_Chan.Discord.Commands.Extensions;
using Color_Chan.Discord.Commands.Models.Contexts;
using Color_Chan.Discord.Commands.Services;
using Color_Chan.Discord.Commands.Services.Implementations;
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

namespace Color_Chan.Discord.Commands.Tests.Services.Implementations
{
    [TestFixture]
    public class DiscordSlashCommandHandlerTests
    {
        private Mock<ILogger<DiscordSlashCommandHandler>> _handlerLoggerMock = null!;
        private OptionsWrapper<SlashCommandConfiguration> _options = null!;
        private Mock<ISlashCommandService> _commandServiceMock = null!;
        private Mock<IDiscordRestChannel> _restChannelMock = null!;
        private Mock<IDiscordRestGuild> _restGuildMock = null!;
        private static string _orderTestMessage = "";

        [SetUp]
        public void SetUp()
        {
            _restGuildMock = new Mock<IDiscordRestGuild>();
            _restChannelMock = new Mock<IDiscordRestChannel>();
            _handlerLoggerMock = new Mock<ILogger<DiscordSlashCommandHandler>>();
            _options = new OptionsWrapper<SlashCommandConfiguration>(new SlashCommandConfiguration());
            _commandServiceMock = new Mock<ISlashCommandService>();
            _orderTestMessage = string.Empty;
            
            _commandServiceMock.Setup(x => x.ExecuteSlashCommandAsync(It.IsAny<ISlashCommandContext>(),
                                                                      It.IsAny<IEnumerable<IDiscordInteractionCommandOption>>(),
                                                                      It.IsAny<IServiceProvider>()))
                               .ReturnsAsync(Result<IDiscordInteractionResponse>.FromSuccess(new DiscordInteractionResponse()))
                               .Callback(FakeSlashCommandCall);
            
            void FakeSlashCommandCall()
            {
                _orderTestMessage += "command";
            }
        }

        [Test]
        public void Should_throw_when_no_data()
        {
            // Arrange
            var serviceProviderMock = new Mock<IServiceProvider>();
            var handler = new DiscordSlashCommandHandler(_commandServiceMock.Object, serviceProviderMock.Object, _handlerLoggerMock.Object, _restGuildMock.Object, _restChannelMock.Object, _options);
            var interaction = new DiscordInteraction(new DiscordInteractionData());

            // Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => handler.HandleSlashCommandAsync(interaction));
        }
        
        [Test]
        public async Task Should_execute_top_level_command_with_no_pipelines()
        {
            // Arrange
            var serviceProvider = new ServiceCollection()
                                  .BuildServiceProvider();
            var handler = new DiscordSlashCommandHandler(_commandServiceMock.Object, serviceProvider, _handlerLoggerMock.Object, _restGuildMock.Object, _restChannelMock.Object, _options);
            var interaction = new DiscordInteraction(new DiscordInteractionData
            {
                Data = new DiscordInteractionCommandData
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
            });

            // Act
            var result = await handler.HandleSlashCommandAsync(interaction);

            // Assert
            result.Should().NotBeNull();
            _orderTestMessage.Should().Be("command");
        }
        
        [Test]
        public async Task Should_execute_top_level_command_with_resolved_and_pipelines()
        {
            // Arrange
            var serviceProvider = new ServiceCollection()
                                  .AddSlashCommandPipeline<ResolvedPipeline>()
                                  .BuildServiceProvider();
            var handler = new DiscordSlashCommandHandler(_commandServiceMock.Object, serviceProvider, _handlerLoggerMock.Object, _restGuildMock.Object, _restChannelMock.Object, _options);
            var interaction = new DiscordInteraction(new DiscordInteractionData
            {
                Data = new DiscordInteractionCommandData
                {
                    Id = ulong.MaxValue,
                    Name = "command",
                    Resolved = new DiscordInteractionCommandResolvedData
                    {
                        Roles = new Dictionary<ulong, DiscordGuildRoleData>
                        {
                            {865723094761078804, new DiscordGuildRoleData
                            {
                                Id = 865723094761078804,
                                Color = Color.Red,
                                Managed = false,
                                Name = "red",
                                Mentionable = true,
                                Permissions = DiscordPermission.None,
                                Position = 12,
                                IsHoisted = false
                            }}
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
            });

            // Act
            var result = await handler.HandleSlashCommandAsync(interaction);

            // Assert
            result.Should().NotBeNull();
            _orderTestMessage.Should().Be("red_command_red");
        }

        [Test]
        public async Task Should_execute_top_level_command_with_pipelines()
        {
            // Arrange
            var serviceProvider = new ServiceCollection()
                                  .AddSlashCommandPipeline<InnerPipeline>()
                                  .AddSlashCommandPipeline<OuterPipeline>()
                                  .BuildServiceProvider();
            var handler = new DiscordSlashCommandHandler(_commandServiceMock.Object, serviceProvider, _handlerLoggerMock.Object, _restGuildMock.Object, _restChannelMock.Object, _options);
            var interaction = new DiscordInteraction(new DiscordInteractionData
            {
                Data = new DiscordInteractionCommandData
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
            });

            // Act
            var result = await handler.HandleSlashCommandAsync(interaction);

            // Assert
            result.Should().NotBeNull();
            _orderTestMessage.Should().Be("outer_inner_command_inner_outer");
        }

        [Test]
        public async Task Should_execute_sub_command_with_pipelines()
        {
            // Arrange
            var serviceProvider = new ServiceCollection()
                                  .AddSlashCommandPipeline<InnerPipeline>()
                                  .AddSlashCommandPipeline<OuterPipeline>()
                                  .BuildServiceProvider();
            var handler = new DiscordSlashCommandHandler(_commandServiceMock.Object, serviceProvider, _handlerLoggerMock.Object, _restGuildMock.Object, _restChannelMock.Object, _options);
            var interaction = new DiscordInteraction(new DiscordInteractionData
            {
                Data = new DiscordInteractionCommandData
                {
                    Id = ulong.MaxValue,
                    Name = "sub",
                    Options = new List<DiscordInteractionCommandOptionData>
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
            });

            // Act
            var result = await handler.HandleSlashCommandAsync(interaction);

            // Assert
            result.Should().NotBeNull();
            _orderTestMessage.Should().Be("outer_inner_command_inner_outer");
        }
        
        [Test]
        public async Task Should_execute_sub_sub_command_with_pipelines()
        {
            // Arrange
            var serviceProvider = new ServiceCollection()
                                  .AddSlashCommandPipeline<InnerPipeline>()
                                  .AddSlashCommandPipeline<OuterPipeline>()
                                  .BuildServiceProvider();
            var handler = new DiscordSlashCommandHandler(_commandServiceMock.Object, serviceProvider, _handlerLoggerMock.Object, _restGuildMock.Object, _restChannelMock.Object, _options);
            var interaction = new DiscordInteraction(new DiscordInteractionData
            {
                Data = new DiscordInteractionCommandData
                {
                    Id = ulong.MaxValue,
                    Name = "sub",
                    Options = new List<DiscordInteractionCommandOptionData>
                    {
                        new()
                        {
                            Type = DiscordApplicationCommandOptionType.SubCommandGroup,
                            Name = "command",
                            SubOptions = new List<DiscordInteractionCommandOptionData>
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
            });

            // Act
            var result = await handler.HandleSlashCommandAsync(interaction);

            // Assert
            result.Should().NotBeNull();
            _orderTestMessage.Should().Be("outer_inner_command_inner_outer");
        }

        private class OuterPipeline : ISlashCommandPipeline
        {
            public async Task<Result<IDiscordInteractionResponse>> HandleAsync(ISlashCommandContext context, SlashCommandHandlerDelegate next)
            {
                context.SlashCommandName.Should().NotBeNull();
                
                _orderTestMessage += "outer_";
                var result = await next();
                _orderTestMessage += "_outer";
                return result;
            }
        }
        
        private class InnerPipeline : ISlashCommandPipeline
        {
            public async Task<Result<IDiscordInteractionResponse>> HandleAsync(ISlashCommandContext context, SlashCommandHandlerDelegate next)
            {
                context.SlashCommandName.Should().NotBeNull();
                
                _orderTestMessage += "inner_";
                var result = await next();
                _orderTestMessage += "_inner";
                
                return result;
            }
        }
        
        private class ResolvedPipeline : ISlashCommandPipeline
        {
            public async Task<Result<IDiscordInteractionResponse>> HandleAsync(ISlashCommandContext context, SlashCommandHandlerDelegate next)
            {
                var role = context.CommandRequest.Resolved?.Roles?.FirstOrDefault(x => x.Key == 865723094761078804).Value;
                
                _orderTestMessage += $"{role?.Name}_";
                var result = await next();
                _orderTestMessage += $"_{role?.Name}";
                
                return result;
            }
        }
    }
}