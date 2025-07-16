using System.Reflection;
using Color_Chan.Discord.Configurations;
using Color_Chan.Discord.Extensions;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .SetBasePath(Path.Combine(AppContext.BaseDirectory))
    .AddJsonFile("appsettings.json")
    .AddJsonFile("appsettings.Development.json", true)
    .AddJsonFile("secrets/appsettings.kubernetes.secrets.json", true, true)
    .Build();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// Configure Color-Chan.Discord
var config = new ColorChanConfigurations
{
    SlashCommandConfigs = slashOptions =>
    {
        slashOptions.EnableAutoSync = true;
        slashOptions.SendDefaultErrorMessage = true;
    },
    ComponentInteractionConfig = componentOptions => { componentOptions.SendDefaultErrorMessage = true; }
};

// Replace the arguments with the data of your bot.
// See https://github.com/Color-Chan/Color-Chan.Discord/tree/main/samples/Secrets.md for more info.
// Note: It is not recommended to hardcode them in, loading them from an environment variable or from a json file is better.
var token = configuration["Discord:Token"];
var publicKey = configuration["Discord:PublicKey"];
var applicationId = configuration.GetValue<ulong>("Discord:ApplicationId");

ArgumentException.ThrowIfNullOrEmpty(token, nameof(token));
ArgumentException.ThrowIfNullOrEmpty(publicKey, nameof(publicKey));

builder.Services.AddColorChanDiscord(token, publicKey, applicationId, config); // <---

var app = builder.Build();

// Register all the commands in an Assembly.
await app.Services.RegisterSlashCommandsAsync(Assembly.GetExecutingAssembly()).ConfigureAwait(false); // <----- 

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policyBuilder => policyBuilder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseHttpsRedirection();

app.UseColorChanDiscord();// <---
app.UseRouting();
app.MapControllers();

app.Run();
