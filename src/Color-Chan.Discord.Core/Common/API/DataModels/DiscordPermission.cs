using System;

namespace Color_Chan.Discord.Core.Common.API.DataModels
{
	/// <summary>
	///     Represents a discord Bitwise Permission Flags API model.
	///     https://discord.com/developers/docs/topics/permissions#permissions-bitwise-permission-flags
	/// </summary>
	[Flags]
    public enum DiscordPermission : ulong
    {
	    /// <summary>
	    ///     A <see cref="DiscordPermission" /> with no permissions.
	    /// </summary>
	    None = 0,

	    /// <summary>
	    ///     Allows creation of instant invites.
	    /// </summary>
	    /// <remarks>
	    ///     Channels types: Text, Voice, Stage.
	    /// </remarks>
	    CreateInstantInvite = 1 << 0,

	    /// <summary>
	    ///     Allows kicking members.
	    /// </summary>
	    /// <remarks>
	    ///     Requires the owner of the account to use two-factor authentication when used on a guild that has server-wide 2FA
	    ///     enabled.
	    /// </remarks>
	    KickMembers = 1 << 1,

	    /// <summary>
	    ///     Allows banning members.
	    /// </summary>
	    /// <remarks>
	    ///     Requires the owner of the account to use two-factor authentication when used on a guild that has server-wide 2FA
	    ///     enabled.
	    /// </remarks>
	    BanMembers = 1 << 2,

	    /// <summary>
	    ///     Allows all permissions and bypasses channel permission overwrites.
	    /// </summary>
	    /// <remarks>
	    ///     Requires the owner of the account to use two-factor authentication when used on a guild that has server-wide 2FA
	    ///     enabled.
	    /// </remarks>
	    Administrator = 1 << 3,

	    /// <summary>
	    ///     Allows management and editing of channels.
	    /// </summary>
	    /// <remarks>
	    ///     Channels types: Text, Voice, Stage.
	    /// </remarks>
	    /// <remarks>
	    ///     Requires the owner of the account to use two-factor authentication when used on a guild that has server-wide 2FA
	    ///     enabled.
	    /// </remarks>
	    ManageChannels = 1 << 4,

	    /// <summary>
	    ///     Allows management and editing of the guild.
	    /// </summary>
	    /// <remarks>
	    ///     Requires the owner of the account to use two-factor authentication when used on a guild that has server-wide 2FA
	    ///     enabled.
	    /// </remarks>
	    ManageGuild = 1 << 5,

	    /// <summary>
	    ///     Allows for the addition of reactions to messages.
	    /// </summary>
	    /// <remarks>
	    ///     Channels types: Text.
	    /// </remarks>
	    AddReactions = 1 << 6,

	    /// <summary>
	    ///     Allows for viewing of audit logs.
	    /// </summary>
	    ViewAuditLog = 1 << 7,

	    /// <summary>
	    ///     Allows for using priority speaker in a voice channel.
	    /// </summary>
	    /// <remarks>
	    ///     Channels types: Voice.
	    /// </remarks>
	    PrioritySpeaker = 1 << 8,

	    /// <summary>
	    ///     Allows the user to go live.
	    /// </summary>
	    /// <remarks>
	    ///     Channels types: Voice.
	    /// </remarks>
	    Stream = 1 << 9,

	    /// <summary>
	    ///     Allows guild members to view a channel, which includes reading messages in text channels.
	    /// </summary>
	    /// <remarks>
	    ///     Channels types: Text, Voice, Stage.
	    /// </remarks>
	    ViewChannel = 1 << 10,

	    /// <summary>
	    ///     Allows for sending messages in a channel.
	    /// </summary>
	    /// <remarks>
	    ///     Channels types: Text.
	    /// </remarks>
	    SendMessages = 1 << 11,

	    /// <summary>
	    ///     Allows for sending of /tts messages.
	    /// </summary>
	    /// <remarks>
	    ///     Channels types: Text.
	    /// </remarks>
	    SendTtsMessages = 1 << 12,

	    /// <summary>
	    ///     Allows for deletion of other users messages.
	    /// </summary>
	    /// <remarks>
	    ///     Channels types: Text.
	    /// </remarks>
	    /// <remarks>
	    ///     Requires the owner of the account to use two-factor authentication when used on a guild that has server-wide 2FA
	    ///     enabled.
	    /// </remarks>
	    ManageMessages = 1 << 13,

	    /// <summary>
	    ///     Links sent by users with this permission will be auto-embedded.
	    /// </summary>
	    /// <remarks>
	    ///     Channels types: Text.
	    /// </remarks>
	    EmbedLinks = 1 << 14,

	    /// <summary>
	    ///     Allows for uploading images and files.
	    /// </summary>
	    /// <remarks>
	    ///     Channels types: Text.
	    /// </remarks>
	    AttachFiles = 1 << 15,

	    /// <summary>
	    ///     Allows for reading of message history.
	    /// </summary>
	    /// <remarks>
	    ///     Channels types: Text.
	    /// </remarks>
	    ReadMessageHistory = 1 << 16,

