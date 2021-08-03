using System;
using System.Text.RegularExpressions;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;

namespace Color_Chan.Discord.Commands.Attributes
{
    /// <summary>
    ///     Makes a parameter available as an slash command option.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class SlashCommandOptionAttribute : Attribute
    {
        private static readonly Regex CommandOptionNameRegex = new(@"^[\w-]{1,32}$", RegexOptions.Compiled, TimeSpan.FromMilliseconds(250));

        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandOptionAttribute" />.
        /// </summary>
        /// <param name="name">The name of the option.</param>
        /// <param name="description">The description of the option.</param>
        /// <param name="isRequired">Whether or not the option is required.</param>
        /// <param name="type">The type of the option. Will be automatically set to the correct type when set to null.</param>
        /// <exception cref="ArgumentException">
        ///     Thrown when <paramref name="name" /> or <paramref name="description" /> doesn't
        ///     match the command name requirements.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="name" /> or <paramref name="description" /> is
        ///     null.
        /// </exception>
        public SlashCommandOptionAttribute(string name, string description, bool isRequired, DiscordApplicationCommandOptionType type)
        {
            if (name.Length is < 1 or > 32) throw new ArgumentException("Command option names must be between 1 and 32 characters.");

            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (description.Length is < 1 or > 100) throw new ArgumentException("Command option descriptions must be between 1 and 100 characters.");

            if (!CommandOptionNameRegex.IsMatch(name))
                throw new ArgumentException("Command option names can not contain special characters and whitespaces");

            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            IsRequired = isRequired;
            Type = type;
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandOptionAttribute" />.
        /// </summary>
        /// <param name="name">The name of the option.</param>
        /// <param name="description">The description of the option.</param>
        /// <param name="type">The type of the option. Will be automatically set to the correct type when set to null.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="name" /> or <paramref name="description" /> is
        ///     null.
        /// </exception>
        public SlashCommandOptionAttribute(string name, string description, DiscordApplicationCommandOptionType type)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Type = type;
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandOptionAttribute" />.
        /// </summary>
        /// <param name="name">The name of the option.</param>
        /// <param name="description">The description of the option.</param>
        /// <param name="isRequired">Whether or not the option is required.</param>
        /// <exception cref="ArgumentException">
        ///     Thrown when <paramref name="name" /> or <paramref name="description" /> doesn't
        ///     match the command name requirements.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="name" /> or <paramref name="description" /> is
        ///     null.
        /// </exception>
        public SlashCommandOptionAttribute(string name, string description, bool isRequired)
        {
            if (name.Length is < 1 or > 32) throw new ArgumentException("Command option names must be between 1 and 32 characters.");

            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (description.Length is < 1 or > 100) throw new ArgumentException("Command option descriptions must be between 1 and 100 characters.");

            if (!CommandOptionNameRegex.IsMatch(name))
                throw new ArgumentException("Command option names can not contain special characters and whitespaces");

            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            IsRequired = isRequired;
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandOptionAttribute" />.
        /// </summary>
        /// <param name="name">The name of the option.</param>
        /// <param name="description">The description of the option.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="name" /> or <paramref name="description" /> is
        ///     null.
        /// </exception>
        public SlashCommandOptionAttribute(string name, string description)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }

        /// <summary>
        ///     The name of the option..
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        ///     The description of the option.
        /// </summary>
        public string Description { get; init; }

        /// <summary>
        ///     Whether or not the option is required.
        /// </summary>
        public bool? IsRequired { get; }

        /// <summary>
        ///     The type of the option.
        ///     Will be automatically set to the correct type when set to null.
        /// </summary>
        public DiscordApplicationCommandOptionType? Type { get; }
    }
}