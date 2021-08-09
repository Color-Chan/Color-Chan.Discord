using System;
using System.Collections.Generic;
using System.Linq;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;

namespace Color_Chan.Discord.Core.Extensions
{
    /// <summary>
    ///     Contains all the extensions methods for <see cref="DiscordGuildPermission" />.
    /// </summary>
    public static class DiscordGuildPermissionExtensions
    {
        /// <summary>
        ///     Get all the flags of <see cref="DiscordGuildPermission" /> separately in a list.
        /// </summary>
        /// <param name="permission">The permission flags.</param>
        /// <returns>
        ///     A list of <see cref="DiscordGuildPermission" /> flags.
        /// </returns>
        public static List<DiscordGuildPermission> ToList(this DiscordGuildPermission? permission)
        {
            if (permission is null)
            {
                throw new ArgumentNullException(nameof(permission));
            }

            return ToList(permission.Value);
        }

        /// <summary>
        ///     Get all the flags of <see cref="DiscordGuildPermission" /> separately in a list.
        /// </summary>
        /// <param name="permission">The permission flags.</param>
        /// <returns>
        ///     A list of <see cref="DiscordGuildPermission" /> flags.
        /// </returns>
        public static List<DiscordGuildPermission> ToList(this DiscordGuildPermission permission)
        {
            return Enum.GetValues(typeof(DiscordGuildPermission))
                       .Cast<Enum>()
                       .Where(permission.HasFlag)
                       .Cast<DiscordGuildPermission>()
                       .ToList();
        }
        
        /// <summary>
        ///     Get a readable string for a single <paramref name="permission"/>.
        /// </summary>
        /// <param name="permission">The permission that will be turned into a string.</param>
        /// <returns>
        ///     A readable string for a single <paramref name="permission"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when there was no string for the provided <paramref name="permission"/>.</exception>
        public static string ToReadableString(this DiscordGuildPermission permission)
        {
            return permission switch
            {
                DiscordGuildPermission.None => "None",
                DiscordGuildPermission.CreateInstantInvite => "Create instant invite",
                DiscordGuildPermission.KickMembers => "Kick members",
                DiscordGuildPermission.BanMembers => "Ban members",
                DiscordGuildPermission.Administrator => "Administrator",
                DiscordGuildPermission.ManageChannels => "Manage channels",
                DiscordGuildPermission.ManageGuild => "Manage server",
                DiscordGuildPermission.AddReactions => "Add reactions",
                DiscordGuildPermission.ViewAuditLog => "View audit log",
                DiscordGuildPermission.PrioritySpeaker => "Priority speaker",
                DiscordGuildPermission.Stream => "Stream",
                DiscordGuildPermission.ViewChannel => "View channel",
                DiscordGuildPermission.SendMessages => "Send messages",
                DiscordGuildPermission.SendTtsMessages => "Send Text-to-Speech messages",
                DiscordGuildPermission.ManageMessages => "Manage messages",
                DiscordGuildPermission.EmbedLinks => "Embed links",
                DiscordGuildPermission.AttachFiles => "Attach files",
                DiscordGuildPermission.ReadMessageHistory => "Read message history",
                DiscordGuildPermission.MentionEveryone => "Mention everyone",
                DiscordGuildPermission.UseExternalEmojis => "Use external emojis",
                DiscordGuildPermission.ViewGuildInsights => "View guild insights",
                DiscordGuildPermission.Connect => "Connect",
                DiscordGuildPermission.Speak => "Speak",
                DiscordGuildPermission.MuteMembers => "Mute members",
                DiscordGuildPermission.DeafenMembers => "Deafen members",
                DiscordGuildPermission.MoveMembers => "Move members",
                DiscordGuildPermission.UseVoiceActivity => "Use voice activity",
                DiscordGuildPermission.ChangeNickname => "Change nicknames",
                DiscordGuildPermission.ManageNicknames => "Manage nicknames",
                DiscordGuildPermission.ManageRoles => "Manage roles",
                DiscordGuildPermission.ManageWebhooks => "Manage webhooks",
                DiscordGuildPermission.ManageEmojisAndStickers => "Manage emojis and stickers",
                DiscordGuildPermission.UseSlashCommands => "Use slash commands",
                DiscordGuildPermission.RequestToSpeak => "Request to speak",
                DiscordGuildPermission.ManageThreads => "Manage threads",
                DiscordGuildPermission.UsePublicThreads => "Use public threads",
                DiscordGuildPermission.UsePrivateThreads => "Use private threads",
                _ => throw new ArgumentOutOfRangeException(nameof(permission), permission, null)
            };
        }
    }
}