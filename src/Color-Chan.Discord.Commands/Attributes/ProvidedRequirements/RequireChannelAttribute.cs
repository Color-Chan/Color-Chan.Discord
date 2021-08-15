using System;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Models.Contexts;
using Color_Chan.Discord.Commands.Models.Results;
using Color_Chan.Discord.Core.Results;

namespace Color_Chan.Discord.Commands.Attributes.ProvidedRequirements
{
    /// <summary>
    ///     This attribute requires a interaction to be executed in a specific channel.
    /// </summary>
    /// <example>
    ///     <code language="cs">
    ///     [RequireChannel(0123456789)]
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
    public class RequireChannelAttribute : InteractionRequirementAttribute
    {
        private readonly ulong _channelId;

        /// <summary>
        ///     Initializes a new instance of <see cref="RequireChannelAttribute" />.
        /// </summary>
        /// <param name="channelId">The ID of the channel.</param>
        public RequireChannelAttribute(ulong channelId)
        {
            _channelId = channelId;
        }

        /// <inheritdoc />
        public override Task<Result> CheckRequirementAsync(IInteractionContext context, IServiceProvider services)
        {
            return Task.FromResult(context.ChannelId != _channelId
                                       ? Result.FromError(new RequireChannelErrorResult($"Channel with ID {_channelId.ToString()} is required to use this command."))
                                       : Result.FromSuccess());
        }
    }
}