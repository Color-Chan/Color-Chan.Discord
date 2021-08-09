using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Attributes.ProvidedRequirements;
using Color_Chan.Discord.Commands.Models.Contexts;
using Color_Chan.Discord.Commands.Models.Results;
using Color_Chan.Discord.Core;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Core.Common.API.Rest;
using Color_Chan.Discord.Core.Common.Models.Guild;
using Color_Chan.Discord.Core.Results;
using Color_Chan.Discord.Rest.Models;
using Color_Chan.Discord.Rest.Models.Guild;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace Color_Chan.Discord.Commands.Tests.Attributes
{
    [TestFixture]
    public class SlashCommandRequireBotPermissionAttributeTests
    {
        [Test]
        public async Task Should_pass_user_permission_requirement()
        {
            // Arrange
            var attribute = new SlashCommandRequireBotPermissionAttribute(DiscordPermission.AddReactions);
            var discordToken = new DiscordTokens("TOKEN", "PUBLIC_KEY", 123);
            var restGuildMock = new Mock<IDiscordRestGuild>();
            var context = GetTestContext(DiscordPermission.Speak | DiscordPermission.AddReactions, DiscordPermission.Speak);
            var botMember = new DiscordGuildMember(new DiscordGuildMemberData
            {
                Roles = new ulong[]{1}
            });
            restGuildMock.Setup(x => x.GetGuildMemberAsync(It.IsAny<ulong>(), It.IsAny<ulong>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(Result<IDiscordGuildMember>.FromSuccess(botMember));

            var collectionProvider = new ServiceCollection()
                                     .AddSingleton(restGuildMock.Object)
                                     .AddSingleton(discordToken)
                                     .BuildServiceProvider();

            // Act
            var result = await attribute.CheckRequirementAsync(context, collectionProvider);

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Test]
        public async Task Should_not_pass_user_permission_requirement_dm()
        {
            // Arrange
            var attribute = new SlashCommandRequireBotPermissionAttribute(DiscordPermission.Administrator);
            var discordToken = new DiscordTokens("TOKEN", "PUBLIC_KEY", 123);
            var restGuildMock = new Mock<IDiscordRestGuild>();
            var context = new SlashCommandContext();
            var botMember = new DiscordGuildMember(new DiscordGuildMemberData
            {
                Roles = new ulong[]{1}
            });
            restGuildMock.Setup(x => x.GetGuildMemberAsync(It.IsAny<ulong>(), It.IsAny<ulong>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(Result<IDiscordGuildMember>.FromSuccess(botMember));

            var collectionProvider = new ServiceCollection()
                                     .AddSingleton(restGuildMock.Object)
                                     .AddSingleton(discordToken)
                                     .BuildServiceProvider();

            // Act
            var result = await attribute.CheckRequirementAsync(context, collectionProvider);

            // Assert
            result.IsSuccessful.Should().BeFalse();
        }

        [Test]
        public async Task Should_not_pass_user_permission_requirement()
        {
            // Arrange
            var discordToken = new DiscordTokens("TOKEN", "PUBLIC_KEY", 123);
            var restGuildMock = new Mock<IDiscordRestGuild>();
            var context = GetTestContext(DiscordPermission.Speak | DiscordPermission.AddReactions, DiscordPermission.Speak);
            var attribute = new SlashCommandRequireBotPermissionAttribute(DiscordPermission.Speak
                                                                          | DiscordPermission.AddReactions);
            var botMember = new DiscordGuildMember(new DiscordGuildMemberData
            {
                Roles = new ulong[]{1}
            });
            restGuildMock.Setup(x => x.GetGuildMemberAsync(It.IsAny<ulong>(), It.IsAny<ulong>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(Result<IDiscordGuildMember>.FromSuccess(botMember));
            
            var collectionProvider = new ServiceCollection()
                                     .AddSingleton(restGuildMock.Object)
                                     .AddSingleton(discordToken)
                                     .BuildServiceProvider();

            // Act
            var result = await attribute.CheckRequirementAsync(context, collectionProvider);

            // Assert
            result.IsSuccessful.Should().BeFalse();
            result.ErrorResult.Should().NotBeNull();
            result.ErrorResult.Should().BeOfType<SlashCommandRequireBotPermissionErrorResult>();
            var reqError = result.ErrorResult as SlashCommandRequireBotPermissionErrorResult;
            reqError!.MissingPermissions.Should().NotBeNull();
            reqError.MissingPermissions!.Count.Should().Be(1);
            foreach (var missingPermission in reqError.MissingPermissions)
            {
                missingPermission.Should().Be(DiscordPermission.Speak);
            }
        }

        private ISlashCommandContext GetTestContext(DiscordPermission permission, DiscordPermission denyChannelPerms)
        {
            var context = new SlashCommandContext
            {
                GuildId = 2,
                Guild = new DiscordGuild(new DiscordGuildData
                {
                    Roles = new List<DiscordGuildRoleData>
                    {
                        new ()
                        {
                            Id = 1,
                            Permissions = permission
                        }
                    }
                }),
                Channel = new DiscordChannel(new DiscordChannelData
                {
                    PermissionOverwrites = new List<DiscordOverwriteData>
                    {
                        new ()
                        {
                            TargetId = 1,
                            TargetType = DiscordPermissionTargetType.Role,
                            Deny = denyChannelPerms
                        }
                    }
                })
            };

            return context;
        }
    }
}