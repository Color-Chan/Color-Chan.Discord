using Color_Chan.Discord.Core.Common.API.DataModels.Guild;

namespace Color_Chan.Discord.Core.Common.Models.Guild;

/// <summary>
///     Represents a discord Guild Scheduled Event Entity Metadata API model.
///     Docs: https://discord.com/developers/docs/resources/guild-scheduled-event#guild-scheduled-event-object-guild-scheduled-event-entity-metadata
/// </summary>
public interface IDiscordGuildEventEntityMetadata
{
    /// <summary>
    ///     Location of the event (1-100 characters).
    /// </summary>
    /// <remarks>
    ///     Required for events with <see cref="DiscordGuildEventEntityType"/>.<see cref="DiscordGuildEventEntityType.External"/>.
    /// </remarks>
    public string? Location { get; set; }
}