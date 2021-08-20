using System;
using System.Collections.Generic;
using System.Linq;
using Color_Chan.Discord.Commands.Exceptions;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Core.Common.Models.Select;
using Color_Chan.Discord.Rest.Models;
using Color_Chan.Discord.Rest.Models.Select;

namespace Color_Chan.Discord.Commands.MessageBuilders
{
    /// <summary>
    ///     A builder class used to build action row components.
    /// </summary>
    public class ActionRowComponentBuilder
    {
        private const int MaxCustomIdLength = 100;
        private const int MaxLabelLength = 80;
        private const int MaxButtons = 5;
        private const int MaxSelectOptions = 25;

        /// <summary>
        ///     A list of child components.
        /// </summary>
        private readonly List<IDiscordComponent> _childComponents = new();

        private DiscordComponent? _selectMenu;

        /// <summary>
        ///     Adds a button to the action row.
        /// </summary>
        /// <param name="label">The label of the button.</param>
        /// <param name="style">The style the button will use.</param>
        /// <param name="customId">The custom id. Required if <paramref name="url" /> is not set.</param>
        /// <param name="url">The URL the button will open. Required if <paramref name="customId" /> is not set.</param>
        /// <param name="emoji">The emoji the button will use.</param>
        /// <param name="disabled">Whether or not the button is disabled.</param>
        /// <returns>
        ///     The <see cref="ActionRowComponentBuilder" /> with the added button.
        /// </returns>
        /// <remarks>
        ///     Only one of <paramref name="url" /> or <paramref name="customId" /> can be set!
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">When the ActionRow already has 5 buttons.</exception>
        /// <exception cref="ArgumentOutOfRangeException">When <paramref name="label" /> contains more then 80 characters.</exception>
        /// <exception cref="MissingButtonPropertiesException">
        ///     When <paramref name="url" /> and <paramref name="customId" /> are
        ///     both null.
        /// </exception>
        /// <exception cref="ArgumentException">When <paramref name="url" /> <paramref name="customId" /> are both set.</exception>
        /// <exception cref="ArgumentNullException">When <paramref name="label" /> is null.</exception>
        /// <exception cref="InvalidActionRowException">When the <see cref="_childComponents" /> contains a select menu component.</exception>
        public ActionRowComponentBuilder WithButton(string label, DiscordButtonStyle style, string? customId, string? url = null, IDiscordEmoji? emoji = null, bool disabled = false)
        {
            if (customId is not null && customId.Length > MaxCustomIdLength)
                throw new ArgumentOutOfRangeException(nameof(customId), $"{nameof(customId)} can not be longer then {MaxCustomIdLength} characters.");
            if (_selectMenu is not null) throw new InvalidActionRowException("An action row can not have a select menu and buttons");
            if (_childComponents.Count >= MaxButtons) throw new ArgumentOutOfRangeException(nameof(_childComponents), $"An action row can not have more then {MaxButtons} buttons");
            if (label.Length > MaxLabelLength) throw new ArgumentOutOfRangeException(nameof(label), $"{nameof(label)} can not be longer then {MaxLabelLength} characters.");
            if (customId is null && url is null) throw new MissingButtonPropertiesException($"{nameof(url)} or {nameof(customId)} needs to be set");
            if (customId is not null && url is not null) throw new ArgumentException($"Only one of {nameof(url)} or {nameof(customId)} can be set");
            if (label is null) throw new ArgumentNullException(nameof(label));

            _childComponents.Add(new DiscordComponent
            {
                Disabled = disabled,
                CustomId = customId,
                Label = label,
                ButtonStyle = style,
                Emoji = emoji,
                Type = DiscordComponentType.Button,
                Url = url,
                ChildComponents = null
            });

            return this;
        }
        
        /// <summary>
        ///     Adds a button to the action row.
        /// </summary>
        /// <param name="emoji">The emoji the button will use.</param>
        /// <param name="style">The style the button will use.</param>
        /// <param name="customId">The custom id. Required if <paramref name="url" /> is not set.</param>
        /// <param name="url">The URL the button will open. Required if <paramref name="customId" /> is not set.</param>
        /// <param name="disabled">Whether or not the button is disabled.</param>
        /// <returns>
        ///     The <see cref="ActionRowComponentBuilder" /> with the added button.
        /// </returns>
        /// <remarks>
        ///     Only one of <paramref name="url" /> or <paramref name="customId" /> can be set!
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">When the ActionRow already has 5 buttons.</exception>
        /// <exception cref="MissingButtonPropertiesException">
        ///     When <paramref name="url" /> and <paramref name="customId" /> are
        ///     both null.
        /// </exception>
        /// <exception cref="ArgumentException">When <paramref name="url" /> <paramref name="customId" /> are both set.</exception>
        /// <exception cref="InvalidActionRowException">When the <see cref="_childComponents" /> contains a select menu component.</exception>
        public ActionRowComponentBuilder WithButton(IDiscordEmoji emoji, DiscordButtonStyle style, string? customId, string? url = null, bool disabled = false)
        {
            if (customId is not null && customId.Length > MaxCustomIdLength)
                throw new ArgumentOutOfRangeException(nameof(customId), $"{nameof(customId)} can not be longer then {MaxCustomIdLength} characters.");
            if (_selectMenu is not null) throw new InvalidActionRowException("An action row can not have a select menu and buttons");
            if (_childComponents.Count >= MaxButtons) throw new ArgumentOutOfRangeException(nameof(_childComponents), $"An action row can not have more then {MaxButtons} buttons");
            if (customId is null && url is null) throw new MissingButtonPropertiesException($"{nameof(url)} or {nameof(customId)} needs to be set");
            if (customId is not null && url is not null) throw new ArgumentException($"Only one of {nameof(url)} or {nameof(customId)} can be set");

            _childComponents.Add(new DiscordComponent
            {
                Disabled = disabled,
                CustomId = customId,
                ButtonStyle = style,
                Emoji = emoji,
                Type = DiscordComponentType.Button,
                Url = url
            });

            return this;
        }

