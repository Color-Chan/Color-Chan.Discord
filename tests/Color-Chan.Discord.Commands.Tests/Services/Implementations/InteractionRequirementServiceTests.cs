using System;
using System.Reflection;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Models.Contexts;
using Color_Chan.Discord.Commands.Models.Info;
using Color_Chan.Discord.Commands.Services.Implementations;
using Color_Chan.Discord.Commands.Tests.Valid;
using Color_Chan.Discord.Commands.Tests.Valid.Requirements;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Color_Chan.Discord.Commands.Tests.Services.Implementations;

[TestFixture]
public class InteractionRequirementServiceTests
{
    [TestCase(0, true)]
    [TestCase(1, false)]
    [TestCase(2, true)]
    public async Task ExecuteSlashCommandRequirementsAsync_should_get_one_error(int methodIndex, bool shouldHaveError)
    {
        // Arrange
        var requirementServiceLoggerMock = new Mock<ILogger<InteractionRequirementService>>();
        var contextMock = new Mock<ISlashCommandContext>();
        var serviceProviderMock = new Mock<IServiceProvider>();
        var requirementService = new InteractionRequirementService(requirementServiceLoggerMock.Object);

        var module = typeof(ValidMockCommandModule1).GetTypeInfo();
        var method = module.GetMethods()[methodIndex];
        var requirementsAttributes = method.GetCustomAttributes<BoolRequirement>();
        var commandInfo = new SlashCommandInfo("test", "desc", false, method, module)
        {
            Requirements = requirementsAttributes
        };

        // Act
        var result = await requirementService.ExecuteRequirementsAsync(commandInfo.Requirements, contextMock.Object, serviceProviderMock.Object).ConfigureAwait(false);

        // Assert
        result.IsSuccessful.Should().Be(!shouldHaveError);
    }
}