using System.Text.Json;

namespace Color_Chan.Discord.Core.Exceptions;

/// <summary>
///     Thrown when a <see cref="ulong" /> value cannot be converted from a <see cref="string" /> json value.
/// </summary>
public class Ulong64JsonConversionException(string message) : JsonException(message);