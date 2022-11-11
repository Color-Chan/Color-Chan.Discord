using Color_Chan.Discord.Core.Common.API.DataModels.Application;
using Color_Chan.Discord.Core.Common.Models.Application;

namespace Color_Chan.Discord.Rest.Models.Application;

/// <inheritdoc />
public class DiscordApplicationCommandPermissions : IDiscordApplicationCommandPermissions
{
    /// <summary>
    ///     Initializes a new <see cref="DiscordApplicationCommandPermissions" />
    /// </summary>
    /// <param name="data">The data needed to create the <see cref="DiscordApplicationCommandPermissions" />.</param>
    public DiscordApplicationCommandPermissions(DiscordApplicationCommandPermissionsData data)
    {
        Id = data.Id;
        Type = data.Type;
        Allow = data.Allow;
    }

    /// <inheritdoc />
    public ulong Id { get; set; }

    /// <inheritdoc />
    public DiscordApplicationCommandPermissionsType Type { get; set; }

    /// <inheritdoc />
    public bool Allow { get; set; }
}