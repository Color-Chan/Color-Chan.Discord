using System;
using System.Text.RegularExpressions;

namespace Color_Chan.Discord.Commands.Attributes
{
    /// <summary>
    ///     Makes a class or a method available as a sub command group.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class SlashCommandGroupAttribute : Attribute
    {
        private static readonly Regex CommandGroupNameRegex = new(@"^[\w-]{1,32}$", RegexOptions.Compiled, TimeSpan.FromMilliseconds(250));

        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandGroupAttribute" />.
        /// </summary>
        /// <param name="name">The name of the command group.</param>
        /// <param name="description">The description of what the command group does.</param>
        /// <param name="defaultPermission">
        ///     Whether the command is enabled by default when the app is added to a guild. Default:
        ///     true.
        /// </param>
        /// <exception cref="ArgumentException">
        ///     Thrown when <paramref name="name" /> or <paramref name="description" /> doesn't
        ///     match the command group name requirements.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="name" /> or <paramref name="description" /> is
        ///     null.
        /// </exception>
        public SlashCommandGroupAttribute(string name, string description, bool defaultPermission = true)
        {
            if (name.Length is < 1 or > 32) throw new ArgumentException("Command group names must be between 1 and 32 characters.");

            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (description.Length is < 1 or > 100) throw new ArgumentException("Command group descriptions must be between 1 and 100 characters.");

            if (!CommandGroupNameRegex.IsMatch(name))
                throw new ArgumentException("Command group names can not contain special characters and whitespaces");

            Name = name ?? throw new ArgumentNullException(nameof(name), "Command group name can not be null");
            Description = description ?? throw new ArgumentNullException(nameof(description), "Command group description can not be null");
            DefaultPermission = defaultPermission;
        }

        /// <summary>
        ///     The name of the command group.
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     The description of what the command group is.
        /// </summary>
        public string Description { get; }

        /// <summary>
        ///     Whether the command is enabled by default when the app is added to a guild.
        /// </summary>
        public bool DefaultPermission { get; }
    }
}