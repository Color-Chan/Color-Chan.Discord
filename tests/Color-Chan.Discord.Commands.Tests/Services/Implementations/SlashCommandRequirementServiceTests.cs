﻿using System;
using System.Reflection;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Info;
using Color_Chan.Discord.Commands.Services.Implementations;
using Color_Chan.Discord.Commands.Tests.Valid;
using Color_Chan.Discord.Commands.Tests.Valid.Requirements;
using Color_Chan.Discord.Core;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Color_Chan.Discord.Commands.Tests.Services.Implementations
{
    [TestFixture]
    public class SlashCommandRequirementServiceTests
    {
        [TestCase(0, 3)]
        [TestCase(1, 0)]
        [TestCase(2, 1)]
        public async Task ExecuteSlashCommandRequirementsAsync_should_get_one_error(int methodIndex, int expectedErrors)
        {
            // Arrange
            var requirementServiceLoggerMock = new Mock<ILogger<SlashCommandRequirementService>>();
            var contextMock = new Mock<ISlashCommandContext>();
            var serviceProviderMock = new Mock<IServiceProvider>();
            var requirementService = new SlashCommandRequirementService(requirementServiceLoggerMock.Object);

            var module = typeof(ValidMockCommandModule1).GetTypeInfo();
            var method = module.GetMethods()[methodIndex];
            var attributes = method.GetCustomAttributes<BoolRequirement>();
            var commandInfo = new SlashCommandInfo("test", "desc", method, module, attributes);

            // Act
            var result = await requirementService.ExecuteSlashCommandRequirementsAsync(commandInfo, contextMock.Object, serviceProviderMock.Object).ConfigureAwait(false);

            // Assert
            result.Count.Should().Be(expectedErrors);
        }
    }
}