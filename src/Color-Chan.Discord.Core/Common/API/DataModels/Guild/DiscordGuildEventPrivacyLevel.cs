namespace Color_Chan.Discord.Core.Common.API.DataModels.Guild;

/// <summary>
///     Represents a discord Guild Scheduled Event Privacy Level API model.
///     Docs: https://discord.com/developers/docs/resources/guild-scheduled-event#guild-scheduled-event-object-guild-scheduled-event-privacy-level
/// </summary>
public enum DiscordGuildEventPrivacyLevel
{
    /// <summary>
    ///     The scheduled event is only accessible to guild members
    /// </summary>
    GuildOnly = 2
}