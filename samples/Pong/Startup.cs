using Color_Chan.Discord.Configurations;
using Color_Chan.Discord.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Pong
{
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

            //  Replace the arguments with the data of your bot.
            //  Note: It is not recommended to hardcode them in, loading them from an environment variable or from a json file is better.
            services.AddColorChanDiscord("ODY5MjE0NzU4OTIwOTIxMTE4.YP69Uw.V9zmxgAph-0JBDYSR7xmDvkh0B4", "d24324b6f62f166324c2b1e27a806a71c8c969572f956c067fd714ea4292f17f", 869214758920921118, config); // <---

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
}