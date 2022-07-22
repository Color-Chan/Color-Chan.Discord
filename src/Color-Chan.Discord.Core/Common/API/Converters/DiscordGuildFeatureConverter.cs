using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;

namespace Color_Chan.Discord.Core.Common.API.Converters;

/// <summary>
///     Converters a <see cref="string" /> json value to a <see cref="DiscordGuildFeature" />.
/// </summary>
public class DiscordGuildFeatureConverter : JsonConverter<DiscordGuildFeature>
{
    /// <inheritdoc />
    public override DiscordGuildFeature Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return value switch
        {
            "ANIMATED_ICON" => DiscordGuildFeature.AnimatedIcon,
            "BANNER" => DiscordGuildFeature.Banner,
            "COMMERCE" => DiscordGuildFeature.Commerce,
            "COMMUNITY" => DiscordGuildFeature.Community,
            "DISCOVERABLE" => DiscordGuildFeature.Discoverable,
            "FEATURABLE" => DiscordGuildFeature.Featurable,
            "INVITE_SPLASH" => DiscordGuildFeature.InviteSplash,
            "MEMBER_VERIFICATION_GATE_ENABLED" => DiscordGuildFeature.MemberVerificationGateEnabled,
            "NEWS" => DiscordGuildFeature.News,
            "PARTNERED" => DiscordGuildFeature.Partnered,
            "PREVIEW_ENABLED" => DiscordGuildFeature.PreviewEnabled,
            "VANITY_URL" => DiscordGuildFeature.VanityUrl,
            "VERIFIED" => DiscordGuildFeature.Verified,
            "VIP_REGIONS" => DiscordGuildFeature.VipRegions,
            "WELCOME_SCREEN_ENABLED" => DiscordGuildFeature.WelcomeScreenEnabled,
            "TICKETED_EVENTS_ENABLED" => DiscordGuildFeature.TicketedEventsEnabled,
            "MONETIZATION_ENABLED" => DiscordGuildFeature.MonetizationEnabled,
            "MORE_STICKERS" => DiscordGuildFeature.MoreStickers,
            "THREE_DAY_THREAD_ARCHIVE" => DiscordGuildFeature.ThreeDayThreadArchive,
            "SEVEN_DAY_THREAD_ARCHIVE" => DiscordGuildFeature.SevenDayThreadArchive,
            "PRIVATE_THREADS" => DiscordGuildFeature.PrivateThreads,
            _ => DiscordGuildFeature.Unknown
        };
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, DiscordGuildFeature value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case DiscordGuildFeature.AnimatedIcon:
                writer.WriteStringValue("ANIMATED_ICON");
                break;
            case DiscordGuildFeature.Banner:
                writer.WriteStringValue("BANNER");
                break;
            case DiscordGuildFeature.Commerce:
                writer.WriteStringValue("COMMERCE");
                break;
            case DiscordGuildFeature.Community:
                writer.WriteStringValue("COMMUNITY");
                break;
            case DiscordGuildFeature.Discoverable:
                writer.WriteStringValue("DISCOVERABLE");
                break;
            case DiscordGuildFeature.Featurable:
                writer.WriteStringValue("FEATURABLE");
                break;
            case DiscordGuildFeature.InviteSplash:
                writer.WriteStringValue("INVITE_SPLASH");
                break;
            case DiscordGuildFeature.MemberVerificationGateEnabled:
                writer.WriteStringValue("MEMBER_VERIFICATION_GATE_ENABLED");
                break;
            case DiscordGuildFeature.News:
                writer.WriteStringValue("NEWS");
                break;
            case DiscordGuildFeature.Partnered:
                writer.WriteStringValue("PARTNERED");
                break;
            case DiscordGuildFeature.PreviewEnabled:
                writer.WriteStringValue("PREVIEW_ENABLED");
                break;
            case DiscordGuildFeature.VanityUrl:
                writer.WriteStringValue("VANITY_URL");
                break;
            case DiscordGuildFeature.Verified:
                writer.WriteStringValue("VERIFIED");
                break;
            case DiscordGuildFeature.VipRegions:
                writer.WriteStringValue("VIP_REGIONS");
                break;
            case DiscordGuildFeature.WelcomeScreenEnabled:
                writer.WriteStringValue("WELCOME_SCREEN_ENABLED");
                break;
            case DiscordGuildFeature.TicketedEventsEnabled:
                writer.WriteStringValue("TICKETED_EVENTS_ENABLED");
                break;
            case DiscordGuildFeature.MonetizationEnabled:
                writer.WriteStringValue("MONETIZATION_ENABLED");
                break;
            case DiscordGuildFeature.MoreStickers:
                writer.WriteStringValue("MORE_STICKERS");
                break;
            case DiscordGuildFeature.ThreeDayThreadArchive:
                writer.WriteStringValue("THREE_DAY_THREAD_ARCHIVE");
                break;
            case DiscordGuildFeature.SevenDayThreadArchive:
                writer.WriteStringValue("SEVEN_DAY_THREAD_ARCHIVE");
                break;
            case DiscordGuildFeature.PrivateThreads:
                writer.WriteStringValue("PRIVATE_THREADS");
                break;
            case DiscordGuildFeature.Unknown:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(value), value, null);
        }
    }
}