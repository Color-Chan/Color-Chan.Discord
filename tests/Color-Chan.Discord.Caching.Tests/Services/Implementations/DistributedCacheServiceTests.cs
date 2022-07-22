using System;
using System.Text.Json;
using Color_Chan.Discord.Caching.Configurations;
using Color_Chan.Discord.Caching.Services.Implementations;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace Color_Chan.Discord.Caching.Tests.Services.Implementations;

[TestFixture]
public class DistributedCacheServiceTests : CacheServiceTestBase<DistributedCacheService>
{
    [SetUp]
    public void SetUp()
    {
        var typeCache = new TypeCacheConfigurationService(new OptionsWrapper<CacheConfiguration>(new CacheConfiguration
        {
            AbsoluteExpiration = TimeSpan.FromSeconds(30),
            SlidingExpiration = TimeSpan.FromSeconds(15)
        }));

        var serviceProvider = new ServiceCollection()
                              .AddDistributedMemoryCache()
                              .BuildServiceProvider();

        var memCache = serviceProvider.GetRequiredService<IDistributedCache>();
        var jsonOptions = serviceProvider.GetRequiredService<IOptions<JsonSerializerOptions>>();
        CacheService = new DistributedCacheService(memCache, jsonOptions, typeCache);
    }
}