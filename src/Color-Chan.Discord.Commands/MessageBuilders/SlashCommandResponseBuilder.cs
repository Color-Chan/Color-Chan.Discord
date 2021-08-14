using System;
using System.Collections.Generic;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Core.Common.Models.Embed;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Rest.Models.Interaction;

namespace Color_Chan.Discord.Commands.MessageBuilders
{
    /// <summary>
    ///     Represents a builder class for creating <see cref="IDiscordInteractionResponse" />s.
    /// </summary>
    public class SlashCommandResponseBuilder
    {
        private const int MaxEmbeds = 10;

        /// <summary>
        ///     Allowed mentions object.
        /// </summary>
        private IDiscordAllowedMentions? _allowedMentions;

        /// <summary>
        ///     Message components.
        /// </summary>
        private List<IDiscordComponent>? _components;

        /// <summary>
        ///     The message content.
        /// </summary>
        private string? _content;

        /// <summary>
        ///     A list of embed that will be added tot he response.
        /// </summary>
        private List<IDiscordEmbed>? _embeds;

        /// <summary>
        ///     Interaction application command callback data flags
        /// </summary>
        private DiscordInteractionCallbackFlags? _flags;

        /// <summary>
        ///     Whether or not the response is TTS.
        /// </summary>
        private bool? _isTts;

        /// <summary>
        ///     Enables Text To Speech for the response.
        /// </summary>
        /// <returns>
        ///     The updated <see cref="SlashCommandResponseBuilder" />.
        /// </returns>
        public SlashCommandResponseBuilder EnableTts()
        {
            _isTts = true;
            return this;
        }

        /// <summary>
        ///     Makes the response only visible to the person that has used the slash command.
        /// </summary>
        /// <returns></returns>
        public SlashCommandResponseBuilder MakePrivate()
        {
            _flags = DiscordInteractionCallbackFlags.Ephemeral;
            return this;
        }

        /// <summary>
        ///     Adds content to the response.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>
        ///     The updated <see cref="SlashCommandResponseBuilder" />.
        /// </returns>
        public SlashCommandResponseBuilder WithContent(string content)
        {
            _content = content;
            return this;
        }

        /// <summary>
        ///     Adds an embed to the response.
        /// </summary>
        /// <param name="embed">The embed that will be added.</param>
        /// <returns>
        ///     The updated <see cref="SlashCommandResponseBuilder" />.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the max embed limit of 10 has been reached.</exception>
        public SlashCommandResponseBuilder WithEmbed(IDiscordEmbed embed)
        {
            _embeds ??= new List<IDiscordEmbed>();

            if (_embeds.Count > 10) throw new ArgumentOutOfRangeException(nameof(embed), $"Can not add more then {MaxEmbeds.ToString()} embeds to a response.");

            _embeds.Add(embed);
            return this;
        }

        /// <summary>
        ///     Sets the allowed mentions for the response.
        /// </summary>
        /// <param name="allowedMentions">The allowed mentions.</param>
        /// <returns>
        ///     The updated <see cref="SlashCommandResponseBuilder" />.
        /// </returns>
        public SlashCommandResponseBuilder WithAllowedMentions(IDiscordAllowedMentions allowedMentions)
        {
            _allowedMentions = allowedMentions;
            return this;
        }

        /// <summary>
        ///     Adds an component to the response.
        /// </summary>
        /// <param name="component">The new component.</param>
        /// <returns>
        ///     The updated <see cref="SlashCommandResponseBuilder" />.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the max component limit of 5 has been reached.</exception>
        public SlashCommandResponseBuilder WithComponent(IDiscordComponent component)
        {
            _components ??= new List<IDiscordComponent>();

            if (_components.Count >= 5) throw new ArgumentOutOfRangeException(nameof(component), "Can not add more then 5 components to one message");

            _components.Add(component);
            return this;
        }

        /// <summary>
        ///     Builds a <see cref="IDiscordInteractionResponse" /> with all the set values.
        /// </summary>
        /// <returns>
        ///     The build <see cref="IDiscordInteractionResponse" />.
        /// </returns>
        public IDiscordInteractionResponse Build()
        {
            return new DiscordInteractionResponse
            {
                Type = DiscordInteractionResponseType.ChannelMessageWithSource,
                Data = new DiscordInteractionCallback
                {
                    Components = _components,
                    Content = _content,
                    Embeds = _embeds,
                    Flags = _flags,
                    AllowedMentions = _allowedMentions,
                    IsTts = _isTts
                }
            };
        }
    }
}