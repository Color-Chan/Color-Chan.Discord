using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Color_Chan.Discord.Core.Common.API.DataModels;
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
        /// <param name="chanelTypes">
        ///     The channel type that will be shown if the supplied option <paramref name="type" /> is set to
        ///     <see cref="DiscordApplicationCommandOptionType.Channel" />.
        /// </param>
        /// <exception cref="ArgumentException">
        ///     Thrown when <paramref name="name" /> or <paramref name="description" /> doesn't
        ///     match the command name requirements.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="name" /> or <paramref name="description" /> is
        ///     null.
        /// </exception>
        public SlashCommandOptionAttribute(string name, string description, bool isRequired, DiscordApplicationCommandOptionType type, IEnumerable<DiscordChannelType>? chanelTypes)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            IsRequired = isRequired;
            Type = type;
            ChanelTypes = chanelTypes;

            ValidateArguments();
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandOptionAttribute" />.
        /// </summary>
        /// <param name="name">The name of the option.</param>
        /// <param name="description">The description of the option.</param>
        /// <param name="type">The type of the option. Will be automatically set to the correct type when set to null.</param>
        /// <param name="chanelTypes">
        ///     The channel type that will be shown if the supplied option <paramref name="type" /> is set to
        ///     <see cref="DiscordApplicationCommandOptionType.Channel" />.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="name" /> or <paramref name="description" /> is
        ///     null.
        /// </exception>
        public SlashCommandOptionAttribute(string name, string description, DiscordApplicationCommandOptionType type, IEnumerable<DiscordChannelType>? chanelTypes)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Type = type;
            ChanelTypes = chanelTypes;

            ValidateArguments();
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandOptionAttribute" />.
        /// </summary>
        /// <param name="name">The name of the option.</param>
        /// <param name="description">The description of the option.</param>
        /// <param name="isRequired">Whether or not the option is required.</param>
        /// <param name="type">The type of the option. Will be automatically set to the correct type when set to null.</param>
        /// <param name="min">
        ///     The minimum value if the supplied option <paramref name="type" /> is set to
        ///     <see cref="DiscordApplicationCommandOptionType.Integer" /> or
        ///     <see cref="DiscordApplicationCommandOptionType.Number" />.
        /// </param>
        /// <param name="max">
        ///     The maximum value if the supplied option <paramref name="type" /> is set to
        ///     <see cref="DiscordApplicationCommandOptionType.Integer" /> or
        ///     <see cref="DiscordApplicationCommandOptionType.Number" />.
        /// </param>
        /// <exception cref="ArgumentException">
        ///     Thrown when <paramref name="name" /> or <paramref name="description" /> doesn't
        ///     match the command name requirements.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="name" /> or <paramref name="description" /> is
        ///     null.
        /// </exception>
        public SlashCommandOptionAttribute(string name, string description, bool isRequired, DiscordApplicationCommandOptionType type, int min, int max)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            IsRequired = isRequired;
            Type = type;
            MinValue = min;
            MaxValue = max;

            ValidateArguments();
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandOptionAttribute" />.
        /// </summary>
        /// <param name="name">The name of the option.</param>
        /// <param name="description">The description of the option.</param>
        /// <param name="type">The type of the option. Will be automatically set to the correct type when set to null.</param>
        /// <param name="min">
        ///     The minimum value if the supplied option <paramref name="type" /> is set to
        ///     <see cref="DiscordApplicationCommandOptionType.Integer" /> or
        ///     <see cref="DiscordApplicationCommandOptionType.Number" />.
        /// </param>
        /// <param name="max">
        ///     The maximum value if the supplied option <paramref name="type" /> is set to
        ///     <see cref="DiscordApplicationCommandOptionType.Integer" /> or
        ///     <see cref="DiscordApplicationCommandOptionType.Number" />.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="name" /> or <paramref name="description" /> is
        ///     null.
        /// </exception>
        public SlashCommandOptionAttribute(string name, string description, DiscordApplicationCommandOptionType type, int min, int max)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Type = type;
            MinValue = min;
            MaxValue = max;

            ValidateArguments();
        }

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
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            IsRequired = isRequired;
            Type = type;

            ValidateArguments();
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

            ValidateArguments();
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
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            IsRequired = isRequired;

            ValidateArguments();
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

            ValidateArguments();
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

        /// <summary>
        ///     If the option is a channel type, the channels shown will be restricted to these types.
        /// </summary>
        public IEnumerable<DiscordChannelType>? ChanelTypes { get; init; }

        /// <summary>
        ///     If the option is an INTEGER or NUMBER type, the minimum value permitted.
        /// </summary>
        public int? MinValue { get; init; }

        /// <summary>
        ///     If the option is an INTEGER or NUMBER type, the maximum value permitted.
        /// </summary>
        public int? MaxValue { get; init; }

        /// <summary>
        ///     Enable autocomplete interactions for this option.
        /// </summary>
        public bool? Autocomplete { get; init; }

        private void ValidateArguments()
        {
            if (Type != DiscordApplicationCommandOptionType.Channel && ChanelTypes is not null)
            {
                throw new ArgumentException("Channel types can only be used with channel options.");
            }

            if (Type is not DiscordApplicationCommandOptionType.Number or DiscordApplicationCommandOptionType.Integer && (MinValue is not null || MaxValue is not null))
            {
                throw new ArgumentException("Min and max values can only be used with numbers and integers.");
            }

            if (Name.Length is < 1 or > 32) throw new ArgumentException("Command option names must be between 1 and 32 characters.");

            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (Description.Length is < 1 or > 100) throw new ArgumentException("Command option descriptions must be between 1 and 100 characters.");

            if (!CommandOptionNameRegex.IsMatch(Name))
                throw new ArgumentException("Command option names can not contain special characters and whitespaces");
        }
    }
}