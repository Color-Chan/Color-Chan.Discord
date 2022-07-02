namespace Color_Chan.Discord.Core.Common.API.DataModels;

public enum DiscordButtonStyle
{
    /// <summary>
    ///     Blurple.
    /// </summary>
    /// <remarks>
    ///     Requires 'custom_id'.
    /// </remarks>
    Primary = 1,

    /// <summary>
    ///     Grey.
    /// </summary>
    /// <remarks>
    ///     Requires 'custom_id'.
    /// </remarks>
    Secondary = 2,

    /// <summary>
    ///     Green.
    /// </summary>
    /// <remarks>
    ///     Requires 'custom_id'.
    /// </remarks>
    Success = 3,

    /// <summary>
    ///     Red.
    /// </summary>
    /// <remarks>
    ///     Requires 'custom_id'.
    /// </remarks>
    Danger = 4,

    /// <summary>
    ///     Grey, navigates to URL.
    /// </summary>
    /// <remarks>
    ///     Requires url.
    /// </remarks>
    Link = 5
}