        /// <summary>
        ///     Adds a select menu to the action row.
        /// </summary>
        /// <param name="customId">The custom id of the select menu.</param>
        /// <param name="placeholder">Custom placeholder text if nothing is selected, max 100 characters.</param>
        /// <param name="minValues">The minimum number of items that must be chosen; default 1, min 0, max 25.</param>
        /// <param name="maxValues">The maximum number of items that can be chosen; default 1, max 25.</param>
        /// <param name="disabled">Disable the select, default false.</param>
        /// <returns>
        ///     The <see cref="ActionRowComponentBuilder" /> with the added select menu.
        /// </returns>
        /// <exception cref="InvalidActionRowException">Thrown when the action row has button assigned to it.</exception>
        /// <exception cref="InvalidActionRowException">Thrown when the action row already has a select menu.</exception>
        public ActionRowComponentBuilder WithSelectMenu(string customId, string? placeholder = null, int? minValues = null, int? maxValues = null, bool? disabled = null)
        {
            if (_childComponents.Select(x => x.Type).Any(x => x == DiscordComponentType.Button)) throw new InvalidActionRowException("An action row can not have a select menu and buttons");
            if (_selectMenu is not null) throw new InvalidActionRowException("An action row can not have more then one select menu");

            _selectMenu = new DiscordComponent
            {
                Type = DiscordComponentType.SelectMenu,
                CustomId = customId,
                SelectOptions = new List<IDiscordSelectOption>(),
                Placeholder = placeholder,
                MinValues = minValues,
                MaxValues = maxValues,
                Disabled = disabled,
                ChildComponents = new List<IDiscordComponent>()
            };

            return this;
        }

        /// <summary>
        ///     Adds a select menu option to the select menu.
        /// </summary>
        /// <param name="label">The user-facing name of the option, max 100 characters.</param>
        /// <param name="value">The dev-define value of the option, max 100 characters.</param>
        /// <param name="description">An additional description of the option, max 100 characters.</param>
        /// <param name="emoji">The emoji containing the id, name, and animated.</param>
        /// <param name="disabled">Will render this option as selected by default.</param>
        /// <returns>
        ///     The <see cref="ActionRowComponentBuilder" /> with the added select menu option.
        /// </returns>
        /// <exception cref="InvalidActionRowException">Thrown when the action row has button assigned to it.</exception>
        /// <exception cref="InvalidActionRowException">Thrown when the action row does not contain a select menu.</exception>
        /// <exception cref="NullReferenceException">Thrown when the <see cref="IDiscordComponent.SelectOptions" /> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the select menu already contains 25 option.</exception>
        public ActionRowComponentBuilder WithSelectMenuOption(string label, string value, string? description = null, IDiscordEmoji? emoji = null, bool? disabled = false)
        {
            if (_childComponents.Select(x => x.Type).Any(x => x == DiscordComponentType.Button)) throw new InvalidActionRowException("An action row can not have a select menu and buttons");

            if (_selectMenu is null) throw new InvalidActionRowException("The action row needs to contain a select menu in order to add a select menu option.");
            if (_selectMenu.SelectOptions is null) throw new NullReferenceException($"{nameof(_selectMenu.SelectOptions)} can not be null");
            if (_selectMenu.SelectOptions.Count >= MaxSelectOptions)
                throw new ArgumentOutOfRangeException(nameof(_selectMenu.SelectOptions), $"A select menu can not have more then {MaxSelectOptions} options");

            _selectMenu.SelectOptions.Add(new DiscordSelectOption(label, value)
            {
                Description = description,
                Emoji = emoji,
                Default = disabled
            });

            return this;
        }

        /// <summary>
        ///     Builds the action row <see cref="IDiscordComponent" />.
        /// </summary>
        /// <returns>
        ///     The build action row <see cref="IDiscordComponent" />.
        /// </returns>
        public IDiscordComponent Build()
        {
            if (_selectMenu is not null)
            {
                return new DiscordComponent
                {
                    Type = DiscordComponentType.ActionRow,
                    ChildComponents = new List<IDiscordComponent>
                    {
                        _selectMenu
                    }
                };
            }

            return new DiscordComponent
            {
                Type = DiscordComponentType.ActionRow,
                ChildComponents = _childComponents
            };
        }
    }
}