using Color_Chan.Discord.Core.Common.API.DataModels.Embed;

namespace Color_Chan.Discord.Core.Common.Models.Embed;

/// <summary>
///     Represents a discord Embed Provider Structure API model.
///     Docs: https://discord.com/developers/docs/resources/channel#embed-object-embed-provider-structure
/// </summary>
public interface IDiscordEmbedProvider
{
    /// <summary>
    ///     Name of provider.
    /// </summary>
    string? Name { get; init; }

    /// <summary>
    ///     Url of provider.
    /// </summary>
    string? Url { get; init; }

    /// <summary>
    ///     Converts the model back to a discord data model so that it can be send to discord.
    /// </summary>
    /// <returns>
    ///     The converted <see cref="DiscordEmbedProviderData" />.
    /// </returns>
    DiscordEmbedProviderData ToDataModel();
}