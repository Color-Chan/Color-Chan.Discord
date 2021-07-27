using Color_Chan.Discord.Core.Common.Models;

namespace Color_Chan.Discord.Rest.Models
{
    public record DiscordReaction : IDiscordReaction
    {
        /// <inheritdoc />
        public int Count { get; init; }

        /// <inheritdoc />
        public bool ByMe { get; init; }

        /// <inheritdoc />
        public IDiscordEmoji Emoji { get; init; } = null!;
    }
}