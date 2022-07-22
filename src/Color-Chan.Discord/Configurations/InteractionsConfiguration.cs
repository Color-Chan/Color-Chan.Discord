using Color_Chan.Discord.Controllers;

namespace Color_Chan.Discord.Configurations;

/// <summary>
///     Holds the configurations for interactions.
/// </summary>
public class InteractionsConfiguration
{
    /// <summary>
    ///     Whether or not the <see cref="DiscordInteractionController" /> should verify the incoming interactions.
    ///     This could be used to test interaction locally. It should never be turned off when the API is public.
    /// </summary>
    public bool VerifyInteractions { get; set; } = true;
}