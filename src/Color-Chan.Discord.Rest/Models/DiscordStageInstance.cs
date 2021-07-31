using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.Models;

namespace Color_Chan.Discord.Rest.Models
{
    public class DiscordStageInstance : IDiscordStageInstance
    {
        public DiscordStageInstance(DiscordStageinstanceData dataStageInstances)
        {
            Id = dataStageInstances.Id;
            GuildId = dataStageInstances.GuildId;
            ChannelId = dataStageInstances.ChannelId;
            Topic = dataStageInstances.Topic;
            PrivacyLevel = dataStageInstances.PrivacyLevel;
            DiscoverableDisabled = dataStageInstances.DiscoverableDisabled;
        }

        /// <inheritdoc />
        public ulong Id { get; init; }

        /// <inheritdoc />
        public ulong GuildId { get; init; }

        /// <inheritdoc />
        public ulong ChannelId { get; init; }

        /// <inheritdoc />
        public string Topic { get; init; }

        /// <inheritdoc />
        public DiscordStagePrivacyLevel PrivacyLevel { get; init; }

        /// <inheritdoc />
        public bool DiscoverableDisabled { get; init; }
    }
}