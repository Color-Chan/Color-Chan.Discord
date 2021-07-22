using System;

namespace Color_Chan.Discord.Commands.Attributes
{
    /// <summary>
    ///     Gives a choice value to a parameter on a slash command.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = true)]
    public class SlashCommandChoiceAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandChoiceAttribute" />.
        /// </summary>
        /// <param name="name">The name of the choice.</param>
        /// <param name="value">The raw value of the choice.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="name" /> or <paramref name="value" /> doesn't match the command name requirements.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="name" /> or <paramref name="value" /> is null.</exception>
        public SlashCommandChoiceAttribute(string name, string value)
        {
            if (name.Length is < 1 or > 32) 
                throw new ArgumentException("Command option choice names must be between 1 and 32 characters.");

            if (value.Length > 100) 
                throw new ArgumentException("Command option choice values must contain less then 100 characters.");
            
            Name = name ?? throw new ArgumentNullException(nameof(name), "Choice name can not be null");
            Value = value ?? throw new ArgumentNullException(nameof(value), "Choice value can not be null");
        }

        /// <summary>
        ///     The name of the choice.
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     The raw value of the choice.
        /// </summary>
        public string Value { get; }
    }
}