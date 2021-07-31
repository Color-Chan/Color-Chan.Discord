using System;
using System.Collections.Generic;
using Color_Chan.Discord.Commands.Exceptions;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;

namespace Color_Chan.Discord.Commands.Info
{
    /// <inheritdoc />
    public class SlashCommandOptionInfo : ISlashCommandOptionInfo
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandOptionInfo" />.
        /// </summary>
        /// <param name="name">The name of the option..</param>
        /// <param name="description">The description of the option.</param>
        /// <param name="type">The type of the parameter.</param>
        /// <param name="isRequired">Whether or not the option is required.</param>
        /// <param name="choices"></param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="name" />, <paramref name="description" /> or
        ///     <paramref name="type" /> is null.
        /// </exception>
        public SlashCommandOptionInfo(string name, string description, Type type, bool? isRequired, IEnumerable<KeyValuePair<string, string>>? choices = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            IsRequired = isRequired;
            Choices = choices;
            Type = type switch
            {
                var t when t == typeof(bool) => DiscordApplicationCommandOptionType.Boolean,
                var t when t == typeof(int) => DiscordApplicationCommandOptionType.Integer,
                var t when t == typeof(double) => DiscordApplicationCommandOptionType.Number,
                var t when t == typeof(string) => DiscordApplicationCommandOptionType.String,
                _ => throw new UnsupportedSlashCommandParameterException($"{type.Name} is currently not supported as a slash command options.")
            };
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
        public IEnumerable<KeyValuePair<string, string>>? Choices { get; init; }
    }
}