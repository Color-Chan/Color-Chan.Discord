using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Attributes.ProvidedRequirements;
using Color_Chan.Discord.Commands.Models.Contexts;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Rest.Models.Guild;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Color_Chan.Discord.Commands.Tests.Attributes
{
    [TestFixture]
    public class RequireGuildAttributeTests
    {
        [Test]
        public async Task Should_not_pass_guild_requirement()
        {
            // Arrange
            var attribute = new RequireGuildAttribute();
            var collectionProvider = new ServiceCollection().BuildServiceProvider();
            var context = new SlashCommandContext();

            // Act
            var result = await attribute.CheckRequirementAsync(context, collectionProvider);

            // Assert
            result.IsSuccessful.Should().BeFalse();
        }

        [Test]
        public async Task Should_pass_guild_requirement()
        {
            // Arrange
            var attribute = new RequireGuildAttribute();
            var collectionProvider = new ServiceCollection().BuildServiceProvider();
            var context = new SlashCommandContext
            {
                Member = new DiscordGuildMember(new DiscordGuildMemberData()),
                GuildId = 1
            };

            // Act
            var result = await attribute.CheckRequirementAsync(context, collectionProvider);

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }
    }
}