using Color_Chan.Discord.Core.Common.API.DataModels.Message;
using Color_Chan.Discord.Core.Common.Models.Message;

namespace Color_Chan.Discord.Rest.Models.Message;

/// <inheritdoc cref="IDiscordMessageReference" />
public class DiscordMessageReference : IDiscordMessageReference
{
    /// <summary>
    ///     Initializes a new <see cref="DiscordMessageReference" />
    /// </summary>
    /// <param name="data">The data needed to create the <see cref="DiscordMessageReference" />.</param>
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