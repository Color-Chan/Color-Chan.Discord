using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.Models;

namespace Color_Chan.Discord.Rest.Models;

/// <inheritdoc cref="IDiscordOverwrite" />
public record DiscordOverwrite : IDiscordOverwrite
{
    /// <summary>
    ///     Initializes a new <see cref="DiscordOverwrite" />
    /// </summary>
    /// <param name="data">The data needed to create the <see cref="DiscordOverwrite" />.</param>
    public DiscordOverwrite(DiscordOverwriteData data)
    {
        TargetId = data.TargetId;
        TargetType = data.TargetType;
        Allow = data.Allow;
        Deny = data.Deny;
    }

    /// <inheritdoc />
    public ulong TargetId { get; init; }

    /// <inheritdoc />
    public DiscordPermissionTargetType TargetType { get; init; }

    /// <inheritdoc />
    public DiscordPermission Allow { get; init; }

    /// <inheritdoc />
    public DiscordPermission Deny { get; init; }
}