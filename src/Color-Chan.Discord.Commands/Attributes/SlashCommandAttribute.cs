using System;
using System.Text.RegularExpressions;
using Color_Chan.Discord.Commands.Models.Contexts;
using Color_Chan.Discord.Core.Common.Models.Interaction;

namespace Color_Chan.Discord.Commands.Attributes
{
    /// <summary>
    ///     Makes a method available as a global slash command.
    /// </summary>
    /// <example>
    ///     The following example will add the slash command `/ping` to the application.
    ///     <code language="cs">
    ///     public class PongCommands : SlashCommandModule
    ///     {
    ///         [SlashCommand("ping", "Ping Pong!")]
    ///         public Task&lt;Result&lt;IDiscordInteractionResponse&gt;&gt; PongAsync()
    ///         {
    ///             // Command code...
    ///         }
    ///     }
    ///     </code>
    ///     The following example will add the slash command `/rolename` to the application.
    ///     With a required string input.
    ///     <code language="cs">
    ///     public class RoleCommands : SlashCommandModule
    ///     {
    ///         [SlashCommand("rolename", "A role command")]
    ///         public Task&lt;Result&lt;IDiscordInteractionResponse&gt;&gt; RoleNameAsync
    ///         (
    ///             [SlashCommandOption("name", "The name of the new role.", true)]
    ///             string roleName
    ///         )
    ///         {
    ///             // Command code...
    ///         }
    ///     }
    ///     </code>
    ///     The following example will add the slash command `/role` to the application.
    ///     With a required role input. The given role will be added to the <see cref="ISlashCommandContext.CommandRequest" />,
    ///     <see cref="IDiscordInteractionCommand.Resolved" />, <see cref="IDiscordInteractionCommandResolved.Roles" /> list.
    ///     <code language="cs">
    ///     public class RoleCommands : SlashCommandModule
    ///     {
    ///         [SlashCommand("role", "A role command")]
    ///         public Task&lt;Result&lt;IDiscordInteractionResponse&gt;&gt; RoleAsync
    ///         (
    ///             [SlashCommandOption("role", "A role.", true, DiscordApplicationCommandOptionType.Role)]
    ///             ulong roleId
    ///         )
    ///         {
    ///             // Command code...
    ///         }
    ///     }
    ///     </code>
    ///     Go to the samples solution folder to see more examples.
    /// </example>
    [AttributeUsage(AttributeTargets.Method)]
    public class SlashCommandAttribute : Attribute
    {
        private static readonly Regex CommandNameRegex = new(@"^[\w-]{1,32}$", RegexOptions.Compiled, TimeSpan.FromMilliseconds(250));

        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandAttribute" />.
        /// </summary>
        /// <param name="name">The name of the command.</param>
        /// <param name="description">The description of what the command does.</param>
        /// <param name="defaultPermission">
        ///     Whether the command is enabled by default when the app is added to a guild. Default:
        ///     true.
        /// </param>
        /// <exception cref="ArgumentException">
        ///     Thrown when <paramref name="name" /> or <paramref name="description" /> doesn't
        ///     match the command name requirements.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="name" /> or <paramref name="description" /> is
        ///     null.
        /// </exception>
        public SlashCommandAttribute(string name, string description, bool defaultPermission = true)
        {
            if (name.Length is < 1 or > 32) throw new ArgumentException("Command names must be between 1 and 32 characters.");

            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (description.Length is < 1 or > 100) throw new ArgumentException("Command descriptions must be between 1 and 100 characters.");

            if (!CommandNameRegex.IsMatch(name))
                throw new ArgumentException("Command names can not contain special characters and whitespaces");

            Name = name ?? throw new ArgumentNullException(nameof(name), "Command name can not be null");
            Description = description ?? throw new ArgumentNullException(nameof(description), "Command description can not be null");
            DefaultPermission = defaultPermission;
        }

        /// <summary>
        ///     The name of the command.
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     The description of what the command does.
        /// </summary>
        public string Description { get; }

        /// <summary>
        ///     Whether the command is enabled by default when the app is added to a guild.
        /// </summary>
        public bool DefaultPermission { get; }
    }
}