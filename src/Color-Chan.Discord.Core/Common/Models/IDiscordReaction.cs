namespace Color_Chan.Discord.Core.Common.Models
{
    public interface IDiscordReaction
    {
        /// <summary>
        ///     Times this emoji has been used to react.
        /// </summary>
        int Count { get; init; }

        /// <summary>
        ///     Whether the current user reacted using this emoji.
        /// </summary>
        bool ByMe { get; init; }

        /// <summary>
        ///     Emoji information.
        /// </summary>
        IDiscordEmoji Emoji { get; init; }
    }
}