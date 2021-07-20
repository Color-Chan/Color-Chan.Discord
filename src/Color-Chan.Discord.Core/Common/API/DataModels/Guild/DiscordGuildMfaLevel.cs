namespace Color_Chan.Discord.Core.Common.API.DataModels.Guild
{
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
}