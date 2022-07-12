using System.Runtime.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels;

/// <summary>
///     Represents a discord Allowed Mention Types API model.
///     Docs: https://discord.com/developers/docs/resources/channel#allowed-mentions-object-allowed-mention-types
/// </summary>
public enum DiscordAllowedMentionsType
{
    /// <summary>
    ///     Controls role mentions.
    /// </summary>
    [EnumMember(Value = "roles")] RoleMentions,

    /// <summary>
    ///     Controls user mentions.
    /// </summary>
    [EnumMember(Value = "users")] UserMentions,

    /// <summary>
    ///     Controls @everyone and @here mentions
    /// </summary>
    [EnumMember(Value = "everyone")] EveryoneMentions
}