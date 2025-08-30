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
            "ANIMATED_BANNER" => DiscordGuildFeature.AnimatedBanner,
            "ANIMATED_ICON" => DiscordGuildFeature.AnimatedIcon,
            "APPLICATION_COMMAND_PERMISSIONS_V2" => DiscordGuildFeature.ApplicationCommandPermissionsV2,
            "AUTO_MODERATION" => DiscordGuildFeature.AutoModeration,
            "BANNER" => DiscordGuildFeature.Banner,
            "COMMERCE" => DiscordGuildFeature.Commerce,
            "COMMUNITY" => DiscordGuildFeature.Community,
            "CREATOR_MONETIZABLE_PROVISIONAL" => DiscordGuildFeature.CreatorMonetizableProvisional,
            "CREATOR_STORE_PAGE" => DiscordGuildFeature.CreatorStorePage,
            "DEVELOPER_SUPPORT_SERVER" => DiscordGuildFeature.DeveloperSupportServer,
            "DISCOVERABLE" => DiscordGuildFeature.Discoverable,
            "FEATURABLE" => DiscordGuildFeature.Featurable,
            "INVITES_DISABLED" => DiscordGuildFeature.InvitesDisabled,
            "INVITE_SPLASH" => DiscordGuildFeature.InviteSplash,
            "MEMBER_VERIFICATION_GATE_ENABLED" => DiscordGuildFeature.MemberVerificationGateEnabled,
            "MORE_SOUNDBOARD" => DiscordGuildFeature.MoreSoundboard,
            "MORE_STICKERS" => DiscordGuildFeature.MoreStickers,
            "NEWS" => DiscordGuildFeature.News,
            "PARTNERED" => DiscordGuildFeature.Partnered,
            "PREVIEW_ENABLED" => DiscordGuildFeature.PreviewEnabled,
            "RAID_ALERTS_DISABLED" => DiscordGuildFeature.RaidAlertsDisabled,
            "ROLE_ICONS" => DiscordGuildFeature.RoleIcons,
            "ROLE_SUBSCRIPTIONS_AVAILABLE_FOR_PURCHASE" => DiscordGuildFeature.RoleSubscriptionsAvailableForPurchase,
            "ROLE_SUBSCRIPTIONS_ENABLED" => DiscordGuildFeature.RoleSubscriptionsEnabled,
            "SOUNDBOARD" => DiscordGuildFeature.Soundboard,
            "TICKETED_EVENTS_ENABLED" => DiscordGuildFeature.TicketedEventsEnabled,
            "VANITY_URL" => DiscordGuildFeature.VanityUrl,
            "VERIFIED" => DiscordGuildFeature.Verified,
            "VIP_REGIONS" => DiscordGuildFeature.VipRegions,
            "WELCOME_SCREEN_ENABLED" => DiscordGuildFeature.WelcomeScreenEnabled,
            _ => DiscordGuildFeature.Unknown
        };
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, DiscordGuildFeature value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case DiscordGuildFeature.AnimatedBanner:
                writer.WriteStringValue("ANIMATED_BANNER");
                break;
            case DiscordGuildFeature.AnimatedIcon:
                writer.WriteStringValue("ANIMATED_ICON");
                break;
            case DiscordGuildFeature.ApplicationCommandPermissionsV2:
                writer.WriteStringValue("APPLICATION_COMMAND_PERMISSIONS_V2");
                break;
            case DiscordGuildFeature.AutoModeration:
                writer.WriteStringValue("AUTO_MODERATION");
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
            case DiscordGuildFeature.CreatorMonetizableProvisional:
                writer.WriteStringValue("CREATOR_MONETIZABLE_PROVISIONAL");
                break;
            case DiscordGuildFeature.CreatorStorePage:
                writer.WriteStringValue("CREATOR_STORE_PAGE");
                break;
            case DiscordGuildFeature.DeveloperSupportServer:
                writer.WriteStringValue("DEVELOPER_SUPPORT_SERVER");
                break;
            case DiscordGuildFeature.Discoverable:
                writer.WriteStringValue("DISCOVERABLE");
                break;
            case DiscordGuildFeature.Featurable:
                writer.WriteStringValue("FEATURABLE");
                break;
            case DiscordGuildFeature.InvitesDisabled:
                writer.WriteStringValue("INVITES_DISABLED");
                break;
            case DiscordGuildFeature.InviteSplash:
                writer.WriteStringValue("INVITE_SPLASH");
                break;
            case DiscordGuildFeature.MemberVerificationGateEnabled:
                writer.WriteStringValue("MEMBER_VERIFICATION_GATE_ENABLED");
                break;
            case DiscordGuildFeature.MoreSoundboard:
                writer.WriteStringValue("MORE_SOUNDBOARD");
                break;
            case DiscordGuildFeature.MoreStickers:
                writer.WriteStringValue("MORE_STICKERS");
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
            case DiscordGuildFeature.RaidAlertsDisabled:
                writer.WriteStringValue("RAID_ALERTS_DISABLED");
                break;
            case DiscordGuildFeature.RoleIcons:
                writer.WriteStringValue("ROLE_ICONS");
                break;
            case DiscordGuildFeature.RoleSubscriptionsAvailableForPurchase:
                writer.WriteStringValue("ROLE_SUBSCRIPTIONS_AVAILABLE_FOR_PURCHASE");
                break;
            case DiscordGuildFeature.RoleSubscriptionsEnabled:
                writer.WriteStringValue("ROLE_SUBSCRIPTIONS_ENABLED");
                break;
            case DiscordGuildFeature.Soundboard:
                writer.WriteStringValue("SOUNDBOARD");
                break;
            case DiscordGuildFeature.TicketedEventsEnabled:
                writer.WriteStringValue("TICKETED_EVENTS_ENABLED");
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
            case DiscordGuildFeature.Unknown:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(value), value, null);
        }
    }
}