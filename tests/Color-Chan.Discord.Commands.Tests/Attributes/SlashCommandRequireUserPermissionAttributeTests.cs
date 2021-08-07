using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Attributes.ProvidedRequirements;
using Color_Chan.Discord.Commands.Commands;
using Color_Chan.Discord.Commands.Results;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Rest.Models.Guild;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Color_Chan.Discord.Commands.Tests.Attributes
{
    [TestFixture]
    public class SlashCommandRequireUserPermissionAttributeTests
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
                    Permissions = DiscordGuildPermission.Administrator
                })
            };
            var attribute = new SlashCommandRequireUserPermissionAttribute(DiscordGuildPermission.Administrator);

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
            var attribute = new SlashCommandRequireUserPermissionAttribute(DiscordGuildPermission.Administrator);

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
                    Permissions = DiscordGuildPermission.AddReactions | DiscordGuildPermission.Speak
                })
            };
            var attribute = new SlashCommandRequireUserPermissionAttribute(DiscordGuildPermission.Administrator 
                                                                           | DiscordGuildPermission.Speak
                                                                           | DiscordGuildPermission.AddReactions);

            // Act
            var result = await attribute.CheckRequirementAsync(context, collectionProvider);
            
            // Assert
            result.IsSuccessful.Should().BeFalse();
            result.ErrorResult.Should().NotBeNull();
            result.ErrorResult.Should().BeOfType<SlashCommandRequireUserPermissionErrorResult>();
            var reqError = result.ErrorResult as SlashCommandRequireUserPermissionErrorResult;
            reqError!.MissingPermissions.Should().NotBeNull();
            reqError.MissingPermissions!.Count.Should().Be(1);
            foreach (var missingPermission in reqError.MissingPermissions)
            {
                missingPermission.Should().Be(DiscordGuildPermission.Administrator);
            }
        }
    }
}