namespace Color_Chan.Discord.Core.Common.API.DataModels.Embed
{
    /// <summary>
    ///     The specific type for a <see cref="DiscordEmbedData"/>.
    /// </summary>
    public enum DiscordEmbedType
    {
        /// <summary>
        ///     Generic embed rendered from embed attributes
        /// </summary>
        Rich,
        /// <summary>
        ///     An image embed.
        /// </summary>
        Image,
        /// <summary>
        ///     An video embed.
        /// </summary>
        Video,
        /// <summary>
        ///     A gif image embed rendered as a video embed.
        /// </summary>
        Gif,
        /// <summary>
        ///     An article embed.
        /// </summary>
        Article,
        /// <summary>
        ///     A link embed.
        /// </summary>
        Link
    }
}
