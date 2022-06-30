using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.Models;

namespace Color_Chan.Discord.Rest.Models;

public record DiscordReaction : IDiscordReaction
{
    public DiscordReaction(DiscordReactionData data)
    {
        Count = data.Count;
        ByMe = data.ByMe;
        Emoji = new DiscordEmoji(data.Emoji);
    }

    /// <inheritdoc />
    public int Count { get; init; }

    /// <inheritdoc />
    public bool ByMe { get; init; }

    /// <inheritdoc />
    public IDiscordEmoji Emoji { get; init; }
}