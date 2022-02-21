namespace Color_Chan.Discord.Core.Common.API.DataModels
{
    /// <summary>
    ///     Represents a discord Webhook Types API model.
    ///     Docs: https://discord.com/developers/docs/resources/webhook#webhook-object-webhook-types
    /// </summary>
    public enum DiscordWebhookType
    {
        /// <summary>
        ///     Incoming Webhooks can post messages to channels with a generated token.
        /// </summary>
        Incoming = 1,

        /// <summary>
        ///     Channel Follower Webhooks are internal webhooks used with Channel Following to post new messages into channels.
        /// </summary>
        ChannelFollower = 2,

        /// <summary>
        ///     Application webhooks are webhooks used with Interactions.
        /// </summary>
        Application = 3
    }
}