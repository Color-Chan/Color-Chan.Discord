using System;
using System.Drawing;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;

namespace Color_Chan.Discord.Core.Common.API.Params.Guild;

/// <summary>
///     Represents a discord Create Guild role API request model.
///     Docs: https://discord.com/developers/docs/resources/guild#membership-screening-object-json-params
/// </summary>
public record DiscordCreateGuildRole
{
    /// <summary>
    ///     The name of the role. Default is "new role"
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; } = null!;

    /// <summary>
    ///     The bitwise value of the enabled/disabled permissions. Default is @everyone permissions in guild.
    /// </summary>
    [JsonPropertyName("permissions")]
    public DiscordPermission Permissions { get; init; }

    /// <summary>
    ///     The RGB color value.
    /// </summary>
    /// <remarks>
    ///     Color will still be returned by the API, but using the colors field is recommended when doing requests.
    /// </remarks>
    [JsonPropertyName("color")]
    [Obsolete("Use Colors property instead.")]
    public Color Color { get; set; }

    /// <summary>
    ///     The role's colors.
    /// </summary>
    [JsonPropertyName("colors")]
    public DiscordGuildRoleColorsData Colors { get; set; } = null!;

    /// <summary>
    ///     Whether the role should be displayed separately in the sidebar. Default: false;
    /// </summary>
    [JsonPropertyName("hoist")]
    public bool IsHoisted { get; set; }

    /// <summary>
    ///     Whether the role should be mentionable. Default: false;
    /// </summary>
    [JsonPropertyName("mentionable")]
    public bool Mentionable { get; set; }
}