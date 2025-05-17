namespace Color_Chan.Discord.Core.Common.API.DataModels;

/// <summary>
///     Represents a discord component types API model.
///     Docs: https://discord.com/developers/docs/components/reference#component-object-component-types
/// </summary>
public enum DiscordComponentType
{
    /// <summary>
    ///     Container to display a row of interactive components.
    /// </summary>
    ActionRow = 1,

    /// <summary>
    ///     Button object.
    /// </summary>
    Button = 2,

    /// <summary>
    ///     Select menu for picking from defined text options.
    /// </summary>
    StringSelect = 3,

    /// <summary>
    ///     Text input object.
    /// </summary>
    TextInput = 4,

    /// <summary>
    ///     Select menu for users.
    /// </summary>
    UserSelect = 5,

    /// <summary>
    ///     Select menu for roles.
    /// </summary>
    RoleSelect = 6,

    /// <summary>
    ///     Select menu for mentionable users and roles.
    /// </summary>
    MentionableSelect = 7,

    /// <summary>
    ///     Select menu for channels.
    /// </summary>
    ChannelSelect = 8,

    /// <summary>
    ///     Container to display text alongside an accessory component.
    /// </summary>
    Section = 9,

    /// <summary>
    ///     Markdown text.
    /// </summary>
    TextDisplay = 10,

    /// <summary>
    ///     Small image that can be used as an accessory.
    /// </summary>
    Thumbnail = 11,

    /// <summary>
    ///     Display images and other media.
    /// </summary>
    MediaGallery = 12,

    /// <summary>
    ///     Displays an attached file.
    /// </summary>
    File = 13,

    /// <summary>
    ///     Component to add vertical padding between other components.
    /// </summary>
    Separator = 14,

    /// <summary>
    ///     Container that visually groups a set of components.
    /// </summary>
    Content = 15
}