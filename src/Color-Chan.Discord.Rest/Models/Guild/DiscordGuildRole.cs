using System.Drawing;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Core.Common.Models.Guild;

namespace Color_Chan.Discord.Rest.Models.Guild
{
    public record DiscordGuildRole : IDiscordGuildRole
    {
        public DiscordGuildRole(DiscordGuildRoleData data)
        {
            Id = data.Id;
            Name = data.Name;
            Color = data.Color;
            IsHoisted = data.IsHoisted;
            Position = data.Position;
            Permissions = data.Permissions;
            Managed = data.Managed;
            Mentionable = data.Mentionable;
        }

        /// <inheritdoc />
        public ulong Id { get; init; }

        /// <inheritdoc />
        public string Name { get; init; }

        /// <inheritdoc />
        public Color Color { get; init; }

        /// <inheritdoc />
        public bool IsHoisted { get; init; }

        /// <inheritdoc />
        public int Position { get; init; }

        /// <inheritdoc />
        public DiscordGuildPermission Permissions { get; init; }

        /// <inheritdoc />
        public bool Managed { get; init; }

        /// <inheritdoc />
        public bool Mentionable { get; init; }

        /// <inheritdoc />
        public DiscordGuildRoleData ToDataModel()
        {
            return new()
            {
                Color = Color,
                Id = Id,
                Managed = Managed,
                Mentionable = Mentionable,
                Name = Name,
                Permissions = Permissions,
                Position = Position,
                IsHoisted = IsHoisted
            };
        }
    }
}