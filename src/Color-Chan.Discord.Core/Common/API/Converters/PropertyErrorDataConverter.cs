using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.API.DataModels.Errors;

namespace Color_Chan.Discord.Core.Common.API.Converters;

/// <summary>
///     Converters a json object value to a <see cref="PropertyErrorData" />.
/// </summary>
public class PropertyErrorDataConverter : JsonConverter<PropertyErrorData>
{
    private const string StartErrorInfo = "_errors";

    /// <inheritdoc />
    public override PropertyErrorData Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject) throw new JsonException();
        if (!reader.Read()) throw new JsonException();

        List<PropertyErrorInfoData>? errors = null;
        Dictionary<string, PropertyErrorData>? memberErrors = null;

        while (reader.TokenType != JsonTokenType.EndObject)
        {
            if (reader.TokenType != JsonTokenType.PropertyName) throw new JsonException();

            var propertyName = reader.GetString();

            if (propertyName is null) throw new JsonException();
            if (!reader.Read()) throw new JsonException();

            // Check if the property contains the error info.
            if (propertyName.Equals(StartErrorInfo))
            {
                if (reader.TokenType != JsonTokenType.StartArray) throw new JsonException();

                // Deserialize the sub errors.
                errors = JsonSerializer.Deserialize<List<PropertyErrorInfoData>>(ref reader, options);
                if (!reader.Read()) throw new JsonException();

                continue;
            }

            // The current line in the reader contains an a sub error.
            memberErrors ??= new Dictionary<string, PropertyErrorData>();

            var propertyErrorDetails = JsonSerializer.Deserialize<PropertyErrorData>(ref reader, options);
            if (propertyErrorDetails is null) throw new JsonException();

            if (!reader.Read()) throw new JsonException();

            memberErrors.Add(propertyName, propertyErrorDetails);
        }

        return new PropertyErrorData
        {
            Errors = errors,
            SubErrors = memberErrors
        };
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, PropertyErrorData value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        {
            if (value.SubErrors is not null)
                foreach (var (propertyName, memberError) in value.SubErrors)
                {
                    writer.WritePropertyName(propertyName);
                    JsonSerializer.Serialize(writer, memberError, options);
                }

            if (value.Errors is not null)
            {
                writer.WritePropertyName(StartErrorInfo);
                JsonSerializer.Serialize(writer, value.Errors, options);
            }
        }
        writer.WriteEndObject();
    }
}