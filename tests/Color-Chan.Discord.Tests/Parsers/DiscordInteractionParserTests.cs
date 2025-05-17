using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Color_Chan.Discord.Core.Extensions;
using Color_Chan.Discord.Parsers;
using FluentAssertions;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace Color_Chan.Discord.Tests.Parsers;

[TestFixture]
public class DiscordInteractionParserTests
{
    [Test]
    public async Task Should_Parse_Interaction()
    {
        // Arrange
        var jsonOptions = Options.Create(new JsonSerializerOptions().RegisterJsonOptions());
        var parser = new DiscordInteractionParser(jsonOptions);
        // var jsonString = await File.ReadAllTextAsync("Parsers/TestData/testRequest.json");
        // var stream = new MemoryStream();
        // var writer = new StreamWriter(stream);
        // await writer.WriteAsync(jsonString);
        var filestream = new FileStream("Parsers/TestData/testRequest.json", FileMode.Open);

        // Act
        var result = await parser.ParseInteractionAsync(filestream);

        // Assert
        result.Should().NotBeNull();
    }
}