using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.Converters
{
    /// <summary>
    ///     Converters a <see cref="ulong" /> <see cref="string" /> json value to a <see cref="ulong" />.
    /// </summary>
    public class Uint64Converter : JsonConverter<ulong>
    {
        /// <inheritdoc />
        public override ulong Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var ulongString = reader.GetString();

            if (ulong.TryParse(ulongString, out var number)) return number;

            throw new JsonException("Failed to convert uin64 (ulong)");
        }

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, ulong value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}