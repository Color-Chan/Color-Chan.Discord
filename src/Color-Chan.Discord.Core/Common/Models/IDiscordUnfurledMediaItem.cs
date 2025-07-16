using Color_Chan.Discord.Core.Common.API.DataModels;

namespace Color_Chan.Discord.Core.Common.Models;

/// <summary>
///     Represents a discord Unfurled Media Item Data API model.
///     Docs: https://discord.com/developers/docs/components/reference#unfurled-media-item-structure
/// </summary>
public interface IDiscordUnfurledMediaItem
{
    /// <summary>
    ///     The URL of the media item.
    /// </summary>
    string Url { get; init; }

    /// <summary>
    ///     Converts the model back to a discord data model so that it can be send to discord.
    /// </summary>
    /// <returns>
    ///     The converted <see cref="DiscordUnfurledMediaItemData" />.
    /// </returns>
    DiscordUnfurledMediaItemData ToDataModel();
}