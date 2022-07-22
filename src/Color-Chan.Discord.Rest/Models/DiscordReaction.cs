using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.Models;

namespace Color_Chan.Discord.Rest.Models;

/// <inheritdoc cref="IDiscordReaction" />
public record DiscordReaction : IDiscordReaction
{
    /// <summary>
    ///     Initializes a new <see cref="DiscordReaction" />
    /// </summary>
    /// <param name="data">The data needed to create the <see cref="DiscordReaction" />.</param>
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