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
using Color_Chan.Discord.Core.Extensions;
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
        [TestCase(DiscordPermission.Speak, DiscordPermission.Speak, DiscordPermission.Administrator)]
        [TestCase(DiscordPermission.Speak | DiscordPermission.DeafenMembers | DiscordPermission.BanMembers,
                  DiscordPermission.None,
                  DiscordPermission.Speak | DiscordPermission.Stream | DiscordPermission.BanMembers | DiscordPermission.DeafenMembers | DiscordPermission.KickMembers)]
        public async Task Should_pass_user_permission_requirement(DiscordPermission required, DiscordPermission deny, DiscordPermission botPerms)
        {
            // Arrange
            var attribute = new SlashCommandRequireBotPermissionAttribute(required);
            var discordToken = new DiscordTokens("TOKEN", "PUBLIC_KEY", 123);
            var restGuildMock = new Mock<IDiscordRestGuild>();
            var context = GetTestContext(botPerms, deny);
            var botMember = new DiscordGuildMember(new DiscordGuildMemberData
            {
                Roles = new ulong[] { 1 }
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
                Roles = new ulong[] { 1 }
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

        [TestCase(DiscordPermission.AddReactions | DiscordPermission.AttachFiles | DiscordPermission.Speak | DiscordPermission.Stream,
                  DiscordPermission.AddReactions,
                  DiscordPermission.Speak | DiscordPermission.AddReactions | DiscordPermission.Speak | DiscordPermission.Stream,
                  DiscordPermission.AddReactions | DiscordPermission.AttachFiles)]
        [TestCase(DiscordPermission.AddReactions | DiscordPermission.Speak | DiscordPermission.Stream,
                  DiscordPermission.AddReactions,
                  DiscordPermission.Speak | DiscordPermission.AddReactions | DiscordPermission.Speak | DiscordPermission.Stream | DiscordPermission.AttachFiles | DiscordPermission.BanMembers,
                  DiscordPermission.AddReactions)]
        public async Task Should_not_pass_user_permission_requirement(DiscordPermission required, DiscordPermission deny, DiscordPermission bot, DiscordPermission missing)
        {
            // Arrange
            var discordToken = new DiscordTokens("TOKEN", "PUBLIC_KEY", 123);
            var restGuildMock = new Mock<IDiscordRestGuild>();
            var context = GetTestContext(bot, deny);
            var attribute = new SlashCommandRequireBotPermissionAttribute(required);
            var botMember = new DiscordGuildMember(new DiscordGuildMemberData
            {
                Roles = new ulong[] { 1 }
            });
            restGuildMock.Setup(x => x.GetGuildMemberAsync(It.IsAny<ulong>(), It.IsAny<ulong>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(Result<IDiscordGuildMember>.FromSuccess(botMember));
            var missingList = missing.ToList();
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
            reqError.MissingPermissions!.Count.Should().Be(missingList.Count);
            foreach (var missingPermission in reqError.MissingPermissions)
            {
                missingList.Should().Contain(missingPermission);
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
                        new()
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
                        new()
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