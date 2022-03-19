using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.Models;

namespace Color_Chan.Discord.Rest.Models;

/// <inheritdoc />
public class DiscordVoiceRegion : IDiscordVoiceRegion
{
    /// <summary>
    ///     Initializes a new <see cref="DiscordVoiceRegion"/>
    /// </summary>
    /// <param name="data">The data needed to create the <see cref="DiscordVoiceRegion"/>.</param>
    public DiscordVoiceRegion(DiscordVoiceRegionData data)
    {
        Id = data.Id;
        Name = data.Name;
        Optimal = data.Optimal;
        Deprecated = data.Deprecated;
        Custom = data.Custom;
    }
    
    /// <inheritdoc />
    public string Id { get; set; }

    /// <inheritdoc />
    public string Name { get; set; }

    /// <inheritdoc />
    public bool Optimal { get; set; }

    /// <inheritdoc />
    public bool Deprecated { get; set; }

    /// <inheritdoc />
    public bool Custom { get; set; }
}