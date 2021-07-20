﻿using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Core.Extensions;

namespace Color_Chan.Discord.Core.Common.API.Converters
{
    /// <summary>
    ///     Converters a <see cref="ulong"/> <see cref="string"/> json value to a <see cref="DiscordGuildPermission"/>.
    /// </summary>
    public class DiscordGuildPermissionJsonConverter : JsonConverter<DiscordGuildPermission>
    {
        /// <inheritdoc />
        public override DiscordGuildPermission Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var permissionString = reader.GetString();

            if (!permissionString.TryParseDiscordGuildPermission(out var permissions)) throw new JsonException("Failed to parse DiscordGuildPermission");

            return permissions.Value;
        }

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, DiscordGuildPermission value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ConvertToString());
        }
    }
}