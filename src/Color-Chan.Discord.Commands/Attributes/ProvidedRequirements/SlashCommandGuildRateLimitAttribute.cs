using System;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Models.Contexts;
using Color_Chan.Discord.Commands.Models.Results;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Commands.Attributes.ProvidedRequirements
{
    /// <summary>
    ///     Specifies a rate limit for a command or command group for the whole guild.
    /// </summary>
    /// <remarks>
    ///     This requirement will limit the amount of time all the users in a guild can request a slash command during a time period.
    /// </remarks>
    /// <example>
    ///     This example limits all the slash commands in the PongCommands slash command module to 2 requests every 10 seconds
    ///     and 4 requests every 30 seconds for the whole guild. You can also put the <see cref="SlashCommandGuildRateLimitAttribute" /> on a method if
    ///     you
    ///     only want to rate limit a specific slash command.
    ///     <code language="cs">
    ///     [SlashCommandServerRateLimit(2, 10)]
    ///     [SlashCommandServerRateLimit(4, 30)]
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
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class SlashCommandGuildRateLimitAttribute : BaseSlashCommandRateLimitAttribute
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="SlashCommandGuildRateLimitAttribute" />.
        /// </summary>
        /// <param name="max">The max amount of time the command could be used during the time period.</param>
        /// <param name="resetAfterSeconds">The timeframe in which the command can be used a certain amount of times.</param>
        public SlashCommandGuildRateLimitAttribute(int max, int resetAfterSeconds) : base(max, resetAfterSeconds)
        {
        }

        /// <inheritdoc />
        public override async Task<Result> CheckRequirementAsync(ISlashCommandContext context, IServiceProvider services)
        {
            if (context.GuildId is null)
            {
                // Ignore DM.s
                return Result.FromSuccess();
            }
            
            var result = await CheckRateLimitAsync(context, services, context.GuildId.Value).ConfigureAwait(false);
            if (!result.IsSuccessful)
            {
                if(result.ErrorResult is BaseSlashCommandRateLimitErrorResult baseError)
                {
                    return Result.FromError(new SlashCommandGuildRateLimitErrorResult(baseError, context.GuildId.Value));
                }

                throw new Exception("The error result was not the correct type");
            }
            
            return Result.FromSuccess();
        }
    }
}