using System.Reflection;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Configurations;
using Color_Chan.Discord.Commands.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Color_Chan.Discord.Extensions
{
    /// <summary>
    ///     Contains all the extension methods for <see cref="IHost" />.
    /// </summary>
    public static class HostExtensions
    {
        /// <summary>
        ///     Add all slash commands in an <see cref="Assembly" /> to the <see cref="ISlashCommandService" />.
        /// </summary>
        /// <param name="host">
        ///     The <see cref="IHost" /> that contains the services where the <see cref="ISlashCommandService" /> is
        ///     registered.
        /// </param>
        /// <param name="assembly">The <see cref="Assembly" /> where the commands are located.</param>
        /// <param name="config">The configurations that will be used while loading and updating the slash commands.</param>
        /// <returns>
        ///     The used <see cref="IHost" />.
        /// </returns>
        public static async Task<IHost> RegisterSlashCommandsAsync(this IHost host, Assembly assembly, SlashCommandConfiguration config)
        {
            // Create a new scope.
            using var scope = host.Services.CreateScope();

            // Get the slash command service.
            var commandService = scope.ServiceProvider.GetRequiredService<ISlashCommandService>();

            // Configure the slash command service.
            commandService.Configure(config);

            // Add all commands in an assembly to the slash command service.
            await commandService.AddInteractionCommandsAsync(assembly).ConfigureAwait(false);

            return host;
        }
    }
}