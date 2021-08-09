using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Color_Chan.Discord.Core.Common.API.DataModels.Guild;
using Color_Chan.Discord.Core.Extensions;

namespace Color_Chan.Discord.Core.Benchmarks
{
    [SimpleJob(RuntimeMoniker.Net50)]
    [MemoryDiagnoser]
    public class GuildPermissionExtensionsBenchmarks
    {
        [Benchmark]
        public DiscordPermission ConvertToGuildPermissionSpan()
        {
            var permString = "1409286155";
            var permSpan = permString.AsSpan();
            return permSpan.ConvertToGuildPermission();
        }

        [Benchmark]
        public DiscordPermission ConvertToGuildPermissionString()
        {
            var permString = "1409286155";
            return permString.ConvertToGuildPermission();
        }
    }
}