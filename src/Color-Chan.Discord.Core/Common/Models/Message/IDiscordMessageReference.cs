namespace Color_Chan.Discord.Core.Common.Models.Message
{
    /// <summary>
    ///     Represents a discord Message Reference Structure API model.
    ///     Docs: https://discord.com/developers/docs/resources/channel#message-reference-object-message-reference-structure
    /// </summary>
    public interface IDiscordMessageReference
    {
        /// <summary>
        ///     Id of the originating message.
        /// </summary>
        ulong? MessageId { get; set; }

        /// <summary>
        ///     Id of the originating message's channel.
        /// </summary>
        /// <remarks>
        ///     channel_id is optional when creating a reply,
        ///     but will always be present when receiving an event/response that includes this data model.
        /// </remarks>
        ulong? ChannelId { get; set; }

        /// <summary>
        ///     Id of the originating message's guild.
        /// </summary>
        ulong? GuildId { get; set; }

        /// <summary>
        ///     When sending, whether to error if the referenced message doesn't exist instead of sending as a normal (non-reply)
        ///     message, default true.
        /// </summary>
        ulong? FailIfNotExists { get; set; }
    }
}