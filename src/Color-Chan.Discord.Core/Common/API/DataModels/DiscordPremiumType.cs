namespace Color_Chan.Discord.Core.Common.API.DataModels;

/// <summary>
///     Premium types denote the level of premium a user has.
/// </summary>
public enum DiscordPremiumType : byte
{
    /// <summary>
    ///     None.
    /// </summary>
    None = 0,

    /// <summary>
    ///     Nitro classic.
    /// </summary>
    NitroClassic = 1,

    /// <summary>
    ///     Nitro.
    /// </summary>
    Nitro = 2
}