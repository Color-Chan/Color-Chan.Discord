using System.Collections.Generic;
using Color_Chan.Discord.Commands.Models.Info;

namespace Color_Chan.Discord.Commands.Models.Contexts;

/// <summary>
///     The context for all component interactions.
/// </summary>
public interface IComponentContext : IInteractionContext
{
    /// <summary>
    ///     The arguments embedded in the <see cref="IComponentInfo.CustomId" />
    /// </summary>
    List<string> Args { get; set; }
}