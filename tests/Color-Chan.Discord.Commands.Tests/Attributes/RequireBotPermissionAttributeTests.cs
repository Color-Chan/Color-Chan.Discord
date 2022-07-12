using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Attributes.ProvidedRequirements;
using Color_Chan.Discord.Commands.Models.Contexts;
using Color_Chan.Discord.Core.Common.API.DataModels;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Color_Chan.Discord.Commands.Tests.Attributes;

[TestFixture]
public class RequireBotPermissionAttributeTests
{
    [Test]
    public async Task Should_not_pass_user_permission_requirement_dm()
    {
        // Arrange
        var attribute = new RequireBotPermissionAttribute(DiscordPermission.None);
        var collectionProvider = new ServiceCollection().BuildServiceProvider();
        var context = new InteractionContext();

        // Act
        var result = await attribute.CheckRequirementAsync(context, collectionProvider);

        // Assert
        result.IsSuccessful.Should().BeFalse();
    }
    
    [TestCase(DiscordPermission.Speak, DiscordPermission.Speak)]
    [TestCase(DiscordPermission.AddReactions, DiscordPermission.Administrator)]
    [TestCase(DiscordPermission.DeafenMembers | DiscordPermission.Stream | DiscordPermission.EmbedLinks, DiscordPermission.DeafenMembers | DiscordPermission.Stream | DiscordPermission.EmbedLinks)]
    public async Task Should_pass_user_permission_requirement(DiscordPermission required, DiscordPermission actual)
    {
        // Arrange
        var attribute = new RequireBotPermissionAttribute(required);
        var collectionProvider = new ServiceCollection().BuildServiceProvider();
        var context = new InteractionContext
        {
            Permissions = actual
        };

        // Act
        var result = await attribute.CheckRequirementAsync(context, collectionProvider);

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }
    
    [TestCase(DiscordPermission.Speak, DiscordPermission.None)]
    [TestCase(DiscordPermission.Administrator, DiscordPermission.Connect)]
    [TestCase(DiscordPermission.MentionEveryone, DiscordPermission.ManageEmojisAndStickers)]
    [TestCase(DiscordPermission.DeafenMembers | DiscordPermission.Stream | DiscordPermission.EmbedLinks, DiscordPermission.DeafenMembers | DiscordPermission.Stream)]
    public async Task Should_not_pass_user_permission_requirement(DiscordPermission required, DiscordPermission actual)
    {
        // Arrange
        var attribute = new RequireBotPermissionAttribute(required);
        var collectionProvider = new ServiceCollection().BuildServiceProvider();
        var context = new InteractionContext
        {
            Permissions = actual
        };

        // Act
        var result = await attribute.CheckRequirementAsync(context, collectionProvider);

        // Assert
        result.IsSuccessful.Should().BeFalse();
    }
}