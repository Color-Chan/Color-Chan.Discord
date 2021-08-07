using System;
using Color_Chan.Discord.Caching.Configurations;
using Color_Chan.Discord.Caching.Services.Implementations;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace Color_Chan.Discord.Caching.Tests.Services.Implementations
{
    [TestFixture]
    public class LocalCacheServiceTests : CacheServiceTestBase<LocalCacheService>
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
                .AddMemoryCache()
                .BuildServiceProvider();

            var memCache = serviceProvider.GetRequiredService<IMemoryCache>();
            CacheService = new LocalCacheService(memCache, typeCache);
        }
    }
}