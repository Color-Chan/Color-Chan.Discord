namespace Color_Chan.Discord.Core.Common.API.DataModels
{
    /// <summary>
    ///     Represents a discord component types API model.
    ///     Docs: https://discord.com/developers/docs/interactions/message-components#component-object-component-types
    /// </summary>
    public enum DiscordComponentType
    {
        /// <summary>
        ///     A container for other components.
        /// </summary>
        ActionRow = 1,

        /// <summary>
        ///     A button object.
        /// </summary>
        Button = 2,

        /// <summary>
        ///     A select menu for picking from choices.
        /// </summary>
        SelectMenu = 3
    }
}