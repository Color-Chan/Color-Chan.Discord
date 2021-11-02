using System;
using System.Collections.Generic;
using System.Reflection;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.Exceptions;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;

namespace Color_Chan.Discord.Commands.Models.Info
{
    /// <inheritdoc />
    public class SlashCommandOptionInfo : ISlashCommandOptionInfo
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandOptionInfo" />.
        /// </summary>
        /// <param name="name">The name of the option.</param>
        /// <param name="description">The description of the option.</param>
        /// <param name="type">The type of the parameter.</param>
        /// <param name="isRequired">Whether or not the option is required.</param>
        /// <param name="choices"></param>
        /// <param name="optionType">
        ///     The type of the option. Will be automatically set to the correct type when set to null, using
        ///     <paramref name="type" />.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="name" />, <paramref name="description" /> or
        ///     <paramref name="type" /> is null.
        /// </exception>
        public SlashCommandOptionInfo(string name, string description, Type type, bool? isRequired, IEnumerable<KeyValuePair<string, object>>? choices = null,
                                      DiscordApplicationCommandOptionType? optionType = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            IsRequired = isRequired;
            Choices = choices;

            if (optionType.HasValue)
            {
                Type = optionType.Value;
                return;
            }

            Type = type switch
            {
                var t when t == typeof(bool) || t == typeof(bool?) => DiscordApplicationCommandOptionType.Boolean,
                var t when t == typeof(int) || t == typeof(int?) => DiscordApplicationCommandOptionType.Integer,
                var t when t == typeof(double) || t == typeof(double?) => DiscordApplicationCommandOptionType.Number,
                var t when t == typeof(string) => DiscordApplicationCommandOptionType.String,
                _ => throw new UnsupportedSlashCommandParameterException($"Failed to find type for {type.Name}, please specify the option type")
            };
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandOptionInfo" />.
        /// </summary>
        /// <param name="name">The name of the sub command.</param>
        /// <param name="description">The description of the sub command.</param>
        /// <param name="type">The type of the option.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="name" />, <paramref name="description" /> is null.
        /// </exception>
        public SlashCommandOptionInfo(string name, string description, DiscordApplicationCommandOptionType type)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Type = type;
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandOptionInfo" />.
        /// </summary>
        /// <param name="name">The name of the sub command.</param>
        /// <param name="description">The description of the sub command.</param>
        /// <param name="acknowledge">
        ///     Whether or not the command should be automatically acknowledge to prevent the token
        ///     from turning inactive after 3 seconds.
        /// </param>
        /// <param name="command">The <see cref="MethodInfo" /> containing the method of the command.</param>
        /// <param name="module">The command module containing the <see cref="CommandMethod" />.</param>
        /// <param name="requirements">
        ///     A <see cref="IEnumerable{T}" /> of <see cref="InteractionRequirementAttribute" />s
        ///     containing all the requirements to execute the command.
        /// </param>
        /// <param name="commandOptions">
        ///     The <see cref="IEnumerable{T}" /> of <see cref="ISlashCommandOptionInfo" /> containing the
        ///     options for the slash command.
        /// </param>
        /// <param name="guilds">
        ///     The <see cref="IEnumerable{T}" /> of <see cref="SlashCommandGuildAttribute" /> containing the IDs
        ///     of the guilds that will get access to this slash command.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="name" />, <paramref name="description" /> is null.
        /// </exception>
        public SlashCommandOptionInfo(string name, string description, bool acknowledge, MethodInfo command, TypeInfo module, IEnumerable<InteractionRequirementAttribute>? requirements = null,
                                      List<ISlashCommandOptionInfo>? commandOptions = null, IEnumerable<SlashCommandGuildAttribute>? guilds = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Type = DiscordApplicationCommandOptionType.SubCommand;
            CommandMethod = command;
            ParentModule = module;
            Requirements = requirements;
            CommandOptions = commandOptions;
            Guilds = guilds;
            Acknowledge = acknowledge;
        }

        /// <inheritdoc />
        public string Name { get; init; }

        /// <inheritdoc />
        public string Description { get; init; }

        /// <inheritdoc />
        public DiscordApplicationCommandOptionType Type { get; init; }

        /// <inheritdoc />
        public bool? IsRequired { get; init; }

        /// <inheritdoc />
        public IEnumerable<KeyValuePair<string, object>>? Choices { get; init; }

        /// <inheritdoc />
        public IEnumerable<DiscordChannelType>? ChanelTypes { get; init; }

        /// <inheritdoc />
        public int? MinValue { get; init; }

        /// <inheritdoc />
        public int? MaxValue { get; init; }

        /// <inheritdoc />
        public bool? Autocomplete { get; init; }

        /// <inheritdoc />
        public MethodInfo? CommandMethod { get; set; }

        /// <inheritdoc />
        public TypeInfo? ParentModule { get; set; }

        /// <inheritdoc />
        public bool Acknowledge { get; }

        /// <inheritdoc />
        public IEnumerable<SlashCommandGuildAttribute>? Guilds { get; set; }

        /// <inheritdoc />
        public IEnumerable<InteractionRequirementAttribute>? Requirements { get; set; }

        /// <inheritdoc />
        public List<ISlashCommandOptionInfo>? CommandOptions { get; set; }
    }
}