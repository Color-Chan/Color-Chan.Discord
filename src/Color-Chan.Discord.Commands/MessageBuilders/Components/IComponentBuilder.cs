using Color_Chan.Discord.Core.Common.Models;

namespace Color_Chan.Discord.Commands.MessageBuilders.Components;

/// <summary>
///     This interface serves as a builder for creating message components.
/// </summary>
public interface IComponentBuilder
{
    /// <summary>
    ///     Creates a <see cref="IDiscordComponent"/> from the current state of the builder.
    /// </summary>
    /// <returns>
    ///     The new <see cref="IDiscordComponent"/> instance.
    /// </returns>
    IDiscordComponent Build();
}