using System;
using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Color_Chan.Discord.Core.Common.API.Converters
{
    /// <summary>
    ///     Converters a <see cref="uint" /> json value to a <see cref="Color" />.
    /// </summary>
    public class ColorConverter : JsonConverter<Color>
    {
        /// <inheritdoc />
        public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.Number)
                throw new JsonException();

            var colorNumber = reader.GetInt32();
            return Color.FromArgb((int) (colorNumber ^ 0xFF000000));
        }

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue((uint) value.ToArgb() & 0x00FFFFFF);
        }
    }
}