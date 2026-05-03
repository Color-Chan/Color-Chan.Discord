using System.Reflection;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Services;
using Microsoft.Extensions.Hosting;

namespace Color_Chan.Discord.Extensions;

/// <summary>
///     Contains all the extension methods for <see cref="IHost" />.
/// </summary>
public static class HostExtensions
{
    /// <summary>
    ///     Add all slash commands from the <see cref="Assembly" />s to the <see cref="ISlashCommandService" />.
    /// </summary>
    /// <param name="host">
    ///     The <see cref="IHost" /> that contains the services where the <see cref="ISlashCommandService" /> is
    ///     registered.
    /// </param>
    /// <param name="assemblies">The <see cref="Assembly" />s where the commands are located.</param>
    /// <returns>
    ///     The used <see cref="IHost" />.
    /// </returns>
    public static async Task<IHost> RegisterSlashCommandsAsync(this IHost host, params Assembly[] assemblies)
    {
        await host.Services.RegisterSlashCommandsAsync(assemblies).ConfigureAwait(false);
        return host;
    }
}