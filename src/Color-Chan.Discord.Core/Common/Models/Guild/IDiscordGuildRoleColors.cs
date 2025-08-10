using System.Drawing;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;

namespace Color_Chan.Discord.Core.Common.Models.Guild;

/// <summary>
///     Represents a discord Role Colors Structure API model.
///     Docs: https://discord.com/developers/docs/topics/permissions#role-object-role-colors-object
/// </summary>
public interface IDiscordGuildRoleColors
{
    /// <summary>
    ///     The primary color of the role.
    /// </summary>
    Color Primary { get; init; }

    /// <summary>
    ///     The secondary color for the role, this will make the role a gradient between the other provided colors
    /// </summary>
    Color? Secondary { get; init; }

    /// <summary>
    ///     The tertiary color for the role, this will turn the gradient into a holographic style
    /// </summary>
    Color? Tertiary { get; init; }
    
    /// <summary>
    ///     Converts the model back to a discord data model so that it can be sent to discord.
    /// </summary>
    /// <returns>
    ///     The converted <see cref="DiscordGuildRoleColorsData" /> model.
    /// </returns>
    DiscordGuildRoleColorsData ToDataModel();
}