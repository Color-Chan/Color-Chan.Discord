using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Extensions;
using FluentAssertions;
using NUnit.Framework;

namespace Color_Chan.Discord.Core.Tests.Extensions;

[TestFixture]
public class DiscordPermissionExtensionsTests
{
    [TestCase(DiscordPermission.Administrator | DiscordPermission.BanMembers)]
    [TestCase(DiscordPermission.ManageRoles)]
    [TestCase(DiscordPermission.ManageNicknames)]
    [TestCase(DiscordPermission.ViewAuditLog)]
    [TestCase(DiscordPermission.KickMembers | DiscordPermission.ManageGuild | DiscordPermission.ViewGuildInsights)]
    public void HasChannelPermissions_should_not_detect_channel_perm(DiscordPermission permission)
    {
        // Act
        var result = permission.HasChannelPermissions();

        // Assert
        result.Should().BeFalse();
    }

    [TestCase(DiscordPermission.ManageChannels)]
    [TestCase(DiscordPermission.AddReactions)]
    [TestCase(DiscordPermission.ManageMessages)]
    [TestCase(DiscordPermission.MuteMembers)]
    [TestCase(DiscordPermission.UsePublicThreads)]
    [TestCase(DiscordPermission.UsePrivateThreads)]
    [TestCase(DiscordPermission.MoveMembers)]
    [TestCase(DiscordPermission.UseVoiceActivity)]
    [TestCase(DiscordPermission.PrioritySpeaker)]
    [TestCase(DiscordPermission.ManageThreads | DiscordPermission.KickMembers)]
    [TestCase(DiscordPermission.UseExternalStickers | DiscordPermission.ManageWebhooks)]
    [TestCase(DiscordPermission.SendTtsMessages | DiscordPermission.ManageGuild)]
    [TestCase(DiscordPermission.Connect | DiscordPermission.BanMembers | DiscordPermission.ManageThreads)]
    [TestCase(DiscordPermission.Administrator | DiscordPermission.BanMembers | DiscordPermission.Stream)]
    [TestCase(DiscordPermission.ChangeNickname | DiscordPermission.PrioritySpeaker)]
    [TestCase(DiscordPermission.KickMembers | DiscordPermission.ManageGuild | DiscordPermission.ViewGuildInsights | DiscordPermission.AttachFiles)]
    [TestCase(DiscordPermission.KickMembers | DiscordPermission.ManageGuild | DiscordPermission.ViewGuildInsights |
              DiscordPermission.ChangeNickname | DiscordPermission.Administrator | DiscordPermission.BanMembers |
              DiscordPermission.ManageGuild | DiscordPermission.ViewAuditLog | DiscordPermission.ManageEmojisAndStickers | DiscordPermission.SendMessages)]
    public void HasChannelPermissions_should_detect_channel_perm(DiscordPermission permission)
    {
        // Act
        var result = permission.HasChannelPermissions();

        // Assert
        result.Should().BeTrue();
    }
}