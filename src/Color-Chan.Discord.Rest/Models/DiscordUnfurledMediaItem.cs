using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.Models;

namespace Color_Chan.Discord.Rest.Models;

/// <inheritdoc />
public class DiscordUnfurledMediaItem : IDiscordUnfurledMediaItem
{
    /// <inheritdoc />
    public required string Url { get; init; }

    /// <inheritdoc />
    public DiscordUnfurledMediaItemData ToDataModel()
    {
        return new DiscordUnfurledMediaItemData
        {
            Url = Url
        };
    }
}