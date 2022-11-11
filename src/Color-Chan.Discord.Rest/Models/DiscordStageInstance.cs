using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.Models;

namespace Color_Chan.Discord.Rest.Models;

/// <inheritdoc cref="IDiscordStageInstance" />
public class DiscordStageInstance : IDiscordStageInstance
{
    /// <summary>
    ///     Initializes a new <see cref="DiscordStageInstance" />
    /// </summary>
    /// <param name="data">The data needed to create the <see cref="DiscordStageInstance" />.</param>
    public DiscordStageInstance(DiscordStageinstanceData data)
    {
        Id = data.Id;
        GuildId = data.GuildId;
        ChannelId = data.ChannelId;
        Topic = data.Topic;
        PrivacyLevel = data.PrivacyLevel;
        DiscoverableDisabled = data.DiscoverableDisabled;
    }

    /// <inheritdoc />
    public ulong Id { get; init; }

    /// <inheritdoc />
    public ulong GuildId { get; init; }

    /// <inheritdoc />
    public ulong ChannelId { get; init; }

    /// <inheritdoc />
    public string Topic { get; init; }

    /// <inheritdoc />
    public DiscordStagePrivacyLevel PrivacyLevel { get; init; }

    /// <inheritdoc />
    public bool DiscoverableDisabled { get; init; }
}