# Color-Chan.Discord
[![Color-Chan.Discord](https://github.com/Color-Chan/Color-Chan.Discord/actions/workflows/dotnet.yml/badge.svg)](https://github.com/Color-Chan/Color-Chan.Discord/actions/workflows/dotnet.yml)
[![CodeFactor](https://www.codefactor.io/repository/github/color-chan/color-chan.discord/badge)](https://www.codefactor.io/repository/github/color-chan/color-chan.discord)
[![NuGet](https://img.shields.io/nuget/vpre/Color-Chan.Discord.svg?maxAge=600?style=plastic)](https://www.nuget.org/packages/Color-Chan.Discord)
[![NuGet](https://img.shields.io/nuget/dt/Color-Chan.Discord.svg?maxAge=600?style=plastic)](https://www.nuget.org/packages/Color-Chan.Discord)


Color-Chan.Discord is a C# Discord library made for slash commands. Using Discord webhooks and .NET 5.
The library is still in development so it should not be used yet, it should oonly be used for testing purposes.

## 1. Installation
Color-Chan.Discord is available on [NuGet](https://www.nuget.org/packages/Color-Chan.Discord).
* [Color-Chan.Discord](https://www.nuget.org/packages/Color-Chan.Discord)
```powershell
Install-Package Color-Chan.Discord
```
OR
```powershell
dotnet add package Color-Chan.Discord
```
The induvidial components are also available on NuGet:
* [Color-Chan.Discord.Rest](https://www.nuget.org/packages/Color-Chan.Discord.Rest)
* [Color-Chan.Discord.Commands](https://www.nuget.org/packages/Color-Chan.Discord.Commands)
* [Color-Chan.Discord.Core](https://www.nuget.org/packages/Color-Chan.Discord.Core)

## 2. Usage
Create a new ASP.NET project and add the following to Program.cs and Startup.cs.

### Program.cs
You will have to replace `Assembly.GetExecutingAssembly()` with the assembly where your commands will be located.

```csharp
public static async Task Main(string[] args)
{
    var host = CreateHostBuilder(args).Build();
    
    // Configure Color-Chan.Discord.Commands
    // EnableAutoSync is off by default. Turn this own if you dont want to send the slash command to discord manually.
    var config = new SlashCommandConfiguration
    {
        EnableAutoSync = true // <----- 
    };  
    await host.RegisterSlashCommandsAsync(Assembly.GetExecutingAssembly(), config).ConfigureAwait(false); // <-----

    // Run the WebHost, and start accepting requests.
    await host.RunAsync().ConfigureAwait(false);
}
```

### Startup.cs
You will need to add your bot token, public key and application id, these can be found at [discordapp.com](https://discordapp.com/developers/applications/).

```csharp
public void ConfigureServices(IServiceCollection services)
{
    //  Replace the arguments with the data of your bot.
    //  Note: It is not recommended to hardcode them in, loading them from an environment variable or from a json file is better.
    services.AddColorChanDiscord("TOKEN", "PUBLIC_KEY", 999999999999999); // <---

    services.AddControllers();
}

public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    // This is needed to validate incoming interaction requests.
    app.UseColorChanDiscord(); // <---

    app.UseRouting();
    app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
}
```
The interaction end point will be located at `/api/v1/discord/interaction` after you've added everything.
Check the [samples](https://github.com/Color-Chan/Color-Chan.Discord/tree/main/samples) folder for more examples on how to set everything up.

## 3. Compiling
You will need the following to compile Color-Chan.Discord:

### With an IDE
* [Visual Studio 2017](https://visualstudio.microsoft.com/downloads/) or [Rider](https://www.jetbrains.com/rider/download/)
* [.NET sdk](https://dotnet.microsoft.com/download)

### With the command line
* [.NET sdk](https://dotnet.microsoft.com/download)

## 4. Versioning
Color-Chan.Discord uses [Semantic Versioning 2.0.0](https://semver.org/#semantic-versioning-200) for its versioning.
### Summary
The versioning will be using the following format: MAJOR.MINOR.PATCH.

* MAJOR version when you make incompatible API changes,
* MINOR version when you add functionality in a backwards compatible manner, and
* PATCH version when you make backwards compatible bug fixes.
* Additional labels for pre-release and build metadata are available as extensions to the MAJOR.MINOR.PATCH format.
