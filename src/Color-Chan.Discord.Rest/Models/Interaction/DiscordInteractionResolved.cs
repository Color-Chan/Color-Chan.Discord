using System.Collections.Generic;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Common.API.DataModels.Message;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Core.Common.Models.Guild;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Common.Models.Message;
using Color_Chan.Discord.Rest.Models.Guild;
using Color_Chan.Discord.Rest.Models.Message;

namespace Color_Chan.Discord.Rest.Models.Interaction;

public record DiscordInteractionResolved : IDiscordInteractionResolved
{
    public DiscordInteractionResolved(DiscordInteractionResolvedData data)
    {
        Users = InitializeUsersDict(data.Users);
        Channels = InitializeChannelsDict(data.Channels);
        Roles = InitializeRolesDict(data.Roles);
        Members = InitializeMembersDict(data.Members);
        Messages = InitializeMessagesDict(data.Messages);
    }

    /// <inheritdoc />
    public IReadOnlyDictionary<ulong, IDiscordUser>? Users { get; init; }

    /// <inheritdoc />
    public IReadOnlyDictionary<ulong, IDiscordGuildMember>? Members { get; init; }

    /// <inheritdoc />
    public IReadOnlyDictionary<ulong, IDiscordGuildRole>? Roles { get; init; }

    /// <inheritdoc />
    public IReadOnlyDictionary<ulong, IDiscordChannel>? Channels { get; init; }

    /// <inheritdoc />
    public IReadOnlyDictionary<ulong, IDiscordMessage>? Messages { get; init; }

    private static IReadOnlyDictionary<ulong, IDiscordMessage> InitializeMessagesDict(IReadOnlyDictionary<ulong, DiscordMessageData>? data)
    {
        if (data is not null)
        {
            var members = new Dictionary<ulong, IDiscordMessage>();
            foreach (var (key, value) in data) members.Add(key, new DiscordMessage(value));

            return members;
        }

        return new Dictionary<ulong, IDiscordMessage>();
    }

    private static Dictionary<ulong, IDiscordGuildMember> InitializeMembersDict(IReadOnlyDictionary<ulong, DiscordGuildMemberData>? data)
    {
        if (data is not null)
        {
            var members = new Dictionary<ulong, IDiscordGuildMember>();
            foreach (var (key, value) in data) members.Add(key, new DiscordGuildMember(value));

            return members;
        }

        return new Dictionary<ulong, IDiscordGuildMember>();
    }

    private static Dictionary<ulong, IDiscordUser> InitializeUsersDict(IReadOnlyDictionary<ulong, DiscordUserData>? data)
    {
        if (data is not null)
        {
            var users = new Dictionary<ulong, IDiscordUser>();
            foreach (var (key, value) in data) users.Add(key, new DiscordUser(value));

            return users;
        }

        return new Dictionary<ulong, IDiscordUser>();
    }

    private static Dictionary<ulong, IDiscordGuildRole> InitializeRolesDict(IReadOnlyDictionary<ulong, DiscordGuildRoleData>? data)
    {
        if (data is not null)
        {
            var roles = new Dictionary<ulong, IDiscordGuildRole>();
            foreach (var (key, value) in data) roles.Add(key, new DiscordGuildRole(value));

            return roles;
        }

        return new Dictionary<ulong, IDiscordGuildRole>();
    }

    private static Dictionary<ulong, IDiscordChannel> InitializeChannelsDict(IReadOnlyDictionary<ulong, DiscordChannelData>? data)
    {
        if (data is not null)
        {
            var channels = new Dictionary<ulong, IDiscordChannel>();
            foreach (var (key, value) in data) channels.Add(key, new DiscordChannel(value));

            return channels;
        }

        return new Dictionary<ulong, IDiscordChannel>();
    }
}