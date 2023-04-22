using System.Collections.Generic;
using Color_Chan.Discord.Core.Common.API.DataModels;
using Color_Chan.Discord.Core.Common.API.DataModels.Application;
using Color_Chan.Discord.Core.Common.API.DataModels.Entitlement;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Core.Common.API.DataModels.Interaction;
using Color_Chan.Discord.Core.Common.API.DataModels.Message;
using Color_Chan.Discord.Core.Common.Models;
using Color_Chan.Discord.Core.Common.Models.Application;
using Color_Chan.Discord.Core.Common.Models.Entitlement;
using Color_Chan.Discord.Core.Common.Models.Guild;
using Color_Chan.Discord.Core.Common.Models.Interaction;
using Color_Chan.Discord.Core.Common.Models.Message;
using Color_Chan.Discord.Core.Results;
using Color_Chan.Discord.Rest.Models;
using Color_Chan.Discord.Rest.Models.Application;
using Color_Chan.Discord.Rest.Models.Entitlement;
using Color_Chan.Discord.Rest.Models.Guild;
using Color_Chan.Discord.Rest.Models.Interaction;
using Color_Chan.Discord.Rest.Models.Message;

namespace Color_Chan.Discord.Rest.API;

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

    internal static Result<IDiscordInteractionResponse> ConvertResult(Result<DiscordInteractionResponseData> result)
    {
        if (!result.IsSuccessful || result.Entity is null) return Result<IDiscordInteractionResponse>.FromError(null, result.ErrorResult);

        return Result<IDiscordInteractionResponse>.FromSuccess(new DiscordInteractionResponse(result.Entity));
    }

    internal static Result<IDiscordMessage> ConvertResult(Result<DiscordMessageData> result)
    {
        if (!result.IsSuccessful || result.Entity is null) return Result<IDiscordMessage>.FromError(null, result.ErrorResult);

        return Result<IDiscordMessage>.FromSuccess(new DiscordMessage(result.Entity));
    }

    internal static Result<IReadOnlyList<IDiscordMessage>> ConvertResult(Result<IReadOnlyList<DiscordMessageData>> result)
    {
        if (!result.IsSuccessful || result.Entity is null) return Result<IReadOnlyList<IDiscordMessage>>.FromError(null, result.ErrorResult);

        var list = new List<IDiscordMessage>();
        foreach (var data in result.Entity) list.Add(new DiscordMessage(data));

        return Result<IReadOnlyList<IDiscordMessage>>.FromSuccess(list);
    }

    internal static Result<IDiscordUser> ConvertResult(Result<DiscordUserData> result)
    {
        if (!result.IsSuccessful || result.Entity is null) return Result<IDiscordUser>.FromError(null, result.ErrorResult);

        return Result<IDiscordUser>.FromSuccess(new DiscordUser(result.Entity));
    }

    internal static Result<IDiscordGuild> ConvertResult(Result<DiscordGuildData> result)
    {
        if (!result.IsSuccessful || result.Entity is null) return Result<IDiscordGuild>.FromError(null, result.ErrorResult);

        return Result<IDiscordGuild>.FromSuccess(new DiscordGuild(result.Entity));
    }

    internal static Result<IDiscordGuildPreview> ConvertResult(Result<DiscordGuildPreviewData> result)
    {
        if (!result.IsSuccessful || result.Entity is null) return Result<IDiscordGuildPreview>.FromError(null, result.ErrorResult);

        return Result<IDiscordGuildPreview>.FromSuccess(new DiscordGuildPreview(result.Entity));
    }

    internal static Result<IDiscordChannel> ConvertResult(Result<DiscordChannelData> result)
    {
        if (!result.IsSuccessful || result.Entity is null) return Result<IDiscordChannel>.FromError(null, result.ErrorResult);

        return Result<IDiscordChannel>.FromSuccess(new DiscordChannel(result.Entity));
    }

    internal static Result<IReadOnlyList<IDiscordChannel>> ConvertResult(Result<IReadOnlyList<DiscordChannelData>> result)
    {
        if (!result.IsSuccessful || result.Entity is null) return Result<IReadOnlyList<IDiscordChannel>>.FromError(null, result.ErrorResult);

        var roles = new List<IDiscordChannel>();
        foreach (var data in result.Entity) roles.Add(new DiscordChannel(data));

        return Result<IReadOnlyList<IDiscordChannel>>.FromSuccess(roles);
    }

    internal static Result<IDiscordGuildRole> ConvertResult(Result<DiscordGuildRoleData> result)
    {
        if (!result.IsSuccessful || result.Entity is null) return Result<IDiscordGuildRole>.FromError(null, result.ErrorResult);

        return Result<IDiscordGuildRole>.FromSuccess(new DiscordGuildRole(result.Entity));
    }

    internal static Result<IReadOnlyList<IDiscordGuildRole>> ConvertResult(Result<IReadOnlyList<DiscordGuildRoleData>> result)
    {
        if (!result.IsSuccessful || result.Entity is null) return Result<IReadOnlyList<IDiscordGuildRole>>.FromError(null, result.ErrorResult);

        var roles = new List<IDiscordGuildRole>();
        foreach (var roleData in result.Entity) roles.Add(new DiscordGuildRole(roleData));

        return Result<IReadOnlyList<IDiscordGuildRole>>.FromSuccess(roles);
    }

    internal static Result<IDiscordGuildMember> ConvertResult(Result<DiscordGuildMemberData> result)
    {
        if (!result.IsSuccessful || result.Entity is null) return Result<IDiscordGuildMember>.FromError(null, result.ErrorResult);

        return Result<IDiscordGuildMember>.FromSuccess(new DiscordGuildMember(result.Entity));
    }

    internal static Result<IReadOnlyList<IDiscordGuildMember>> ConvertResult(Result<IReadOnlyList<DiscordGuildMemberData>> result)
    {
        if (!result.IsSuccessful || result.Entity is null) return Result<IReadOnlyList<IDiscordGuildMember>>.FromError(null, result.ErrorResult);

        var list = new List<IDiscordGuildMember>();
        foreach (var data in result.Entity) list.Add(new DiscordGuildMember(data));

        return Result<IReadOnlyList<IDiscordGuildMember>>.FromSuccess(list);
    }

    internal static Result<IDiscordPartialGuild> ConvertResult(Result<DiscordPartialGuildData> result)
    {
        if (!result.IsSuccessful || result.Entity is null) return Result<IDiscordPartialGuild>.FromError(null, result.ErrorResult);

        return Result<IDiscordPartialGuild>.FromSuccess(new DiscordPartialGuild(result.Entity));
    }

    internal static Result<IReadOnlyList<IDiscordPartialGuild>> ConvertResult(Result<IReadOnlyList<DiscordPartialGuildData>> result)
    {
        if (!result.IsSuccessful || result.Entity is null) return Result<IReadOnlyList<IDiscordPartialGuild>>.FromError(null, result.ErrorResult);

        var list = new List<IDiscordPartialGuild>();
        foreach (var data in result.Entity) list.Add(new DiscordPartialGuild(data));

        return Result<IReadOnlyList<IDiscordPartialGuild>>.FromSuccess(list);
    }

    internal static Result<IDiscordConnection> ConvertResult(Result<DiscordConnectionData> result)
    {
        if (!result.IsSuccessful || result.Entity is null) return Result<IDiscordConnection>.FromError(null, result.ErrorResult);

        return Result<IDiscordConnection>.FromSuccess(new DiscordConnection(result.Entity));
    }

    internal static Result<IReadOnlyList<IDiscordConnection>> ConvertResult(Result<IReadOnlyList<DiscordConnectionData>> result)
    {
        if (!result.IsSuccessful || result.Entity is null) return Result<IReadOnlyList<IDiscordConnection>>.FromError(null, result.ErrorResult);

        var list = new List<IDiscordConnection>();
        foreach (var data in result.Entity) list.Add(new DiscordConnection(data));

        return Result<IReadOnlyList<IDiscordConnection>>.FromSuccess(list);
    }

    internal static Result<IDiscordEntitlement> ConvertResult(Result<DiscordEntitlementData> result)
    {
        if (!result.IsSuccessful || result.Entity is null) return Result<IDiscordEntitlement>.FromError(null, result.ErrorResult);

        return Result<IDiscordEntitlement>.FromSuccess(new DiscordEntitlement(result.Entity));
    }

    internal static Result<IReadOnlyList<IDiscordEntitlement>> ConvertResult(Result<IReadOnlyList<DiscordEntitlementData>> result)
    {
        if (!result.IsSuccessful || result.Entity is null) return Result<IReadOnlyList<IDiscordEntitlement>>.FromError(null, result.ErrorResult);

        var list = new List<IDiscordEntitlement>();
        foreach (var data in result.Entity) list.Add(new DiscordEntitlement(data));

        return Result<IReadOnlyList<IDiscordEntitlement>>.FromSuccess(list);
    }
}