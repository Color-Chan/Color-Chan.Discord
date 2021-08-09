using System.Linq;
using Color_Chan.Discord.Commands.Services.Implementations.Builders;
using Color_Chan.Discord.Commands.Tests.Valid;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Color_Chan.Discord.Commands.Tests.Services.Implementations.Builders
{
    [TestFixture]
    public class SlashCommandRequirementBuildServiceTests
    {
        [Test]
        public void Should_get_one_requirementAttribute_from_CommandMethod14()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<SlashCommandRequirementBuildService>>();
            var buildService = new SlashCommandRequirementBuildService(loggerMock.Object);

            // Act
            var attributes = buildService.GetCommandRequirements(typeof(ValidMockCommandModule5).Methods().First());

            // Assert
            attributes.Count().Should().Be(7);
        }
    }
}