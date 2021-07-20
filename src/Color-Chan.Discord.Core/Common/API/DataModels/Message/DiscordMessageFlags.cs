using System;

namespace Color_Chan.Discord.Core.Common.API.DataModels.Message
{
    [Flags]
    public enum DiscordMessageFlags
    {
        /// <summary>
        ///     Default value for flags, when none are given to a message.
        /// </summary>
        None = 0,

        /// <summary>
        ///     Flag given to messages that have been published to subscribed
        ///     channels (via Channel Following).
        /// </summary>
        CrossPosted = 1 << 0,

        /// <summary>
        ///     Flag given to messages that originated from a message in another
        ///     channel (via Channel Following).
        /// </summary>
        IsCrossPost = 1 << 1,

        /// <summary>
        ///     Flag given to messages that do not display any embeds.
        /// </summary>
        SuppressEmbeds = 1 << 2,

        /// <summary>
        ///     Flag given to messages that the source message for this crossPost
        ///     has been deleted (via Channel Following).
        /// </summary>
        SourceMessageDeleted = 1 << 3,

        /// <summary>
        ///     Flag given to messages that came from the urgent message system.
        /// </summary>
        Urgent = 1 << 4
    }
}