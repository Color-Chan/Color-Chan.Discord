﻿using System;
using System.Text.RegularExpressions;

namespace Color_Chan.Discord.Commands.Attributes
{
    /// <summary>
    ///     Makes a method available as an global slash command.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class SlashCommandAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandAttribute" />.
        /// </summary>
        /// <param name="name">The name of the command.</param>
        /// <param name="description">The description of what the command does.</param>
        /// <exception cref="ArgumentException">Thrown when the command doesn't match the command name requirements.</exception>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="name" /> or <paramref name="description" /> is
        ///     null.
        /// </exception>
        public SlashCommandAttribute(string name, string description)
        {
            if (name.Length is < 1 or > 32) throw new ArgumentException("Command names must be between 1 and 32 characters.");

            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (description.Length is < 1 or > 100) throw new ArgumentException("Command descriptions must be between 1 and 100 characters.");

            if (!Regex.IsMatch(name, @"^[\w-]{1,32}$"))
                throw new ArgumentException("Command names can not contain special characters and whitespaces");

            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }

        /// <summary>
        ///     The name of the command.
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     The description of what the command does.
        /// </summary>
        public string Description { get; }
    }
}