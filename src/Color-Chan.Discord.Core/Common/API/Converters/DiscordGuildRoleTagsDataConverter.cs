using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Core.Exceptions;

namespace Color_Chan.Discord.Core.Common.API.Converters;

/// <summary>
///     Enables us to have a decent domain model for <see cref="DiscordGuildRoleTagsData" />.
///     I hate discord so much for implementing this in such a dumb way....
///     Why is boolean true when the json property exists, and false when it doesn't?
///     They better have a good reason for this (:
/// </summary>
public class DiscordGuildRoleTagsDataConverter : JsonConverter<DiscordGuildRoleTagsData>
{
    /// <inheritdoc />
    public override DiscordGuildRoleTagsData? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException("Expected start of object.");
        }

        var tagsData = new DiscordGuildRoleTagsData();

        while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
        {
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException("Expected property name.");
            }

            var propertyName = reader.GetString();
            reader.Read();

            switch (propertyName)
            {
                case "bot_id":
                    tagsData.BotId = new Uint64Converter().Read(ref reader, typeof(ulong), options);
                    break;
                case "integration_id":
                    tagsData.IntegrationId = new Uint64Converter().Read(ref reader, typeof(ulong), options);
                    break;
                case "premium_subscriber":
                    tagsData.PremiumSubscriber = true;
                    break;
                case "subscription_listing_id":
                    tagsData.SubscriptionListingId = new Uint64Converter().Read(ref reader, typeof(ulong), options);
                    break;
                case "available_for_purchase":
                    tagsData.AvailableForPurchase = true;
                    break;
                case "guild_connections":
                    tagsData.GuildConnections = true;
                    break;
                default:
                    throw new JsonException($"Unknown property: {propertyName}");
            }
        }

        return tagsData;
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, DiscordGuildRoleTagsData value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        if (value.BotId.HasValue)
            writer.WriteString("bot_id", value.BotId.Value.ToString());

        if (value.IntegrationId.HasValue)
            writer.WriteString("integration_id", value.IntegrationId.Value.ToString());

        if (value.SubscriptionListingId.HasValue)
            writer.WriteString("subscription_listing_id", value.SubscriptionListingId.Value.ToString());

        if (value.PremiumSubscriber)
            writer.WriteNull("premium_subscriber");

        if (value.AvailableForPurchase)
            writer.WriteNull("available_for_purchase");

        if (value.GuildConnections)
            writer.WriteNull("guild_connections");

        writer.WriteEndObject();
    }
}