using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Core.Common.Models.Guild;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Common.Models.Message;
using Color_Chan.Discord.Rest.Models.Guild;

namespace Color_Chan.Discord.Rest.Models.Interaction
{
    public record DiscordInteraction : IDiscordInteraction
    {
        public DiscordInteraction(DiscordInteractionData data)
        {
            Id = data.Id;
            ApplicationId = data.ApplicationId;
            RequestType = data.RequestType;
            if (data.Data is not null) Data = new DiscordInteractionCommand(data.Data);
            GuildId = data.GuildId;
            ChannelId = data.ChannelId;
            if (data.GuildMember is not null) GuildMember = new DiscordGuildMember(data.GuildMember);
            if (data.User is not null) User = new DiscordUser(data.User);
            Token = data.Token;
            Versions = data.Versions;
        }

        /// <inheritdoc />
        public ulong Id { get; init; }

        /// <inheritdoc />
        public ulong ApplicationId { get; init; }

        /// <inheritdoc />
        public DiscordInteractionRequestType RequestType { get; init; }

        /// <inheritdoc />
        public IDiscordInteractionCommand? Data { get; init; }

        /// <inheritdoc />
        public ulong? GuildId { get; init; }

        /// <inheritdoc />
        public ulong? ChannelId { get; init; }

        /// <inheritdoc />
        public IDiscordGuildMember? GuildMember { get; init; }

        /// <inheritdoc />
        public IDiscordUser? User { get; init; }

        /// <inheritdoc />
        public string Token { get; init; }

        /// <inheritdoc />
        public int Versions { get; init; }

        /// <inheritdoc />
        public IDiscordMessage? Message { get; init; }

        /// <inheritdoc />
        public bool IsPingInteraction()
        {
            return RequestType == DiscordInteractionRequestType.Ping;
        }
    }
}