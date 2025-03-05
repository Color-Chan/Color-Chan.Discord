using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Parsers.Interfaces;
using Microsoft.Extensions.Options;

namespace Color_Chan.Discord.Parsers;

/// <inheritdoc />
public class DiscordInteractionParser : IDiscordInteractionParser
{
    private readonly JsonSerializerOptions _serializerOptions;

    /// <summary>
    ///     Creates a new instance of <see cref="DiscordInteractionParser" />.
    /// </summary>
    /// <param name="serializerOptions">The serializer options used for deserialization.</param>
    public DiscordInteractionParser(IOptions<JsonSerializerOptions> serializerOptions)
    {
        _serializerOptions = serializerOptions.Value;
    }
    
    /// <inheritdoc />
    public async Task<DiscordInteractionData> ParseInteractionAsync(Stream body)
    {
        // Convert the JSON body to a DiscordInteractionData object.
        if (body.CanSeek) body.Seek(0, SeekOrigin.Begin);
        var interactionData = await JsonSerializer.DeserializeAsync<DiscordInteractionData>(body, _serializerOptions).ConfigureAwait(false);
        if (interactionData is null) throw new JsonException("Failed to deserialize JSON body to DiscordInteractionData");

        return interactionData;
    }

    /// <inheritdoc />
    public string SerializeInteraction(DiscordInteractionResponseData data)
    {
        return JsonSerializer.Serialize(data, data.GetType(), _serializerOptions);
    }
}