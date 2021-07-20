using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Configurations;
using Color_Chan.Discord.Commands.Models;
using Color_Chan.Discord.Commands.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Pong
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            
            // Create a new scope.
            using (var scope = host.Services.CreateScope())
            {
                // Get the slash command service.
                var commandService = scope.ServiceProvider.GetRequiredService<ISlashCommandService>();

                // Configure the slash command service.
                var configurations = new SlashCommandConfiguration
                {
                    SlashCommandsAutoSync = SlashCommandsAutoSync.AddUpdate | SlashCommandsAutoSync.Delete
                };
                commandService.Configure(configurations);
                
                // Add all commands in an assembly to the slash command service.
                await commandService.AddInteractionCommandsAsync(Assembly.GetExecutingAssembly()).ConfigureAwait(false);
            }
            
            // Run the WebHost, and start accepting requests.
            await host.RunAsync().ConfigureAwait(false);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}