using System.Drawing;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Core.Common.Models.Guild;

namespace Color_Chan.Discord.Rest.Models.Guild;

/// <inheritdoc cref="IDiscordGuildRoleColors" />
public class DiscordGuildRoleColors : IDiscordGuildRoleColors
{
    /// <inheritdoc />
    public Color Primary { get; init; }

    /// <inheritdoc />
    public Color? Secondary { get; init; }

    /// <inheritdoc />
    public Color? Tertiary { get; init; }

    /// <inheritdoc />
    public DiscordGuildRoleColorsData ToDataModel()
    {
        return new DiscordGuildRoleColorsData
        {
            Primary = Primary,
            Secondary = Secondary,
            Tertiary = Tertiary
        };
    }
}