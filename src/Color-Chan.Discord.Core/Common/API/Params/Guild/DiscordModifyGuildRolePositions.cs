using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.Params.Guild;

public class DiscordModifyGuildRolePositions
{
    /// <summary>
    ///     The role id.
    /// </summary>
    [JsonPropertyName("id")]
    public ulong Id { get; set; }

    /// <summary>
    ///     Sorting position of the role.
    /// </summary>
    [JsonPropertyName("position")]
    public int? Position { get; set; }
}