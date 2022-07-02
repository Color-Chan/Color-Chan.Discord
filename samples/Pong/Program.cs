using System.Reflection;
using System.Threading.Tasks;
using Color_Chan.Discord.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Pong;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        // Register all the commands in an Assembly.
        await host.RegisterSlashCommandsAsync(Assembly.GetExecutingAssembly()).ConfigureAwait(false); // <----- 

        // Run the WebHost, and start accepting requests.
        await host.RunAsync().ConfigureAwait(false);
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
                   .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}