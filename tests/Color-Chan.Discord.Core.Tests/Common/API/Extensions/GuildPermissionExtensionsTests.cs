using System;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Core.Extensions;
using FluentAssertions;
using NUnit.Framework;

namespace Color_Chan.Discord.Core.Tests.Common.API.Extensions
{
    [TestFixture]
    public class GuildPermissionExtensionsTests
    {
        [TestCase("1409286155", DiscordGuildPermission.Administrator |
                                DiscordGuildPermission.ManageRoles |
                                DiscordGuildPermission.KickMembers |
                                DiscordGuildPermission.CreateInstantInvite |
                                DiscordGuildPermission.ChangeNickname |
                                DiscordGuildPermission.ManageEmojisAndStickers)]
        [TestCase("671088820", DiscordGuildPermission.ManageGuild |
                               DiscordGuildPermission.ManageChannels |
                               DiscordGuildPermission.BanMembers |
                               DiscordGuildPermission.ViewAuditLog |
                               DiscordGuildPermission.ManageNicknames |
                               DiscordGuildPermission.ManageWebhooks)]
        [TestCase("349184", DiscordGuildPermission.ViewChannel |
                            DiscordGuildPermission.SendTtsMessages |
                            DiscordGuildPermission.EmbedLinks |
                            DiscordGuildPermission.ReadMessageHistory |
                            DiscordGuildPermission.UseExternalEmojis)]
        [TestCase("174144", DiscordGuildPermission.SendMessages |
                            DiscordGuildPermission.ManageMessages |
                            DiscordGuildPermission.AttachFiles |
                            DiscordGuildPermission.MentionEveryone |
                            DiscordGuildPermission.AddReactions)]
        [TestCase("6442975488", DiscordGuildPermission.ViewGuildInsights |
                                DiscordGuildPermission.UseSlashCommands |
                                DiscordGuildPermission.PrioritySpeaker |
                                DiscordGuildPermission.RequestToSpeak)]
        [TestCase("120259084800", DiscordGuildPermission.Stream |
                                  DiscordGuildPermission.ManageThreads |
                                  DiscordGuildPermission.UsePrivateThreads |
                                  DiscordGuildPermission.UsePublicThreads)]
        [TestCase("22020096", DiscordGuildPermission.Connect |
                              DiscordGuildPermission.MuteMembers |
                              DiscordGuildPermission.MoveMembers)]
        [TestCase("44040192", DiscordGuildPermission.Speak |
                              DiscordGuildPermission.DeafenMembers |
                              DiscordGuildPermission.UseVoiceActivity)]
        [TestCase("0", (DiscordGuildPermission) 0)]
        public void Should_parse_permissions_string(string permissionString, DiscordGuildPermission expectedPermission)
        {
            // Act
            var permission = permissionString.ConvertToGuildPermission();

            // Assert
            expectedPermission.Should().BeEquivalentTo(permission);
        }

        [TestCase("1409286155", DiscordGuildPermission.Administrator |
                                DiscordGuildPermission.ManageRoles |
                                DiscordGuildPermission.KickMembers |
                                DiscordGuildPermission.CreateInstantInvite |
                                DiscordGuildPermission.ChangeNickname |
                                DiscordGuildPermission.ManageEmojisAndStickers)]
        [TestCase("671088820", DiscordGuildPermission.ManageGuild |
                               DiscordGuildPermission.ManageChannels |
                               DiscordGuildPermission.BanMembers |
                               DiscordGuildPermission.ViewAuditLog |
                               DiscordGuildPermission.ManageNicknames |
                               DiscordGuildPermission.ManageWebhooks)]
        [TestCase("349184", DiscordGuildPermission.ViewChannel |
                            DiscordGuildPermission.SendTtsMessages |
                            DiscordGuildPermission.EmbedLinks |
                            DiscordGuildPermission.ReadMessageHistory |
                            DiscordGuildPermission.UseExternalEmojis)]
        [TestCase("174144", DiscordGuildPermission.SendMessages |
                            DiscordGuildPermission.ManageMessages |
                            DiscordGuildPermission.AttachFiles |
                            DiscordGuildPermission.MentionEveryone |
                            DiscordGuildPermission.AddReactions)]
        [TestCase("6442975488", DiscordGuildPermission.ViewGuildInsights |
                                DiscordGuildPermission.UseSlashCommands |
                                DiscordGuildPermission.PrioritySpeaker |
                                DiscordGuildPermission.RequestToSpeak)]
        [TestCase("120259084800", DiscordGuildPermission.Stream |
                                  DiscordGuildPermission.ManageThreads |
                                  DiscordGuildPermission.UsePrivateThreads |
                                  DiscordGuildPermission.UsePublicThreads)]
        [TestCase("22020096", DiscordGuildPermission.Connect |
                              DiscordGuildPermission.MuteMembers |
                              DiscordGuildPermission.MoveMembers)]
        [TestCase("44040192", DiscordGuildPermission.Speak |
                              DiscordGuildPermission.DeafenMembers |
                              DiscordGuildPermission.UseVoiceActivity)]
        [TestCase("0", (DiscordGuildPermission) 0)]
        public void Should_parse_permissions_span(string permissionString, DiscordGuildPermission expectedPermission)
        {
            // Arrange
            var span = permissionString.AsSpan();

            // Act
            var permission = span.ConvertToGuildPermission();

            // Assert
            expectedPermission.Should().BeEquivalentTo(permission);
        }
    }
}