using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Core.Common.Models.Guild;

namespace Color_Chan.Discord.Rest.Models.Guild;

/// <inheritdoc cref="IDiscordGuildWelcomeChannel" />
public record DiscordGuildWelcomeChannel : IDiscordGuildWelcomeChannel
{
    /// <summary>
    ///     Initializes a new <see cref="DiscordGuildWelcomeChannel" />
    /// </summary>
    /// <param name="data">The data needed to create the <see cref="DiscordGuildWelcomeChannel" />.</param>
    public DiscordGuildWelcomeChannel(DiscordGuildWelcomeChannelData data)
    {
        ChannelId = data.ChannelId;
        Description = data.Description;
        EmojiId = data.EmojiId;
        EmojiName = data.EmojiName;
    }

    /// <inheritdoc />
    public ulong ChannelId { get; set; }

    /// <inheritdoc />
    public string? Description { get; set; }

    /// <inheritdoc />
    public string? EmojiId { get; set; }

    /// <inheritdoc />
    public string? EmojiName { get; set; }
}