namespace Color_Chan.Discord.Core.Common.API.DataModels;

public enum DiscordChannelType : byte
{
    /// <summary>
    ///     The channel is a text channel.
    /// </summary>
    GuildText = 0,

    /// <summary>
    ///     The channel is a Direct Message channel.
    /// </summary>
    Dm = 1,

    /// <summary>
    ///     The channel is a voice channel.
    /// </summary>
    GuildVoice = 2,

    /// <summary>
    ///     The channel is a group channel.
    /// </summary>
    GroupDm = 3,

    /// <summary>
    ///     The channel is a category channel.
    /// </summary>
    GuildCategory = 4,

    /// <summary>
    ///     The channel is a news channel.
    /// </summary>
    GuildNews = 5,

    /// <summary>
    ///     A channel in which game developers can sell their game on Discord.
    /// </summary>
    GuildStore = 6,

    /// <summary>
    ///     A temporary sub-channel within a GUILD_NEWS channel.
    /// </summary>
    GuildNewsThread = 10,

    /// <summary>
    ///     A temporary sub-channel within a GUILD_TEXT channel.
    /// </summary>
    GuildPublicThread = 11,

    /// <summary>
    ///     A temporary sub-channel within a GUILD_TEXT channel that is only viewable by those invited and those with the
    ///     MANAGE_THREADS permission.
    /// </summary>
    GuildPrivateThread = 12,

    /// <summary>
    ///     A voice channel for hosting events with an audience.
    /// </summary>
    GuildStageVoice = 13
}