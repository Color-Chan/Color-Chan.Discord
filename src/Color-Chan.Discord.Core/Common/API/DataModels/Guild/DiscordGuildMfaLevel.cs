namespace Color_Chan.Discord.Core.Common.API.DataModels.Guild;

/// <summary>
///     Represents a discord MFA Level API model.
///     Docs: https://discord.com/developers/docs/resources/guild#guild-object-mfa-level
/// </summary>
public enum DiscordGuildMfaLevel
{
    /// <summary>
    ///     Guild has no MFA/2FA requirement for moderation actions.
    /// </summary>
    None = 0,

    /// <summary>
    ///     Guild has a 2FA requirement for moderation actions.
    /// </summary>
    Elevated = 1
}