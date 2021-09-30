using System;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Extensions;
using FluentAssertions;
using NUnit.Framework;

namespace Color_Chan.Discord.Core.Tests.Common.API.Extensions
{
    [TestFixture]
    public class GuildPermissionExtensionsTests
    {
        [TestCase("1409286155", DiscordPermission.Administrator |
                                DiscordPermission.ManageRoles |
                                DiscordPermission.KickMembers |
                                DiscordPermission.CreateInstantInvite |
                                DiscordPermission.ChangeNickname |
                                DiscordPermission.ManageEmojisAndStickers)]
        [TestCase("671088820", DiscordPermission.ManageGuild |
                               DiscordPermission.ManageChannels |
                               DiscordPermission.BanMembers |
                               DiscordPermission.ViewAuditLog |
                               DiscordPermission.ManageNicknames |
                               DiscordPermission.ManageWebhooks)]
        [TestCase("349184", DiscordPermission.ViewChannel |
                            DiscordPermission.SendTtsMessages |
                            DiscordPermission.EmbedLinks |
                            DiscordPermission.ReadMessageHistory |
                            DiscordPermission.UseExternalEmojis)]
        [TestCase("174144", DiscordPermission.SendMessages |
                            DiscordPermission.ManageMessages |
                            DiscordPermission.AttachFiles |
                            DiscordPermission.MentionEveryone |
                            DiscordPermission.AddReactions)]
        [TestCase("6442975488", DiscordPermission.ViewGuildInsights |
                                DiscordPermission.UseApplicationCommands |
                                DiscordPermission.PrioritySpeaker |
                                DiscordPermission.RequestToSpeak)]
        [TestCase("120259084800", DiscordPermission.Stream |
                                  DiscordPermission.ManageThreads |
                                  DiscordPermission.UsePrivateThreads |
                                  DiscordPermission.UsePublicThreads)]
        [TestCase("22020096", DiscordPermission.Connect |
                              DiscordPermission.MuteMembers |
                              DiscordPermission.MoveMembers)]
        [TestCase("44040192", DiscordPermission.Speak |
                              DiscordPermission.DeafenMembers |
                              DiscordPermission.UseVoiceActivity)]
        [TestCase("0", (DiscordPermission)0)]
        public void Should_parse_permissions_string(string permissionString, DiscordPermission expectedPermission)
        {
            // Act
            var permission = permissionString.ConvertToGuildPermission();

            // Assert
            expectedPermission.Should().Be(permission);
        }

        [TestCase("1409286155", DiscordPermission.Administrator |
                                DiscordPermission.ManageRoles |
                                DiscordPermission.KickMembers |
                                DiscordPermission.CreateInstantInvite |
                                DiscordPermission.ChangeNickname |
                                DiscordPermission.ManageEmojisAndStickers)]
        [TestCase("671088820", DiscordPermission.ManageGuild |
                               DiscordPermission.ManageChannels |
                               DiscordPermission.BanMembers |
                               DiscordPermission.ViewAuditLog |
                               DiscordPermission.ManageNicknames |
                               DiscordPermission.ManageWebhooks)]
        [TestCase("349184", DiscordPermission.ViewChannel |
                            DiscordPermission.SendTtsMessages |
                            DiscordPermission.EmbedLinks |
                            DiscordPermission.ReadMessageHistory |
                            DiscordPermission.UseExternalEmojis)]
        [TestCase("174144", DiscordPermission.SendMessages |
                            DiscordPermission.ManageMessages |
                            DiscordPermission.AttachFiles |
                            DiscordPermission.MentionEveryone |
                            DiscordPermission.AddReactions)]
        [TestCase("6442975488", DiscordPermission.ViewGuildInsights |
                                DiscordPermission.UseApplicationCommands |
                                DiscordPermission.PrioritySpeaker |
                                DiscordPermission.RequestToSpeak)]
        [TestCase("120259084800", DiscordPermission.Stream |
                                  DiscordPermission.ManageThreads |
                                  DiscordPermission.UsePrivateThreads |
                                  DiscordPermission.UsePublicThreads)]
        [TestCase("22020096", DiscordPermission.Connect |
                              DiscordPermission.MuteMembers |
                              DiscordPermission.MoveMembers)]
        [TestCase("44040192", DiscordPermission.Speak |
                              DiscordPermission.DeafenMembers |
                              DiscordPermission.UseVoiceActivity)]
        [TestCase("0", (DiscordPermission)0)]
        public void Should_parse_permissions_span(string permissionString, DiscordPermission expectedPermission)
        {
            // Arrange
            var span = permissionString.AsSpan();

            // Act
            var permission = span.ConvertToGuildPermission();

            // Assert
            expectedPermission.Should().Be(permission);
        }
    }
}