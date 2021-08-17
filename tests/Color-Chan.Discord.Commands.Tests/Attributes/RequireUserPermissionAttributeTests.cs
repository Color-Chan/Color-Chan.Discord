using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Attributes.ProvidedRequirements;
using Color_Chan.Discord.Commands.Models.Contexts;
using Color_Chan.Discord.Commands.Models.Results;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Rest.Models.Guild;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Color_Chan.Discord.Commands.Tests.Attributes
{
    [TestFixture]
    public class RequireUserPermissionAttributeTests
    {
        [Test]
        public async Task Should_pass_user_permission_requirement()
        {
            // Arrange
            var collectionProvider = new ServiceCollection().BuildServiceProvider();
            var context = new SlashCommandContext
            {
                Member = new DiscordGuildMember(new DiscordGuildMemberData
                {
                    Permissions = DiscordPermission.Administrator
                })
            };
            var attribute = new RequireUserPermissionAttribute(DiscordPermission.Administrator);

            // Act
            var result = await attribute.CheckRequirementAsync(context, collectionProvider);

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Test]
        public async Task Should_not_pass_user_permission_requirement_dm()
        {
            // Arrange
            var collectionProvider = new ServiceCollection().BuildServiceProvider();
            var context = new SlashCommandContext();
            var attribute = new RequireUserPermissionAttribute(DiscordPermission.Administrator);

            // Act
            var result = await attribute.CheckRequirementAsync(context, collectionProvider);

            // Assert
            result.IsSuccessful.Should().BeFalse();
        }

        [Test]
        public async Task Should_not_pass_user_permission_requirement()
        {
            // Arrange
            var collectionProvider = new ServiceCollection().BuildServiceProvider();
            var context = new SlashCommandContext
            {
                Member = new DiscordGuildMember(new DiscordGuildMemberData
                {
                    Permissions = DiscordPermission.AddReactions | DiscordPermission.Speak
                })
            };
            var attribute = new RequireUserPermissionAttribute(DiscordPermission.Administrator
                                                               | DiscordPermission.Speak
                                                               | DiscordPermission.AddReactions);

            // Act
            var result = await attribute.CheckRequirementAsync(context, collectionProvider);

            // Assert
            result.IsSuccessful.Should().BeFalse();
            result.ErrorResult.Should().NotBeNull();
            result.ErrorResult.Should().BeOfType<RequireUserPermissionErrorResult>();
            var reqError = result.ErrorResult as RequireUserPermissionErrorResult;
            reqError!.MissingPermissions.Should().NotBeNull();
            reqError.MissingPermissions!.Count.Should().Be(1);
            foreach (var missingPermission in reqError.MissingPermissions)
            {
                missingPermission.Should().Be(DiscordPermission.Administrator);
            }
        }
    }
}