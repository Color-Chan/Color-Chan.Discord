using System;
using System.Reflection;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Color_Chan.Discord.Extensions;

/// <summary>
///     Contains all the extension methods for <see cref="IServiceProvider" />.
/// </summary>
public static class ServiceProviderExtensions
{
    /// <summary>
    ///     Registers all slash commands in an <see cref="Assembly" /> to the <see cref="ISlashCommandService" />.
    /// </summary>
    /// <param name="serviceProvider">The <see cref="IServiceProvider" /> that contains the services where the <see cref="ISlashCommandService" /> is registered.</param>
    /// <param name="assembly">The <see cref="Assembly" /> where the commands are located.</param>
    /// <returns>
    ///     The updated <see cref="IServiceProvider" />.
    /// </returns>
    public static async Task<IServiceProvider> RegisterSlashCommandsAsync(this IServiceProvider serviceProvider, Assembly assembly)
    {
        var scope = serviceProvider.CreateAsyncScope();
        
        // Get the slash command service.
        var commandService = scope.ServiceProvider.GetRequiredService<ISlashCommandService>();

        // Add all commands in an assembly to the slash command service.
        await commandService.AddInteractionCommandsAsync(assembly).ConfigureAwait(false);

        // Get the slash command service.
        var componentService = scope.ServiceProvider.GetRequiredService<IComponentService>();

        // Add all components in an assembly to the component service.
        await componentService.AddComponentsAsync(assembly).ConfigureAwait(false);
        
        return serviceProvider;
    }
}