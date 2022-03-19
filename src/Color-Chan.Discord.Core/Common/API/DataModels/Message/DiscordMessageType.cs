﻿namespace Color_Chan.Discord.Core.Common.API.DataModels.Message
{
    public enum DiscordMessageType
    {
        /// <summary>
        ///     The default message type.
        /// </summary>
        Default = 0,

        /// <summary>
        ///     The message when a recipient is added.
        /// </summary>
        RecipientAdd = 1,

        /// <summary>
        ///     The message when a recipient is removed.
        /// </summary>
        RecipientRemove = 2,

        /// <summary>
        ///     The message when a user is called.
        /// </summary>
        Call = 3,

        /// <summary>
        ///     The message when a channel name is changed.
        /// </summary>
        ChannelNameChange = 4,

        /// <summary>
        ///     The message when a channel icon is changed.
        /// </summary>
        ChannelIconChange = 5,

        /// <summary>
        ///     The message when another message is pinned.
        /// </summary>
        ChannelPinnedMessage = 6,

        /// <summary>
        ///     The message when a new member joined.
        /// </summary>
        GuildMemberJoin = 7,

        /// <summary>
        ///     The message for when a user boosts a guild.
        /// </summary>
        UserPremiumGuildSubscription = 8,

        /// <summary>
        ///     The message for when a guild reaches Tier 1 of Nitro boosts.
        /// </summary>
        UserPremiumGuildSubscriptionTier1 = 9,

        /// <summary>
        ///     The message for when a guild reaches Tier 2 of Nitro boosts.
        /// </summary>
        UserPremiumGuildSubscriptionTier2 = 10,

        /// <summary>
        ///     The message for when a guild reaches Tier 3 of Nitro boosts.
        /// </summary>
        UserPremiumGuildSubscriptionTier3 = 11,

        /// <summary>
        ///     The message for when a news channel subscription is added to a text channel.
        /// </summary>
        ChannelFollowAdd = 12,

        /// <summary>
        ///     The message is an inline reply.
        /// </summary>
        Reply = 19
    }
}