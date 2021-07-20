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
        public SlashCommandChoiceAttribute(string name, string value)
        {
            Name = name;
            Value = value;
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