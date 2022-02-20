namespace Color_Chan.Discord.Core.Common.Models;

/// <summary>
///     Represents a discord Connection API model.
///     Docs: https://discord.com/developers/docs/resources/user#connection-object
/// </summary>
public interface IDiscordConnection
{
    /// <summary>
    ///     Id of the connection account.
    /// </summary>
    public ulong Id { get; set; }

    /// <summary>
    ///     The username of the connection account.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    ///     The service of the connection (twitch, youtube).
    /// </summary>
    public string Type { get; set; }
    
    /// <summary>
    ///     Whether the connection is revoked.
    /// </summary>
    public bool? Revoked { get; set; }

    // Todo: Add partial integrations.
    // public IReadOnlyList<> integrations { get; set; }
    
    /// <summary>
    ///     Whether the connection is verified.
    /// </summary>
    public bool Verified { get; set; }
    
    /// <summary>
    ///     Whether friend sync is enabled for this connection.
    /// </summary>
    public bool FriendSync { get; set; }
    
    /// <summary>
    ///     Whether activities related to this connection will be shown in presence updates.
    /// </summary>
    public bool ShowActivity { get; set; }
    
    /// <summary>
    ///     Visibility of this connection.
    /// </summary>
    public int Visibility { get; set; }
}