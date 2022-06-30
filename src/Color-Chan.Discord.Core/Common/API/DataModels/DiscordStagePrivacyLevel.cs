namespace Color_Chan.Discord.Core.Common.API.DataModels;

/// <summary>
///     Represents a Privacy Level API model.
///     https://discord.com/developers/docs/resources/stage-instance#stage-instance-object-privacy-level
/// </summary>
public enum DiscordStagePrivacyLevel
{
    /// <summary>
    ///     The Stage instance is visible publicly, such as on Stage discovery.
    /// </summary>
    Public = 1,

    /// <summary>
    ///     The Stage instance is visible to only guild members.
    /// </summary>
    GuildOnly = 2
}