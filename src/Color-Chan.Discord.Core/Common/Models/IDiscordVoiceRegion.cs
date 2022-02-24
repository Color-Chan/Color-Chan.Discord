namespace Color_Chan.Discord.Core.Common.Models;

/// <summary>
///     Represents a discord Voice region API model.
///     Docs: https://discord.com/developers/docs/resources/voice#voice-region-object-voice-region-structure
/// </summary>
public interface IDiscordVoiceRegion
{
    /// <summary>
    ///     Unique ID for the region.
    /// </summary>
    public string Id { get; set; }
    
    /// <summary>
    ///     Name of the region.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    ///     True for a single server that is closest to the current user's client.
    /// </summary>
    public bool Optimal { get; set; }
    
    /// <summary>
    ///     Whether this is a deprecated voice region (avoid switching to these).
    /// </summary>
    public bool Deprecated { get; set; }
    
    /// <summary>
    ///     Whether this is a custom voice region (used for events/etc).
    /// </summary>
    public bool Custom { get; set; }
}