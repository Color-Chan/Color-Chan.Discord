using System.Text.Json;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.API.Converters;

namespace Color_Chan.Discord.Core.Extensions
{
    internal static class JsonSerializerOptionsExtensions
    {
        /// <summary>
        ///     Registers Color-Chan.Discord.Core's json options and converters.
        /// </summary>
        /// <param name="options">The <see cref="JsonSerializerOptions" />.</param>
        /// <returns>
        ///     The updated <see cref="JsonSerializerOptions" />.
        /// </returns>
        public static JsonSerializerOptions RegisterJsonOptions(this JsonSerializerOptions options)
        {
            options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

            options
                .AddJsonConverter<Uint64Converter>()
                .AddJsonConverter<ColorConverter>()
                .AddJsonConverter<PropertyErrorDataConverter>()
                .AddJsonConverter<DiscordGuildPermissionJsonConverter>()
                .AddJsonConverter<DiscordGuildFeatureConverter>()
                .AddJsonConverter<DiscordEmbedTypeConverter>();

            return options;
        }

        /// <summary>
        ///     Adds a <see cref="TConverter" /> of type <see cref="JsonConverter" /> to the <see cref="JsonSerializerOptions" />.
        /// </summary>
        /// <param name="options">The <see cref="JsonSerializerOptions" />.</param>
        /// <typeparam name="TConverter">The type of the <see cref="JsonConverter{T}" />.</typeparam>
        /// <returns>
        ///     The updated <see cref="JsonSerializerOptions" />.
        /// </returns>
        private static JsonSerializerOptions AddJsonConverter<TConverter>(this JsonSerializerOptions options) where TConverter : JsonConverter, new()
        {
            options.Converters.Add(new TConverter());
            return options;
        }
    }
}