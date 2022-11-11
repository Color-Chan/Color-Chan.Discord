using System;
using Color_Chan.Discord.Caching.Configurations;
using Color_Chan.Discord.Caching.Extensions;
using Color_Chan.Discord.Caching.Services;
using Color_Chan.Discord.Caching.Services.Implementations;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace Color_Chan.Discord.Caching.Tests.Extensions;

[TestFixture]
public class ServiceCollectionExtensionsTests
{
    public void SetUpDistributedCache()
    {
        _serviceProviderDistributed = new ServiceCollection()
                                      .AddColorChanCache(null, options => { options.Configuration = "connectionString"; })
                                      .BuildServiceProvider();
    }

    public void SetUpDefaultCache()
    {
        _serviceProviderDefault = new ServiceCollection()
                                  .AddColorChanCache()
                                  .BuildServiceProvider();
    }

    private IServiceProvider _serviceProviderDefault = null!;
    private IServiceProvider _serviceProviderDistributed = null!;

    [Test]
    public void Should_resolve_DefaultCacheConfig()
    {
        // Arrange 
        SetUpDefaultCache();

        // Act
        var cacheConfig = _serviceProviderDefault.GetService<IOptions<CacheConfiguration>>();

        // Assert
        cacheConfig.Should().NotBeNull();
        cacheConfig!.Value.Should().NotBeNull();
        cacheConfig.Value.AbsoluteExpiration.Should().Be(TimeSpan.FromSeconds(30));
        cacheConfig.Value.SlidingExpiration.Should().Be(TimeSpan.FromSeconds(15));
    }

    [Test]
    public void Should_resolve_LocalCacheService()
    {
        // Arrange 
        SetUpDefaultCache();

        // Act
        var application = _serviceProviderDefault.GetService<ICacheService>();

        // Assert
        application.Should().NotBeNull();
        application!.GetType().Should().Be<LocalCacheService>();
    }

    [Test]
    public void Should_resolve_DistributedCacheService()
    {
        // Arrange 
        SetUpDistributedCache();

        // Act
        var application = _serviceProviderDistributed.GetService<ICacheService>();

        // Assert
        application.Should().NotBeNull();
        application!.GetType().Should().Be<DistributedCacheService>();
    }

    [Test]
    public void Should_resolve_ITypeCacheConfigurationService()
    {
        // Arrange 
        SetUpDistributedCache();

        // Act
        var typeCache = _serviceProviderDefault.GetService<ITypeCacheConfigurationService>();

        // Assert
        typeCache.Should().NotBeNull();
    }
}