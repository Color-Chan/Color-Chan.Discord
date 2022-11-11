using System;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Models.Contexts;
using Color_Chan.Discord.Commands.Models.Results;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Commands.Attributes.ProvidedRequirements;

/// <summary>
///     Specifies a rate limit for a interaction request or command group for the whole guild.
/// </summary>
/// <remarks>
///     This requirement will limit the amount of time all the users in a guild can request a interaction during a time
///     period.
/// </remarks>
/// <example>
///     This example limits all the interactions in the PongCommands slash command module to 2 requests every 10 seconds
///     and 4 requests every 30 seconds for the whole guild. You can also put the
///     <see cref="GuildRateLimitAttribute" /> on a method if
///     you only want to rate limit a specific interaction.
///     <code language="cs">
///     [GuildRateLimit(2, 10)]
///     [GuildRateLimit(4, 30)]
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
public class GuildRateLimitAttribute : BaseRateLimitAttribute
{
    /// <summary>
    ///     Initializes a new instance of <see cref="GuildRateLimitAttribute" />.
    /// </summary>
    /// <param name="max">The max amount of time the command could be used during the time period.</param>
    /// <param name="resetAfterSeconds">The timeframe in which the command can be used a certain amount of times.</param>
    public GuildRateLimitAttribute(int max, int resetAfterSeconds) : base(max, resetAfterSeconds)
    {
    }

    /// <inheritdoc />
    public override async Task<Result> CheckRequirementAsync(IInteractionContext context, IServiceProvider services)
    {
        if (context.GuildId is null)
        {
            // Ignore DM.s
            return Result.FromSuccess();
        }

        var result = await CheckRateLimitAsync(context, services, context.GuildId.Value).ConfigureAwait(false);
        if (!result.IsSuccessful)
        {
            if (result.ErrorResult is BaseRateLimitErrorResult baseError)
            {
                return Result.FromError(new GuildRateLimitErrorResult(baseError, context.GuildId.Value));
            }

            throw new Exception("The error result was not the correct type");
        }

        return Result.FromSuccess();
    }
}