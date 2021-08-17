using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Common.Models.Interaction;

namespace Color_Chan.Discord.Rest.Models.Interaction
{
    public record DiscordInteractionOption : IDiscordInteractionOption
    {
        public DiscordInteractionOption(DiscordInteractionOptionData data)
        {
            Name = data.Name;
            Type = data.Type;
            Value = ConvertValueToCorrectType(data.JsonValue);
            SubOptions = data.SubOptions?.Select(interactionData => new DiscordInteractionOption(interactionData));
        }

        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public DiscordApplicationCommandOptionType Type { get; set; }

        /// <inheritdoc />
        public object? Value { get; set; }

        /// <inheritdoc />
        public IEnumerable<IDiscordInteractionOption>? SubOptions { get; set; }

        /// <inheritdoc />
        public string? GetStringValue()
        {
            if (Type != DiscordApplicationCommandOptionType.String || Value is null)
                throw new InvalidCastException("Can not cast Value if the Type is not a String");

            return Value as string;
        }

        /// <inheritdoc />
        public int GetIntValue()
        {
            if (Type != DiscordApplicationCommandOptionType.Integer || Value is null)
                throw new InvalidCastException("Can not cast Value if the Type is not a Integer");

            return (int)Value;
        }

        /// <inheritdoc />
        public double GetNumberValue()
        {
            if (Type != DiscordApplicationCommandOptionType.Number || Value is null)
                throw new InvalidCastException("Can not cast Value if the Type is not a Number");

            return (double)Value;
        }

        /// <inheritdoc />
        public bool GetBoolValue()
        {
            if (Type != DiscordApplicationCommandOptionType.Boolean || Value is null)
                throw new InvalidCastException("Can not cast Value if the Type is not a Boolean");

            return (bool)Value;
        }

        /// <inheritdoc />
        public ulong GetUserValue()
        {
            if (Type != DiscordApplicationCommandOptionType.User || Value is null)
                throw new InvalidCastException("Can not cast Value if the Type is not a User");

            return (ulong)Value;
        }

        /// <inheritdoc />
        public ulong GetChannelValue()
        {
            if (Type != DiscordApplicationCommandOptionType.Channel || Value is null)
                throw new InvalidCastException("Can not cast Value if the Type is not a Channel");

            return (ulong)Value;
        }

        /// <inheritdoc />
        public ulong GetRoleValue()
        {
            if (Type != DiscordApplicationCommandOptionType.Role || Value is null)
                throw new InvalidCastException("Can not cast Value if the Type is not a Role");

            return (ulong)Value;
        }

        private object? ConvertValueToCorrectType(JsonElement? jsonValue)
        {
            switch (Type)
            {
                case DiscordApplicationCommandOptionType.SubCommand:
                    return null;
                case DiscordApplicationCommandOptionType.SubCommandGroup:
                    return null;
                case DiscordApplicationCommandOptionType.String:
                    return jsonValue?.ToString();
                case DiscordApplicationCommandOptionType.Integer:
                    return jsonValue?.GetInt32();
                case DiscordApplicationCommandOptionType.Boolean:
                    return jsonValue?.GetBoolean();
                case DiscordApplicationCommandOptionType.Number:
                    return jsonValue?.GetDouble();
                case DiscordApplicationCommandOptionType.User:
                    if (ulong.TryParse(jsonValue?.GetString(), out var userId))
                        return userId;
                    break;
                case DiscordApplicationCommandOptionType.Channel:
                    if (ulong.TryParse(jsonValue?.GetString(), out var channelId))
                        return channelId;
                    break;
                case DiscordApplicationCommandOptionType.Role:
                    if (ulong.TryParse(jsonValue?.GetString(), out var roleId))
                        return roleId;
                    break;
                case DiscordApplicationCommandOptionType.Mentionable:
                    if (ulong.TryParse(jsonValue?.GetString(), out var mentionableId))
                        return mentionableId;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            throw new InvalidCastException("Failed to cast option value");
        }
    }
}