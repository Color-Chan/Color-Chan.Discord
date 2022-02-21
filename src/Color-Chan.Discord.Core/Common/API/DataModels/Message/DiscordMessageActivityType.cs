namespace Color_Chan.Discord.Core.Common.API.DataModels.Message
{
    /// <summary>
    ///     Represents a discord Message Activity Types API model.
    ///     Docs: https://discord.com/developers/docs/resources/channel#message-object-message-activity-types
    /// </summary>
    public enum DiscordMessageActivityType : byte
    {
        /// <summary>
        ///     A Join message type.
        /// </summary>
        Join = 1,
        
        /// <summary>
        ///     A Spectate message type.
        /// </summary>
        Spectate = 2,
        
        /// <summary>
        ///     A Listen message type.
        /// </summary>
        Listen = 3,
        
        /// <summary>
        ///     A Join request message type.
        /// </summary>
        JoinRequest = 5
    }
}