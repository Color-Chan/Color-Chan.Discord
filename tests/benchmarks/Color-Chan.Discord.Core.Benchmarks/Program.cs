using BenchmarkDotNet.Running;

namespace Color_Chan.Discord.Core.Benchmarks;

public class Program
{
    public static void Main()
    {
        BenchmarkGuildPermissionExtensions();
    }

    private static void BenchmarkGuildPermissionExtensions()
    {
        BenchmarkRunner.Run<GuildPermissionExtensionsBenchmarks>();
    }
}