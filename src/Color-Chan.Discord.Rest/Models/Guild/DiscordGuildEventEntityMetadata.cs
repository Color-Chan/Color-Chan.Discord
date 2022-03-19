using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Core.Common.Models.Guild;

namespace Color_Chan.Discord.Rest.Models.Guild;

/// <inheritdoc />
public class DiscordGuildEventEntityMetadata : IDiscordGuildEventEntityMetadata
{
    /// <summary>
    ///     Initializes a new <see cref="DiscordGuildEventEntityMetadata"/>
    /// </summary>
    /// <param name="data">The data needed to create the <see cref="DiscordGuildEventEntityMetadata"/>.</param>
    public DiscordGuildEventEntityMetadata(DiscordGuildEventEntityMetadataData data)
    {
        Location = data.Location;
    }
    
    /// <inheritdoc />
    public string? Location { get; set; }
}