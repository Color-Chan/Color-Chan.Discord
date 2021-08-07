using System;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Results;
using Color_Chan.Discord.Core;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Commands.Attributes.ProvidedRequirements
{
    /// <summary>
    ///     Requires the slash command to be executed in a guild.
    /// </summary>
    /// <example>
    ///     This example limits all the slash commands in the PongCommands slash command module so they can only be used in
    ///     guilds. You can also put the <see cref="SlashRequireGuildAttribute" /> on a method if you only want to have it ona specific command.
    ///     <code language="cs">
    ///     [SlashRequireGuild]
    ///     public class PongCommands : SlashCommandModule
    ///     {
    ///         [SlashCommand("ping", "Ping Pong!")]
    ///         public Task&lt;IDiscordInteractionResponse&gt; PongAsync()
    ///         {
    ///             // Command code...
    ///         }
    ///     }
    ///     </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class SlashRequireGuildAttribute : SlashCommandRequirementAttribute
    {
        /// <inheritdoc />
        public override Task<Result> CheckRequirementAsync(ISlashCommandContext context, IServiceProvider services)
        {
            if (context.GuildId is not null && context.Member is not null)
            {
                return Task.FromResult(Result.FromSuccess());
            }
            
            return Task.FromResult(Result.FromError(new SlashCommandRequirementErrorResult("Command can not be executed in DMs")));
        }
    }
}