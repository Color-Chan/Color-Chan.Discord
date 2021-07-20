namespace Color_Chan.Discord.Core.Common.API.DataModels
{
    public enum DiscordChannelType : byte
    {
        /// <summary>
        ///     The channel is a text channel.
        /// </summary>
        Text = 0,

        /// <summary>
        ///     The channel is a Direct Message channel.
        /// </summary>
        Dm = 1,

        /// <summary>
        ///     The channel is a voice channel.
        /// </summary>
        Voice = 2,

        /// <summary>
        ///     The channel is a group channel.
        /// </summary>
        Group = 3,

        /// <summary>
        ///     The channel is a category channel.
        /// </summary>
        Category = 4,

        /// <summary>
        ///     The channel is a news channel.
        /// </summary>
        News = 5
    }
}