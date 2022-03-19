using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.Models;

namespace Color_Chan.Discord.Rest.Models;

/// <inheritdoc cref="IDiscordConnection"/>
public class DiscordConnection : IDiscordConnection
{
    /// <summary>
    ///     Initializes a new <see cref="DiscordConnectionData"/>
    /// </summary>
    /// <param name="data">The data needed to create the <see cref="DiscordConnectionData"/>.</param>
    public DiscordConnection(DiscordConnectionData data)
    {
        Id = data.Id;
        Name = data.Name;
        Type = data.Type;
        Revoked = data.Revoked;
        Verified = data.Verified;
        FriendSync = data.FriendSync;
        ShowActivity = data.ShowActivity;
        Visibility = data.Visibility;
    }

    /// <inheritdoc cref="IDiscordConnection.Id"/>
    public ulong Id { get; set; }

    /// <inheritdoc cref="IDiscordConnection.Name"/>
    public string Name { get; set; } = null!;
    
    /// <inheritdoc cref="IDiscordConnection.Type"/>
    public string Type { get; set; } = null!;
    
    /// <inheritdoc cref="IDiscordConnection.Revoked"/>
    public bool? Revoked { get; set; }

    // Todo: Add partial integrations.
    // [JsonPropertyName("integrations")]
    // public IReadOnlyList<> integrations { get; set; }
    
    /// <inheritdoc cref="IDiscordConnection.Verified"/>
    public bool Verified { get; set; }
    
    /// <inheritdoc cref="IDiscordConnection.FriendSync"/>
    public bool FriendSync { get; set; }
    
    /// <inheritdoc cref="IDiscordConnection.ShowActivity"/>
    public bool ShowActivity { get; set; }
    
    /// <inheritdoc cref="IDiscordConnection.Visibility"/>
    public int Visibility { get; set; }
}