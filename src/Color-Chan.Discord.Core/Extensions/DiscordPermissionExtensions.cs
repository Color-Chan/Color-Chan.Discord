using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Color_Chan.Discord.Core.Common.API.DataModels;

namespace Color_Chan.Discord.Core.Extensions;

/// <summary>
///     Contains all the extensions methods for <see cref="DiscordPermission" />.
/// </summary>
public static class DiscordPermissionExtensions
{
    private const DiscordPermission ChannelPerms = DiscordPermission.CreateInstantInvite | DiscordPermission.ManageChannels | DiscordPermission.AddReactions | DiscordPermission.PrioritySpeaker |
                                                   DiscordPermission.Stream | DiscordPermission.ViewChannel | DiscordPermission.SendMessages | DiscordPermission.SendTtsMessages |
                                                   DiscordPermission.ManageMessages | DiscordPermission.EmbedLinks | DiscordPermission.AttachFiles | DiscordPermission.ReadMessageHistory |
                                                   DiscordPermission.MentionEveryone | DiscordPermission.UseExternalEmojis | DiscordPermission.Connect | DiscordPermission.Speak |
                                                   DiscordPermission.MuteMembers | DiscordPermission.DeafenMembers | DiscordPermission.MoveMembers | DiscordPermission.UseVoiceActivity |
                                                   DiscordPermission.ManageWebhooks | DiscordPermission.UseApplicationCommands | DiscordPermission.RequestToSpeak |
                                                   DiscordPermission.ManageThreads | DiscordPermission.UsePublicThreads | DiscordPermission.UsePrivateThreads |
                                                   DiscordPermission.UseExternalStickers | DiscordPermission.SendMessagesInThreads | DiscordPermission.UseEmbeddedActivities;

    /// <summary>
    ///     Convert a permission <see cref="ReadOnlySpan{T}" /> of <see cref="char" /> into a
    ///     <see cref="DiscordPermission" />.
    /// </summary>
    /// <param name="permissions">
    ///     The <see cref="ReadOnlySpan{T}" /> of <see cref="char" /> that will be converter into a
    ///     <see cref="DiscordPermission" />.
    /// </param>
    /// <returns>
    ///     The converted <see cref="DiscordPermission" />.
    /// </returns>
    public static DiscordPermission ConvertToGuildPermission(this ReadOnlySpan<char> permissions)
    {
        var number = ulong.Parse(permissions);
        return (DiscordPermission)Enum.ToObject(typeof(DiscordPermission), number);
    }

    /// <summary>
    ///     Convert a permission <see cref="string" /> into a <see cref="DiscordPermission" />.
    /// </summary>
    /// <param name="permissions">
    ///     The <see cref="string" /> that will be converter into a <see cref="DiscordPermission" />.
    /// </param>
    /// <returns>
    ///     The converted <see cref="DiscordPermission" />.
    /// </returns>
    public static DiscordPermission ConvertToGuildPermission(this string permissions)
    {
        var number = ulong.Parse(permissions);
        return (DiscordPermission)Enum.ToObject(typeof(DiscordPermission), number);
    }

    /// <summary>
    ///     Try to parse a <see cref="string" /> into a <see cref="DiscordPermission" />.
    /// </summary>
    /// <param name="permissionString">
    ///     The <see cref="string" /> that will be converter into a
    ///     <see cref="DiscordPermission" />.
    /// </param>
    /// <param name="permission">The converted <see cref="DiscordPermission" />.</param>
    /// <returns>
    ///     Whether or not the <see cref="permissionString" /> has been converted to a <see cref="DiscordPermission" />.
    /// </returns>
    public static bool TryParseDiscordGuildPermission(this string? permissionString, [NotNullWhen(true)] out DiscordPermission? permission)
    {
        permission = default;

        if (ulong.TryParse(permissionString, out var permissionTemp))
        {
            permission = (DiscordPermission)Enum.ToObject(typeof(DiscordPermission), permissionTemp);
            return true;
        }

        return false;
    }

    /// <summary>
    ///     Try to parse a <see cref="ReadOnlySpan{T}" /> of <see cref="char" /> into a <see cref="DiscordPermission" />.
    /// </summary>
    /// <param name="permissionSpan">
    ///     The <see cref="ReadOnlySpan{T}" /> of <see cref="char" /> that will be converter into a
    ///     <see cref="DiscordPermission" />.
    /// </param>
    /// <param name="permission">The converted <see cref="DiscordPermission" />.</param>
    /// <returns>
    ///     Whether or not the <see cref="permissionSpan" /> has been converted to a <see cref="DiscordPermission" />.
    /// </returns>
    public static bool TryParseDiscordGuildPermission(this ReadOnlySpan<char> permissionSpan, [NotNullWhen(true)] out DiscordPermission? permission)
    {
        permission = default;

        if (ulong.TryParse(permissionSpan, out var permissionTemp))
        {
            permission = (DiscordPermission)Enum.ToObject(typeof(DiscordPermission), permissionTemp);
            return true;
        }

        return false;
    }

