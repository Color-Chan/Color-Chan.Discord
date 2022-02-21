namespace Color_Chan.Discord.Core.Common.API.DataModels.Application
{
    /// <summary>
    ///     Represents a discord Application Command Types API model.
    ///     Docs: https://discord.com/developers/docs/interactions/application-commands#application-command-object-application-command-types
    /// </summary>
    public enum DiscordApplicationCommandTypes
    {
        /// <summary>
        ///     Slash commands; a text-based command that shows up when a user types `/`.
        /// </summary>
        ChatInput = 1,

        /// <summary>
        ///     UI-based command that shows up when you right click or tap on a user.
        /// </summary>
        User = 2,

        /// <summary>
        ///     A UI-based command that shows up when you right click or tap on a messages.
        /// </summary>
        Message = 3
    }
}