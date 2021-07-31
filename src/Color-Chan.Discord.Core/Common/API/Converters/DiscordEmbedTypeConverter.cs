using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.API.DataModels.Embed;

namespace Color_Chan.Discord.Core.Common.API.Converters
{
    /// <summary>
    ///     Converters a <see cref="string" /> json value to a <see cref="DiscordEmbedType" />.
    /// </summary>
    public class DiscordEmbedTypeConverter : JsonConverter<DiscordEmbedType>
    {
        /// <inheritdoc />
        public override DiscordEmbedType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return value switch
            {
                "rich" => DiscordEmbedType.Rich,
                "image" => DiscordEmbedType.Image,
                "video" => DiscordEmbedType.Video,
                "gifv" => DiscordEmbedType.Gif,
                "article" => DiscordEmbedType.Article,
                "link" => DiscordEmbedType.Link,
                _ => throw new ArgumentOutOfRangeException(nameof(value), value, "unknown DiscordEmbedType type")
            };
        }

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, DiscordEmbedType value, JsonSerializerOptions options)
        {
            switch (value)
            {
                case DiscordEmbedType.Rich:
                    writer.WriteStringValue("rich");
                    break;
                case DiscordEmbedType.Image:
                    writer.WriteStringValue("image");
                    break;
                case DiscordEmbedType.Video:
                    writer.WriteStringValue("video");
                    break;
                case DiscordEmbedType.Gif:
                    writer.WriteStringValue("gifv");
                    break;
                case DiscordEmbedType.Article:
                    writer.WriteStringValue("article");
                    break;
                case DiscordEmbedType.Link:
                    writer.WriteStringValue("link");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, "unknown DiscordEmbedType type");
            }
        }
    }
}