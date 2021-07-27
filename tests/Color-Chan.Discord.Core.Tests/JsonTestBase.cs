using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        [Test]
        public async Task Should_serialize()
        {
            // Arrange
            var entity = new TEntity();
            await using var stream = new MemoryStream();

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await JsonSerializer.SerializeAsync(stream, entity, _jsonOptions));
        }

        [TestCaseSource(nameof(GetFiles))]
        public async Task Should_deserialize(string filepath)
        {
            // Arrange
            await using var testJson = File.OpenRead(filepath);

            // Act
            var entity = await JsonSerializer.DeserializeAsync<TEntity>(testJson, _jsonOptions);

            // Assert
            entity.Should().NotBeNull();
        }

        [TestCaseSource(nameof(GetFiles))]
        public async Task Should_serialize_and_deserialize(string filepath)
        {
            // Arrange
            await using var testJson = File.OpenRead(filepath);
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

        /// <summary>
        ///     Get the test JSON files for the <see cref="TEntity" />.
        /// </summary>
        protected static IEnumerable<string> GetFiles()
        {
            var dataModelDirPath = Path.Combine("TestJson/API/DataModels/", typeof(TEntity).Name);
            var paramsPath = Path.Combine("TestJson/API/Params/", typeof(TEntity).Name);

            if (Directory.Exists(dataModelDirPath) && Directory.GetFiles(dataModelDirPath).Any())
                foreach (var file in Directory.GetFiles(dataModelDirPath))
                    yield return file;
            else if (Directory.Exists(paramsPath) && Directory.GetFiles(paramsPath).Any())
                foreach (var file in Directory.GetFiles(paramsPath))
                    yield return file;
            else Assert.Fail($"Failed to find test JSON files for {typeof(TEntity).Name}");
        }
    }
}