    /// <summary>
    ///     Convert a permission <see cref="DiscordPermission" /> into a <see cref="string" />.
    /// </summary>
    /// <param name="permissions">
    ///     The <see cref="DiscordPermission" /> that will be converter into a <see cref="string" />
    ///     .
    /// </param>
    /// <returns>
    ///     The converted <see cref="string" />.
    /// </returns>
    public static string ConvertToString(this DiscordPermission permissions)
    {
        return ((ulong)permissions).ToString();
    }

    /// <summary>
    ///     Get all the flags of <see cref="DiscordPermission" /> separately in a list.
    /// </summary>
    /// <param name="permission">The permission flags.</param>
    /// <returns>
    ///     A list of <see cref="DiscordPermission" /> flags.
    /// </returns>
    public static List<DiscordPermission> ToList(this DiscordPermission? permission)
    {
        if (permission is null)
        {
            throw new ArgumentNullException(nameof(permission));
        }

        return ToList(permission.Value);
    }

    /// <summary>
    ///     Get all the flags of <see cref="DiscordPermission" /> separately in a list.
    /// </summary>
    /// <param name="permission">The permission flags.</param>
    /// <returns>
    ///     A list of <see cref="DiscordPermission" /> flags.
    /// </returns>
    public static List<DiscordPermission> ToList(this DiscordPermission permission)
    {
        var permissions = Enum.GetValues(typeof(DiscordPermission))
                              .Cast<Enum>()
                              .Where(permission.HasFlag)
                              .Cast<DiscordPermission>()
                              .ToList();

        permissions.Remove(DiscordPermission.None);
        return permissions;
    }

    /// <summary>
    ///     Get a readable string for a single <paramref name="permission" />.
    /// </summary>
    /// <param name="permission">The permission that will be turned into a string.</param>
    /// <returns>
    ///     A readable string for a single <paramref name="permission" />.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Thrown when there was no string for the provided
    ///     <paramref name="permission" />.
    /// </exception>
    public static string ToReadableString(this DiscordPermission permission)
    {
        return permission switch
        {
            DiscordPermission.None => "None",
            DiscordPermission.CreateInstantInvite => "Create instant invite",
            DiscordPermission.KickMembers => "Kick members",
            DiscordPermission.BanMembers => "Ban members",
            DiscordPermission.Administrator => "Administrator",
            DiscordPermission.ManageChannels => "Manage channels",
            DiscordPermission.ManageGuild => "Manage server",
            DiscordPermission.AddReactions => "Add reactions",
            DiscordPermission.ViewAuditLog => "View audit log",
            DiscordPermission.PrioritySpeaker => "Priority speaker",
            DiscordPermission.Stream => "Stream",
            DiscordPermission.ViewChannel => "View channel",
            DiscordPermission.SendMessages => "Send messages",
            DiscordPermission.SendTtsMessages => "Send Text-to-Speech messages",
            DiscordPermission.ManageMessages => "Manage messages",
            DiscordPermission.EmbedLinks => "Embed links",
            DiscordPermission.AttachFiles => "Attach files",
            DiscordPermission.ReadMessageHistory => "Read message history",
            DiscordPermission.MentionEveryone => "Mention everyone",
            DiscordPermission.UseExternalEmojis => "Use external emojis",
            DiscordPermission.ViewGuildInsights => "View guild insights",
            DiscordPermission.Connect => "Connect",
            DiscordPermission.Speak => "Speak",
            DiscordPermission.MuteMembers => "Mute members",
            DiscordPermission.DeafenMembers => "Deafen members",
            DiscordPermission.MoveMembers => "Move members",
            DiscordPermission.UseVoiceActivity => "Use voice activity",
            DiscordPermission.ChangeNickname => "Change nicknames",
            DiscordPermission.ManageNicknames => "Manage nicknames",
            DiscordPermission.ManageRoles => "Manage roles",
            DiscordPermission.ManageWebhooks => "Manage webhooks",
            DiscordPermission.ManageEmojisAndStickers => "Manage emojis and stickers",
            DiscordPermission.UseApplicationCommands => "Use application commands",
            DiscordPermission.RequestToSpeak => "Request to speak",
            DiscordPermission.ManageThreads => "Manage threads",
            DiscordPermission.UsePublicThreads => "Use public threads",
            DiscordPermission.UsePrivateThreads => "Use private threads",
            DiscordPermission.UseExternalStickers => "Use external stickers",
            _ => throw new ArgumentOutOfRangeException(nameof(permission), permission, null)
        };
    }

    /// <summary>
    ///     Checks if the provided <paramref name="permissions" /> contains any channel permissions.
    /// </summary>
    /// <param name="permissions">The <see cref="DiscordPermission" /> that will be checked for channel permissions.</param>
    /// <returns>
    ///     Whether or not the <paramref name="permissions" /> contained any channel permissions.
    /// </returns>
    public static bool HasChannelPermissions(this DiscordPermission permissions)
    {
        return (permissions & ChannelPerms) != 0;
    }
}