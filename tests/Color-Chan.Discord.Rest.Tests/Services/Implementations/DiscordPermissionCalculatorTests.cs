using System.Collections.Generic;
using System.Threading.Tasks;
using Color_Chan.Discord.Core;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Core.Common.API.Rest;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Core.Common.Models.Guild;
using Color_Chan.Discord.Core.Results;
using Color_Chan.Discord.Rest.Models;
using Color_Chan.Discord.Rest.Models.Guild;
using Color_Chan.Discord.Rest.Services.Implementations;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Color_Chan.Discord.Rest.Tests.Services.Implementations;

[TestFixture]
public class DiscordPermissionCalculatorTests
{
    [TestCase(DiscordPermission.Connect, DiscordPermission.Speak, DiscordPermission.Connect | DiscordPermission.Speak)]
    [TestCase(DiscordPermission.Connect, DiscordPermission.None, DiscordPermission.Connect)]
    [TestCase(DiscordPermission.Speak | DiscordPermission.AddReactions, DiscordPermission.Stream, DiscordPermission.Stream | DiscordPermission.Speak | DiscordPermission.AddReactions)]
    [TestCase(DiscordPermission.Connect | DiscordPermission.ChangeNickname, DiscordPermission.Speak, DiscordPermission.Connect | DiscordPermission.Speak | DiscordPermission.ChangeNickname)]
    public async Task Should_calculate_permissions(DiscordPermission expected, DiscordPermission deny, DiscordPermission botPerms)
    {
        // Arrange
        var mockRestChannel = new Mock<IDiscordRestChannel>();
        mockRestChannel.Setup(x=>x.GetChannelAsync(ulong.MaxValue, default))
                       .ReturnsAsync(Result<IDiscordChannel>.FromSuccess(new DiscordChannel(new DiscordChannelData
                       {
                           PermissionOverwrites = new List<DiscordOverwriteData>
                           {
                               new ()
                               {
                                   TargetId = 1,
                                   TargetType = DiscordPermissionTargetType.Role,
                                   Deny = deny
                               }
                           }
                       })));
        var mockRestGuild = new Mock<IDiscordRestGuild>();
        mockRestGuild.Setup(x=>x.GetGuildAsync(ulong.MaxValue, false, default))
                     .ReturnsAsync(Result<IDiscordGuild>.FromSuccess(new DiscordGuild(new DiscordGuildData
                     {
                         Roles = new List<DiscordGuildRoleData>
                         {
                             new ()
                             {
                                 Id = 1,
                                 Permissions = botPerms
                             }
                         }
                     })));
        mockRestGuild.Setup(x => x.GetGuildMemberAsync(ulong.MaxValue, ulong.MaxValue, default))
                     .ReturnsAsync(Result<IDiscordGuildMember>.FromSuccess(new DiscordGuildMember(new DiscordGuildMemberData
                     {
                         Roles = new ulong[] { 1 }
                     })));

        var discordTokens = new DiscordTokens("", "", ulong.MaxValue);
        var calculator = new DiscordPermissionCalculator(mockRestGuild.Object, mockRestChannel.Object, discordTokens);

        // Act
        var result = await calculator.CalculatePermissionAsync(ulong.MaxValue, ulong.MaxValue).ConfigureAwait(false);

        // Assert
        result.IsSuccessful.Should().BeTrue();
        (result.Entity == expected).Should().BeTrue();
    }
}