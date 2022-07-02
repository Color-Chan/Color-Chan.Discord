using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Core.Common.Models.Guild;

namespace Color_Chan.Discord.Rest.Models.Guild;

public record DiscordGuildWelcomeChannel : IDiscordGuildWelcomeChannel
{
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