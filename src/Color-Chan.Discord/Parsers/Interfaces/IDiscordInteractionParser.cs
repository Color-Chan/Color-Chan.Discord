using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;

namespace Color_Chan.Discord.Parsers.Interfaces;

/// <summary>
///     Parses a raw json body to a <see cref="DiscordInteractionData"/>.
/// </summary>
public interface IDiscordInteractionParser
{
    /// <summary>
    ///     Parses a raw json body to a <see cref="DiscordInteractionData"/>.
    /// </summary>
    /// <param name="body">
    ///     The raw json body from the discord interaction request.
    /// </param>
    /// <returns>
    ///     The parsed <see cref="DiscordInteractionData"/>.
    /// </returns>
    /// <exception cref="JsonException">
    ///     Thrown when the body could not be deserialized to a <see cref="DiscordInteractionData"/>.
    /// </exception>
    Task<DiscordInteractionData> ParseInteractionAsync(Stream body);

    /// <summary>
    ///     Serializes a <see cref="DiscordInteractionResponseData"/> to a json <see cref="string"/>.
    /// </summary>
    /// <param name="data">The <see cref="DiscordInteractionResponseData"/> to serialize.</param>
    /// <returns>
    ///     A json <see cref="string"/> representing the <see cref="DiscordInteractionResponseData"/>.
    /// </returns>
    string SerializeInteraction(DiscordInteractionResponseData data);
}