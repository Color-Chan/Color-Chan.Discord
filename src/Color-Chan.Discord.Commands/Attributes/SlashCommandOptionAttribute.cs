using System;

namespace Color_Chan.Discord.Commands.Attributes
{
    /// <summary>
    ///     Makes a parameter available as an slash command option.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class SlashCommandOptionAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandOptionAttribute" />.
        /// </summary>
        /// <param name="name">The name of the option.</param>
        /// <param name="description">The description of the option.</param>
        /// <param name="isRequired">Whether or not the option is required.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="name" /> or <paramref name="description" /> is
        ///     null.
        /// </exception>
        public SlashCommandOptionAttribute(string name, string description, bool isRequired)
        {
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
    }
}