#pragma warning disable 1998
using System.Threading.Tasks;
using Color_Chan.Discord.Commands.Attributes;
using Color_Chan.Discord.Commands.MessageBuilders;
using Color_Chan.Discord.Commands.Modules;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;

namespace ButtonArgs.Components
{
    /// <summary>
    ///     The component module for the args button components.
    /// </summary>
    public class ArgsButtonComponent : ComponentInteractionModule
    {
        public const string ButtonId = "button_with_args";

        /// <summary>
        ///     Edits a message with a component with the id of <see cref="ButtonId" />.
        /// </summary>
        [Component(ButtonId, DiscordComponentType.Button)]
        public async Task<Result<IDiscordInteractionResponse>> ArgsButtonButtonAsync()
        {
            var embedBuilder = new DiscordEmbedBuilder().WithTitle("Supplied arguments");
            foreach (var arg in Context.Args)
            {
                embedBuilder.WithField(Context.Args.IndexOf(arg).ToString(), arg);
            }

            var response = new InteractionResponseBuilder()
                           .WithEmbed(embedBuilder.Build())
                           .EmptyComponents()
                           .Build(DiscordInteractionCallbackType.UpdateMessage);

            return FromSuccess(response);
        }
    }
}