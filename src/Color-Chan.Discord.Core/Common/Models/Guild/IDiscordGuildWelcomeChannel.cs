namespace Color_Chan.Discord.Core.Common.Models.Guild;

/// <summary>
///     Represents a discord Welcome Screen Channel Structure API model.
///     Docs: https://discord.com/developers/docs/resources/guild#welcome-screen-object-welcome-screen-channel-structure
/// </summary>
public interface IDiscordGuildWelcomeChannel
{
    /// <summary>
    ///     The channel's id.
    /// </summary>
    ulong ChannelId { get; set; }

    /// <summary>
    ///     The description shown for the channel
    /// </summary>
    string? Description { get; set; }

    /// <summary>
    ///     The emoji id, if the emoji is custom.
    /// </summary>
    string? EmojiId { get; set; }

    /// <summary>
    ///     The emoji name if custom, the unicode character if standard, or null if no emoji is set.
    /// </summary>
    string? EmojiName { get; set; }
}