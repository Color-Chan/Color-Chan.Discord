using System;
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Models.Contexts;
using Color_Chan.Discord.Commands.Models.Results;
using Color_Chan.Discord.Core.Common.API.Rest;
using Color_Chan.Discord.Core.Common.Models.Guild;
using Color_Chan.Discord.Core.Results;
using Microsoft.Extensions.DependencyInjection;

namespace Color_Chan.Discord.Commands.Attributes.ProvidedRequirements
{
    /// <summary>
    ///     Requires the user that requested the interaction to be the owner of the guild.
    /// </summary>
    /// <example>
    ///     This example limits all the slash commands in the PongCommands slash command module so they can only be used by the
    ///     owner of the guild they are being requested in. You can also put the
    ///     <see cref="RequireGuildOwnerAttribute" />
    ///     on a method if you only want to have it on a specific command.
    ///     <code language="cs">
    ///     [RequireGuildOwner]
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
    public class RequireGuildOwnerAttribute : InteractionRequirementAttribute
    {
        /// <inheritdoc />
        public override async Task<Result> CheckRequirementAsync(IInteractionContext context, IServiceProvider services)
        {
            if (context.GuildId is null)
            {
                return Result.FromError(new RequireGuildOwnerErrorResult("Interaction can not be executed in DMs"));
            }

            IDiscordGuild? guild;
            if (context.Guild is not null)
            {
                guild = context.Guild;
            }
            else
            {
                var restGuild = services.GetRequiredService<IDiscordRestGuild>();
                var guildResult = await restGuild.GetGuildAsync(context.GuildId.Value).ConfigureAwait(false);
                guild = guildResult.Entity;
            }

            if (guild is null)
            {
                throw new NullReferenceException("Failed to get guild");
            }

            if (guild.OwnerId == context.User.Id)
            {
                return Result.FromSuccess();
            }

            return Result.FromError(new RequireGuildOwnerErrorResult("Guild owner is required"));
        }
    }
}