	    /// <summary>
	    ///     Allows for using the @everyone tag to notify all users in a channel, and the @here tag to notify all online users
	    ///     in a channel.
	    /// </summary>
	    /// <remarks>
	    ///     Channels types: Text.
	    /// </remarks>
	    MentionEveryone = 1 << 17,

	    /// <summary>
	    ///     Allows the usage of custom emojis from other servers.
	    /// </summary>
	    /// <remarks>
	    ///     Channels types: Text.
	    /// </remarks>
	    UseExternalEmojis = 1 << 18,

	    /// <summary>
	    ///     Allows for viewing guild insights.
	    /// </summary>
	    ViewGuildInsights = 1 << 19,

	    /// <summary>
	    ///     Allows for joining of a voice channel.
	    /// </summary>
	    /// <remarks>
	    ///     Channels types: Text, Voice.
	    /// </remarks>
	    Connect = 1 << 20,

	    /// <summary>
	    ///     Allows for speaking in a voice channel.
	    /// </summary>
	    /// <remarks>
	    ///     Channels types: Voice.
	    /// </remarks>
	    Speak = 1 << 21,

	    /// <summary>
	    ///     Allows for muting members in a voice channel.
	    /// </summary>
	    /// <remarks>
	    ///     Channels types: Voice, Stage.
	    /// </remarks>
	    MuteMembers = 1 << 22,

	    /// <summary>
	    ///     Allows for deafening of members in a voice channel.
	    /// </summary>
	    /// <remarks>
	    ///     Channels types: Voice, Stage.
	    /// </remarks>
	    DeafenMembers = 1 << 23,

	    /// <summary>
	    ///     Allows for moving of members between voice channels.
	    /// </summary>
	    /// <remarks>
	    ///     Channels types: Voice, Stage.
	    /// </remarks>
	    MoveMembers = 1 << 24,

	    /// <summary>
	    ///     Allows for using voice-activity-detection in a voice channel.
	    /// </summary>
	    /// <remarks>
	    ///     Channels types: Voice.
	    /// </remarks>
	    UseVoiceActivity = 1 << 25,

	    /// <summary>
	    ///     Allows for modification of own nickname.
	    /// </summary>
	    ChangeNickname = 1 << 26,

	    /// <summary>
	    ///     Allows for modification of other users nicknames.
	    /// </summary>
	    ManageNicknames = 1 << 27,

	    /// <summary>
	    ///     Allows management and editing of roles.
	    /// </summary>
	    /// <remarks>
	    ///     Channels types: Text, Voice, Stage.
	    /// </remarks>
	    /// <remarks>
	    ///     Requires the owner of the account to use two-factor authentication when used on a guild that has server-wide 2FA
	    ///     enabled.
	    /// </remarks>
	    ManageRoles = 1 << 28,

	    /// <summary>
	    ///     Allows management and editing of webhooks.
	    /// </summary>
	    /// <remarks>
	    ///     Channels types: Text.
	    /// </remarks>
	    /// <remarks>
	    ///     Requires the owner of the account to use two-factor authentication when used on a guild that has server-wide 2FA
	    ///     enabled.
	    /// </remarks>
	    ManageWebhooks = 1 << 29,

	    /// <summary>
	    ///     Allows management and editing of emojis.
	    /// </summary>
	    /// <remarks>
	    ///     Requires the owner of the account to use two-factor authentication when used on a guild that has server-wide 2FA
	    ///     enabled.
	    /// </remarks>
	    ManageEmojisAndStickers = 1 << 30,

	    /// <summary>
	    ///     Allows members to use slash commands in text channels.
	    /// </summary>
	    /// <remarks>
	    ///     Channels types: Text.
	    /// </remarks>
	    UseApplicationCommands = (ulong)1 << 31,

	    /// <summary>
	    ///     Allows for requesting to speak in stage channels. (This permission is under active development and may be changed
	    ///     or removed.)
	    /// </summary>
	    /// <remarks>
	    ///     Channels types: Stage.
	    /// </remarks>
	    RequestToSpeak = (ulong)1 << 32,

	    /// <summary>
	    ///     Allows for deleting and archiving threads, and viewing all private threads.
	    /// </summary>
	    /// <remarks>
	    ///     Channels types: Text.
	    /// </remarks>
	    /// <remarks>
	    ///     Requires the owner of the account to use two-factor authentication when used on a guild that has server-wide 2FA
	    ///     enabled.
	    /// </remarks>
	    ManageThreads = (ulong)1 << 34,

	    /// <summary>
	    ///     Allows for creating and participating in threads.
	    /// </summary>
	    /// <remarks>
	    ///     Channels types: Text.
	    /// </remarks>
	    UsePublicThreads = (ulong)1 << 35,

	    /// <summary>
	    ///     Allows for creating and participating in private threads.
	    /// </summary>
	    /// <remarks>
	    ///     Channels types: Text.
	    /// </remarks>
	    UsePrivateThreads = (ulong)1 << 36,

	    /// <summary>
	    ///     Allows the usage of custom stickers from other servers.
	    /// </summary>
	    /// <remarks>
	    ///     Channels types: Text.
	    /// </remarks>
	    UseExternalStickers = (ulong)1 << 37
    }
}