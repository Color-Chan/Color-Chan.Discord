using System;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Models.Contexts;
using Color_Chan.Discord.Commands.Models.Results;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Commands.Attributes.ProvidedRequirements;

/// <summary>
///     Specifies a rate limit for a command or command group for a user.
/// </summary>
/// <remarks>
///     This requirement will limit the amount of time a user can request interactions during a time period.
/// </remarks>
/// <example>
///     This example limits all the interaction commands in the PongCommands interaction command module to 2 requests every
///     10 seconds
///     and 4 requests every 30 seconds per user. You can also put the <see cref="UserRateLimitAttribute" /> on
///     a method if
///     you only want to rate limit a specific interaction.
///     <code language="cs">
///     [UserRateLimit(2, 10)]
///     [UserRateLimit(4, 30)]
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
public class UserRateLimitAttribute : BaseRateLimitAttribute
{
    /// <summary>
    ///     Initializes a new instance of <see cref="UserRateLimitAttribute" />.
    /// </summary>
    /// <param name="max">The max amount of time the command could be used during the time period.</param>
    /// <param name="resetAfterSeconds">The timeframe in which the command can be used a certain amount of times.</param>
    public UserRateLimitAttribute(int max, int resetAfterSeconds) : base(max, resetAfterSeconds)
    {
    }

    /// <inheritdoc />
    public override async Task<Result> CheckRequirementAsync(IInteractionContext context, IServiceProvider services)
    {
        var result = await CheckRateLimitAsync(context, services, context.User.Id).ConfigureAwait(false);
        if (!result.IsSuccessful)
        {
            if (result.ErrorResult is BaseRateLimitErrorResult baseError)
            {
                return Result.FromError(new UserRateLimitErrorResult(baseError, context.User));
            }

            throw new Exception("The error result was not the correct type");
        }

        return Result.FromSuccess();
    }
}