using Color_Chan.Discord.Core.Common.API.DataModels;

namespace Color_Chan.Discord.Core.Common.Models
{
    /// <summary>
    ///     Represents a Stage Instance Structure API model.
    ///     Docs: https://discord.com/developers/docs/resources/stage-instance#stage-instance-object-stage-instance-structure
    /// </summary>
    public interface IDiscordStageInstance
    {
        /// <summary>
        ///     The id of this Stage instance.
        /// </summary>
        ulong Id { get; init; }

        /// <summary>
        ///     The guild id of the associated Stage channel.
        /// </summary>
        ulong GuildId { get; init; }

        /// <summary>
        ///     The id of the associated Stage channel.
        /// </summary>
        ulong ChannelId { get; init; }

        /// <summary>
        ///     The topic of the Stage instance (1-120 characters).
        /// </summary>
        string Topic { get; init; }

        /// <summary>
        ///     The privacy level of the Stage instance.
        /// </summary>
        DiscordStagePrivacyLevel PrivacyLevel { get; init; }

        /// <summary>
        ///     Whether or not Stage discovery is disabled.
        /// </summary>
        bool DiscoverableDisabled { get; init; }
    }
}