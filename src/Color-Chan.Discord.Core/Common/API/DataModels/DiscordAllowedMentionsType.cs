using System.Runtime.Serialization;

namespace Color_Chan.Discord.Core.Common.API.DataModels;

public enum DiscordAllowedMentionsType
{
    [EnumMember(Value = "roles")] RoleMentions,

    [EnumMember(Value = "users")] UserMentions,

    [EnumMember(Value = "everyone")] EveryoneMentions
}