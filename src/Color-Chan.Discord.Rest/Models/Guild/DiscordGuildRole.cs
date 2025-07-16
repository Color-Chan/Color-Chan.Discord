using System.Drawing;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Core.Common.Models.Guild;

#pragma warning disable CS0618 // Type or member is obsolete

namespace Color_Chan.Discord.Rest.Models.Guild;

/// <inheritdoc cref="IDiscordGuildRole" />
public record DiscordGuildRole : IDiscordGuildRole
{
    /// <summary>
    ///     Initializes a new <see cref="DiscordGuildRole" />
    /// </summary>
    /// <param name="data">The data needed to create the <see cref="DiscordGuildRole" />.</param>
    public DiscordGuildRole(DiscordGuildRoleData data)
    {
        Id = data.Id;
        Name = data.Name;
        Color = data.Color;
        IsHoisted = data.IsHoisted;
        Icon = data.Icon;
        UnicodeEmoji = data.UnicodeEmoji;
        Position = data.Position;
        Permissions = data.Permissions;
        Managed = data.Managed;
        Mentionable = data.Mentionable;
        Tags = data.Tags is not null ? new DiscordGuildRoleTags(data.Tags) : null;
        Flags = data.Flags;

        // Colors should never be null in production. But it can happen when using an older version of the API.
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (data.Colors is not null)
        {
            Colors = new DiscordGuildRoleColors
            {
                Primary = data.Colors.Primary,
                Secondary = data.Colors.Secondary,
                Tertiary = data.Colors.Tertiary
            };
        }
        else
        {
            Colors = null!;
        }
    }

    /// <inheritdoc />
    public ulong Id { get; init; }

    /// <inheritdoc />
    public string Name { get; init; }

    /// <inheritdoc />
    public Color Color { get; init; }

    /// <inheritdoc />
    public IDiscordGuildRoleColors Colors { get; init; }

    /// <inheritdoc />
    public bool IsHoisted { get; init; }

    /// <inheritdoc />
    public string? Icon { get; init; }

    /// <inheritdoc />
    public string? UnicodeEmoji { get; init; }

    /// <inheritdoc />
    public int Position { get; init; }

    /// <inheritdoc />
    public DiscordPermission Permissions { get; init; }

    /// <inheritdoc />
    public bool Managed { get; init; }

    /// <inheritdoc />
    public bool Mentionable { get; init; }

    /// <inheritdoc />
    public IDiscordGuildRoleTags? Tags { get; init; }

    /// <inheritdoc />
    public DiscordGuildRoleFlags Flags { get; init; }

    /// <inheritdoc />
    public DiscordGuildRoleData ToDataModel()
    {
        return new DiscordGuildRoleData
        {
            Color = Color,
            Colors = Colors.ToDataModel(),
            Id = Id,
            Managed = Managed,
            Mentionable = Mentionable,
            Name = Name,
            Permissions = Permissions,
            Position = Position,
            IsHoisted = IsHoisted,
            Icon = Icon,
            UnicodeEmoji = UnicodeEmoji,
            Tags = Tags?.ToDataModel() ?? null,
            Flags = Flags
        };
    }
}