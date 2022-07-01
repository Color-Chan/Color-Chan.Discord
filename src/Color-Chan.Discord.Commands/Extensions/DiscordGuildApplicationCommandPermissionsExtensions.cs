using System.Collections.Generic;
using System.Linq;
using Color_Chan.Discord.Core.Common.API.Params.Application;
using Color_Chan.Discord.Core.Common.Models.Guild;

namespace Color_Chan.Discord.Commands.Extensions;

internal static class DiscordGuildApplicationCommandPermissionsExtensions
{
    internal static bool ShouldUpdatePermissions(this List<DiscordBatchEditApplicationCommandPermissions> localCommandPerms, IReadOnlyList<IDiscordGuildApplicationCommandPermissions> existingPerms)
    {
        foreach (var localCommandPerm in localCommandPerms)
        {
            var existingCommandPerm = existingPerms.FirstOrDefault(x => x.CommandId.Equals(localCommandPerm.CommandId));

            if (existingCommandPerm is null)
            {
                // New command perms found.
                return true;
            }

            // Found existing perm.

            if (ContainsNewOrUpdatedPerm(localCommandPerm, existingCommandPerm))
            {
                return true;
            }
        }

        foreach (var existingPerm in existingPerms)
        {
            var localCommandPerm = localCommandPerms.FirstOrDefault(x => x.CommandId.Equals(existingPerm.CommandId));

            if (localCommandPerm is null)
            {
                // Deleted command perms found.
                return true;
            }
        }

        return false;
    }

    private static bool ContainsNewOrUpdatedPerm(DiscordBatchEditApplicationCommandPermissions localCommandPerm, IDiscordGuildApplicationCommandPermissions existingCommandPerm)
    {
        if (localCommandPerm.Permissions.Count() != existingCommandPerm.Permissions.Count())
        {
            return true;
        }

        foreach (var localPerm in localCommandPerm.Permissions)
        {
            var existingPerm = existingCommandPerm.Permissions.FirstOrDefault(x => x.Id == localPerm.Id);

            if (existingPerm is null)
            {
                return true;
            }

            if (localPerm.Type != existingPerm.Type)
            {
                return true;
            }

            if (localPerm.Allow != existingPerm.Allow)
            {
                return true;
            }
        }

        foreach (var existingPerm in existingCommandPerm.Permissions)
        {
            var localPerm = localCommandPerm.Permissions.FirstOrDefault(x => x.Id == existingPerm.Id);

            if (localPerm is null)
            {
                return true;
            }

            if (existingPerm.Type != localPerm.Type)
            {
                return true;
            }

            if (existingPerm.Allow != localPerm.Allow)
            {
                return true;
            }
        }

        return false;
    }
}