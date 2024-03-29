using System;
using Color_Chan.Discord.Commands.Extensions;
using Color_Chan.Discord.Configurations;
using Color_Chan.Discord.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RoleManager.Pipelines;

namespace RoleManager;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        // Configure Color-Chan.Discord
        var config = new ColorChanConfigurations
        {
            SlashCommandConfigs = slashOptions =>
            {
                slashOptions.EnableAutoSync = true;
                slashOptions.EnableAutoGetGuild = true;
                slashOptions.SendDefaultErrorMessage = true;
            }
        };

        // Replace the arguments with the data of your bot.
        // See https://github.com/Color-Chan/Color-Chan.Discord/tree/main/samples/Secrets.md for more info.
        // Note: It is not recommended to hardcode them in, loading them from an environment variable or from a json file is better.
        var token = Configuration["Discord:Token"];
        var publicKey = Configuration["Discord:PublicKey"];
        var applicationId = Configuration.GetValue<ulong>("Discord:ApplicationId");

        ArgumentException.ThrowIfNullOrEmpty(token, nameof(token));
        ArgumentException.ThrowIfNullOrEmpty(publicKey, nameof(publicKey));

        services.AddColorChanDiscord(token, publicKey, applicationId, config); // <---

        // Register your custom pipelines if any.
        services.AddInteractionPipeline<PerformancePipeline>(); // <---

        services.AddControllers();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // This is needed to validate incoming interaction requests.
        app.UseColorChanDiscord(); // <---

        app.UseRouting();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}