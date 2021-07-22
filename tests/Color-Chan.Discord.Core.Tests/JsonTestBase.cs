using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Color_Chan.Discord.Core.Extensions;
using FluentAssertions;
using NUnit.Framework;

namespace Color_Chan.Discord.Core.Tests
{
    public class JsonTestBase<TEntity> where TEntity : new()
    {
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true
        }.RegisterJsonOptions();

        private string _filePath = string.Empty;

        [SetUp]
        public void Setup()
        {
            var dataModelPath = Path.Combine("TestJson/API/DataModels/", typeof(TEntity).Name + ".json");
            if (File.Exists(dataModelPath))
            {
                _filePath = dataModelPath;
                return;
            }

            var paramsPath = Path.Combine("TestJson/API/Params/", typeof(TEntity).Name + ".json");
            if (File.Exists(paramsPath))
            {
                _filePath = paramsPath;
                return;
            }

            Assert.Fail($"Failed to find test json file for {typeof(TEntity).Name}.");
        }

        [Test]
        public async Task Should_serialize()
        {
            // Arrange
            var entity = new TEntity();
            await using var stream = new MemoryStream();

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await JsonSerializer.SerializeAsync(stream, entity, _jsonOptions));
        }

        [Test]
        public async Task Should_deserialize()
        {
            // Arrange
            await using var testJson = File.OpenRead(_filePath);

            // Act
            var entity = await JsonSerializer.DeserializeAsync<TEntity>(testJson, _jsonOptions);

            // Assert
            entity.Should().NotBeNull();
        }

        [Test]
        public async Task Should_serialize_and_deserialize()
        {
            // Arrange
            await using var testJson = File.OpenRead(_filePath);
            await using var stream = new MemoryStream();

            // Act
            var entity = await JsonSerializer.DeserializeAsync<TEntity>(testJson, _jsonOptions);
            await JsonSerializer.SerializeAsync(stream, entity, _jsonOptions);

            await stream.FlushAsync();
            stream.Seek(0, SeekOrigin.Begin);
            testJson.Seek(0, SeekOrigin.Begin);

            // Assert
            entity.Should().NotBeNull();

            var serialized = await JsonDocument.ParseAsync(stream);
            var original = await JsonDocument.ParseAsync(testJson);

            var serializedString = serialized.RootElement.GetRawText();
            var originalString = original.RootElement.GetRawText();

            serializedString.Should().BeEquivalentTo(originalString);
        }
    }
}