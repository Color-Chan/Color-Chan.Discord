<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->

[![NuGet][nuget-version-shield]][package-url]
[![NuGet][nuget-downloads-shield]][package-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]
[![Stargazers][stars-shield]][stars-url]

<!-- PROJECT LOGO -->
<br />
<p align="center">
  <a href="https://github.com/Color-Chan/Color-Chan.Discord">
    <img src="https://cdn.colorchan.com/pfp/pfp3/Color-Chan03_round_512x.png" alt="Logo" width="120" height="120">
  </a>

  <h3 align="center">Color-Chan.Discord</h3>

  <p align="center">
    A Discord library made in .NET 5 for interactions using HTTPS.
    <br />
    <a href="https://discord-library.colorchan.com/"><strong>Explore the docs »</strong></a>
    <br />
    <br />
    <a href="https://github.com/Color-Chan/Color-Chan.Discord">View Demo W.I.P</a>
    ·
    <a href="https://github.com/Color-Chan/Color-Chan.Discord/issues">Report Bug</a>
    ·
    <a href="https://github.com/Color-Chan/Color-Chan.Discord/issues">Request Feature</a>
  </p>
</p>



<!-- TABLE OF CONTENTS -->
<details open="open">
  <summary><h2 style="display: inline-block">Table of Contents</h2></summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#versioning">Versioning</a></li>
    <li><a href="#acknowledgements">Acknowledgements</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

Color-Chan.Discord is a C# Discord library made for slash commands, using Discord webhooks, .NET 5 and ASP.NET.  
The library is still in development so there expect some bug here and there, but please report it if you find one!

Color-Chan.Discord was originally made because i wanted full controll over what the bot exactly does, i also wanted it to be reliable and it had to run without a connection with discord gateway.


### Built With

* [.NET 5](https://dotnet.microsoft.com/download/dotnet/5.0)
* [ASP.NET](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-5.0)


<!-- GETTING STARTED -->
## Getting Started

To get a local copy up and running follow these simple steps.


### Prerequisites

* [.NET 5](https://dotnet.microsoft.com/download/dotnet/5.0)
* [ASP.NET](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-5.0)

### Installation

#### Nuget

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


#### Cloning


1. Clone the repo
   ```sh
   git clone https://github.com/Color-Chan/Color-Chan.Discord.git
   ```
2. Build the repo
   ```sh
   dotnet build
   ```



<!-- USAGE EXAMPLES -->
## Usage


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


### Commands
After this you can start to create your own commands!
Here is a simple example on how you can do that.


```csharp
public class PongCommands : SlashCommandModule
{
    /// <summary>
    ///     A simple Ping Pong command.
    /// </summary>
    [SlashCommand("ping", "Ping Pong!")]
    public Task<IDiscordInteractionResponse> PongAsync()
    {
        //  Build the response embed.
        var embedBuilder = new DiscordEmbedBuilder()
                           .WithTitle("Ping response")
                           .WithDescription("Pong!")
                           .WithColor(Color.Aqua)
                           .WithTimeStamp();

        // Build the response with the embed.
        var response = new SlashCommandResponseBuilder()
                       .WithEmbed(embedBuilder.Build())
                       .MakePrivate()
                       .Build();

        //  Return the response to Discord.
        return Task.FromResult(response);
    }
}
```


### URL

The interaction end point is located at `https://YOUR_DOMAIN.COM/api/v1/discord/interaction`.  
You will need to add this URL to you [application](https://discord.com/developers/applications/).
&nbsp;  
[![interactionUrlSetup](https://cdn.colorchan.com/examples/interactionUrlExample.png)](https://discord.com/developers/applications/)

_For more examples, please refer to the [Samples folder](https://github.com/Color-Chan/Color-Chan.Discord/tree/main/samples)_



<!-- ROADMAP -->
## Roadmap

See the [open issues](https://github.com/Color-Chan/Color-Chan.Discord/issues) for a list of proposed features (and known issues) W.I.P.



<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to be learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request



<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE` for more information.


## Versioning

Color-Chan.Discord uses [Semantic Versioning 2.0.0](https://semver.org/#semantic-versioning-200) for its versioning.


### Summary

The versioning will be using the following format: MAJOR.MINOR.PATCH.

* MAJOR version when you make incompatible API changes,
* MINOR version when you add functionality in a backwards compatible manner, and
* PATCH version when you make backwards compatible bug fixes.
* Additional labels for pre-release and build metadata are available as extensions to the MAJOR.MINOR.PATCH format.



<!-- ACKNOWLEDGEMENTS -->
## Acknowledgements

* [Sodium.core](https://github.com/tabrath/libsodium-core)
* [Microsoft.Extensions.Http.Polly](https://www.nuget.org/packages/Microsoft.Extensions.Http.Polly/)
* [Readme-template](https://github.com/othneildrew/Best-README-Template)
* [Scrutor](https://github.com/khellang/Scrutor)





<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/Color-Chan/Color-Chan.Discord.svg?style=for-the-badge
[contributors-url]: https://github.com/Color-Chan/Color-Chan.Discord/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/Color-Chan/Color-Chan.Discord.svg?style=for-the-badge
[forks-url]: https://github.com/Color-Chan/Color-Chan.Discord/network/members
[stars-shield]: https://img.shields.io/github/stars/Color-Chan/Color-Chan.Discord.svg?style=for-the-badge
[stars-url]: https://github.com/Color-Chan/Color-Chan.Discord/stargazers
[issues-shield]: https://img.shields.io/github/issues/Color-Chan/Color-Chan.Discord.svg?style=for-the-badge
[issues-url]: https://github.com/Color-Chan/Color-Chan.Discord/issues
[license-shield]: https://img.shields.io/github/license/Color-Chan/Color-Chan.Discord.svg?style=for-the-badge
[license-url]: https://github.com/Color-Chan/Color-Chan.Discord/blob/master/LICENSE.txt
[package-url]: https://www.nuget.org/packages/Color-Chan.Discord
[nuget-version-shield]: https://img.shields.io/nuget/vpre/Color-Chan.Discord.svg?maxAge=600&style=for-the-badge
[nuget-downloads-shield]: https://img.shields.io/nuget/dt/Color-Chan.Discord.svg?maxAge=600&style=for-the-badge
