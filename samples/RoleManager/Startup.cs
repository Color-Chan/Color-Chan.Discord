using Color_Chan.Discord.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RoleManager
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
            //  Replace the arguments with the data of your bot.
            //  Note: It is not recommended to hardcode them in, loading them from an environment variable or from a json file is better.
            services.AddColorChanDiscord("TOKEN", "PUBLIC_KEY", 999999999999999); // <---

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