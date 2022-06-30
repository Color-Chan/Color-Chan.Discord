using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.Models;

namespace Color_Chan.Discord.Rest.Models;

public record DiscordUser : IDiscordUser
{
    public DiscordUser(DiscordUserData data)
    {
        Id = data.Id;
        Username = data.Username;
        Discriminator = data.Discriminator;
        Avatar = data.Avatar;
        IsBot = data.IsBot;
        IsSystemUser = data.IsSystemUser;
        HasMfaEnabled = data.HasMfaEnabled;
        Locale = data.Locale;
        Verified = data.Verified;
        Email = data.Email;
        PrivateFlags = data.PrivateFlags;
        PremiumType = data.PremiumType;
        PublicFlags = data.PublicFlags;
    }

    /// <inheritdoc />
    public ulong Id { get; init; }

    /// <inheritdoc />
    public string Username { get; init; }

    /// <inheritdoc />
    public string Discriminator { get; init; }

    /// <inheritdoc />
    public string? Avatar { get; init; }

    /// <inheritdoc />
    public bool? IsBot { get; init; }

    /// <inheritdoc />
    public bool? IsSystemUser { get; init; }

    /// <inheritdoc />
    public bool? HasMfaEnabled { get; init; }

    /// <inheritdoc />
    public string? Locale { get; init; }

    /// <inheritdoc />
    public bool? Verified { get; init; }

    /// <inheritdoc />
    public string? Email { get; init; }

    /// <inheritdoc />
    public DiscordUserProperties? PrivateFlags { get; init; }

    /// <inheritdoc />
    public DiscordPremiumType? PremiumType { get; init; }

    /// <inheritdoc />
    public DiscordUserProperties? PublicFlags { get; init; }

    /// <inheritdoc />
    public DiscordUserData ToDataModel()
    {
        return new DiscordUserData
        {
            Avatar = Avatar,
            Discriminator = Discriminator,
            Email = Email,
            Id = Id,
            Locale = Locale,
            Username = Username,
            Verified = Verified,
            IsBot = IsBot,
            PremiumType = PremiumType,
            PrivateFlags = PrivateFlags,
            PublicFlags = PublicFlags,
            HasMfaEnabled = HasMfaEnabled,
            IsSystemUser = IsSystemUser
        };
    }
}