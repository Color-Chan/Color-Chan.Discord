using Color_Chan.Discord.Core.Common.API.DataModels.Message;
using Color_Chan.Discord.Core.Common.Models.Message;

namespace Color_Chan.Discord.Rest.Models.Message;

public class DiscordMessageReference : IDiscordMessageReference
{
    public DiscordMessageReference(DiscordMessageReferenceData data)
    {
        MessageId = data.MessageId;
        ChannelId = data.ChannelId;
        GuildId = data.GuildId;
        FailIfNotExists = data.FailIfNotExists;
    }

    /// <inheritdoc />
    public ulong? MessageId { get; set; }

    /// <inheritdoc />
    public ulong? ChannelId { get; set; }

    /// <inheritdoc />
    public ulong? GuildId { get; set; }

    /// <inheritdoc />
    public ulong? FailIfNotExists { get; set; }
}