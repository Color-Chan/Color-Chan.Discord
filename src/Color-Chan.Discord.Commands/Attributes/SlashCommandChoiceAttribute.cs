using System;

namespace Color_Chan.Discord.Commands.Attributes;

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
    /// <param name="value">The <see cref="string" /> value of the choice.</param>
    /// <exception cref="ArgumentException">
    ///     Thrown when <paramref name="name" /> or <paramref name="value" /> doesn't match the
    ///     command name requirements.
    /// </exception>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="name" /> or <paramref name="value" /> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Thrown when the <paramref name="name"> or </paramref>
    ///     <paramref name="value" /> is to short or to long.
    /// </exception>
    public SlashCommandChoiceAttribute(string name, string value)
    {
        if (name.Length is < 1 or > 32)
            throw new ArgumentOutOfRangeException(nameof(name.Length), "Command option choice names must be between 1 and 32 characters.");

        if (value.Length is < 1 or > 100)
            throw new ArgumentOutOfRangeException(nameof(value.Length), "Command option choice values must be between 1 and 100 characters.");

        Name = name ?? throw new ArgumentNullException(nameof(name), "Choice name can not be null");
        StringValue = value ?? throw new ArgumentNullException(nameof(value), "Choice value can not be null");
    }

    /// <summary>
    ///     Initializes a new instance of <see cref="SlashCommandChoiceAttribute" />.
    /// </summary>
    /// <param name="name">The name of the choice.</param>
    /// <param name="value">The <see cref="int" /> value of the choice.</param>
    /// <exception cref="ArgumentException">
    ///     Thrown when <paramref name="name" /> or <paramref name="value" /> doesn't match the
    ///     command name requirements.
    /// </exception>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="name" />.</exception>
    public SlashCommandChoiceAttribute(string name, int value)
    {
        if (name.Length is < 1 or > 32)
            throw new ArgumentException("Command option choice names must be between 1 and 32 characters.");

        Name = name ?? throw new ArgumentNullException(nameof(name), "Choice name can not be null");
        IntValue = value;
    }

    /// <summary>
    ///     Initializes a new instance of <see cref="SlashCommandChoiceAttribute" />.
    /// </summary>
    /// <param name="name">The name of the choice.</param>
    /// <param name="value">The <see cref="double" /> value of the choice.</param>
    /// <exception cref="ArgumentException">
    ///     Thrown when <paramref name="name" /> or <paramref name="value" /> doesn't match the
    ///     command name requirements.
    /// </exception>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="name" />.</exception>
    public SlashCommandChoiceAttribute(string name, double value)
    {
        if (name.Length is < 1 or > 32)
            throw new ArgumentException("Command option choice names must be between 1 and 32 characters.");

        Name = name ?? throw new ArgumentNullException(nameof(name), "Choice name can not be null");
        NumberValue = value;
    }

    /// <summary>
    ///     Initializes a new instance of <see cref="SlashCommandChoiceAttribute" />.
    /// </summary>
    /// <param name="name">The name of the choice.</param>
    /// <param name="value">The <see cref="bool" /> value of the choice.</param>
    /// <exception cref="ArgumentException">
    ///     Thrown when <paramref name="name" /> or <paramref name="value" /> doesn't match the
    ///     command name requirements.
    /// </exception>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="name" />.</exception>
    public SlashCommandChoiceAttribute(string name, bool value)
    {
        if (name.Length is < 1 or > 32)
            throw new ArgumentException("Command option choice names must be between 1 and 32 characters.");

        Name = name ?? throw new ArgumentNullException(nameof(name), "Choice name can not be null");
        BoolValue = value;
    }

    /// <summary>
    ///     The name of the choice.
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///     The <see cref="string" /> value of the choice.
    /// </summary>
    public string? StringValue { get; }

    /// <summary>
    ///     The <see cref="int" /> value of the choice.
    /// </summary>
    public int? IntValue { get; }

    /// <summary>
    ///     The <see cref="double" /> value of the choice.
    /// </summary>
    public double? NumberValue { get; }

    /// <summary>
    ///     The <see cref="bool" /> value of the choice.
    /// </summary>
    public bool? BoolValue { get; }

    /// <summary>
    ///     Get the <see cref="object" /> value of the set value.
    /// </summary>
    /// <returns>
    ///     The value as an <see cref="object" />.
    /// </returns>
    /// <exception cref="ArgumentException">Thrown when no choice value is set.</exception>
    public object ObjectValue()
    {
        if (StringValue is not null) return StringValue;
        if (IntValue is not null) return IntValue;
        if (NumberValue is not null) return NumberValue;
        if (BoolValue is not null) return BoolValue;
        throw new ArgumentException("No choice value provided");
    }
}