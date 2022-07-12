using Color_Chan.Discord.Core.Common.API.DataModels.Message;
using Color_Chan.Discord.Core.Common.Models.Message;

namespace Color_Chan.Discord.Rest.Models.Message;

/// <inheritdoc cref="IDiscordMessageActivity" />
public class DiscordMessageActivity : IDiscordMessageActivity
{
    /// <summary>
    ///     Initializes a new <see cref="DiscordMessageActivity" />
    /// </summary>
    /// <param name="data">The data needed to create the <see cref="DiscordMessageActivity" />.</param>
    public DiscordMessageActivity(DiscordMessageActivityData data)
    {
        Type = data.Type;
        PartyId = data.PartyId;
    }

    /// <inheritdoc />
    public DiscordMessageActivityType Type { get; set; }

    /// <inheritdoc />
    public string? PartyId { get; set; }
}