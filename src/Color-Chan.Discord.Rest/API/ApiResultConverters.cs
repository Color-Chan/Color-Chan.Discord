using System.Collections.Generic;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Common.Models.Application;
using Color_Chan.Discord.Core.Common.Models.Guild;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Results;
using Color_Chan.Discord.Rest.Models.Application;
using Color_Chan.Discord.Rest.Models.Guild;
using Color_Chan.Discord.Rest.Models.Interaction;

namespace Color_Chan.Discord.Rest.API
{
    internal static class ApiResultConverters
    {
        internal static Result<IDiscordApplicationCommand> ConvertResult(Result<DiscordApplicationCommandData> result)
        {
            if (!result.IsSuccessful || result.Entity is null) return Result<IDiscordApplicationCommand>.FromError(null, result.ErrorResult);

            return Result<IDiscordApplicationCommand>.FromSuccess(new DiscordApplicationCommand(result.Entity));
        }

        internal static Result<IReadOnlyList<IDiscordApplicationCommand>> ConvertResult(Result<IReadOnlyList<DiscordApplicationCommandData>> result)
        {
            if (!result.IsSuccessful || result.Entity is null) return Result<IReadOnlyList<IDiscordApplicationCommand>>.FromError(null, result.ErrorResult);

            var roles = new List<IDiscordApplicationCommand>();
            foreach (var roleData in result.Entity) roles.Add(new DiscordApplicationCommand(roleData));

            return Result<IReadOnlyList<IDiscordApplicationCommand>>.FromSuccess(roles);
        }

        internal static Result<IDiscordGuildApplicationCommandPermissions> ConvertResult(Result<DiscordGuildApplicationCommandPermissionsData> result)
        {
            if (!result.IsSuccessful || result.Entity is null) return Result<IDiscordGuildApplicationCommandPermissions>.FromError(null, result.ErrorResult);

            return Result<IDiscordGuildApplicationCommandPermissions>.FromSuccess(new DiscordGuildApplicationCommandPermissions(result.Entity));
        }

        internal static Result<IReadOnlyList<IDiscordGuildApplicationCommandPermissions>> ConvertResult(Result<IReadOnlyList<DiscordGuildApplicationCommandPermissionsData>> result)
        {
            if (!result.IsSuccessful || result.Entity is null) return Result<IReadOnlyList<IDiscordGuildApplicationCommandPermissions>>.FromError(null, result.ErrorResult);

            var roles = new List<IDiscordGuildApplicationCommandPermissions>();
            foreach (var roleData in result.Entity) roles.Add(new DiscordGuildApplicationCommandPermissions(roleData));

            return Result<IReadOnlyList<IDiscordGuildApplicationCommandPermissions>>.FromSuccess(roles);
        }

        public static Result<IDiscordInteractionResponse> ConvertResult(Result<DiscordInteractionResponseData> result)
        {
            if (!result.IsSuccessful || result.Entity is null) return Result<IDiscordInteractionResponse>.FromError(null, result.ErrorResult);

            return Result<IDiscordInteractionResponse>.FromSuccess(new DiscordInteractionResponse(result.Entity));
        }
    }
}