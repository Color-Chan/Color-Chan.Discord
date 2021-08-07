using System;
using Color_Chan.Discord.Caching.Configurations;
using Color_Chan.Discord.Caching.Services.Implementations;
using FluentAssertions;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace Color_Chan.Discord.Caching.Tests.Services.Implementations
{
    [TestFixture]
    public class TypeCacheConfigurationServiceTests
    {
        [Test]
        public void Should_add_cache_config()
        {
            // Arrange
            var typeCache = new TypeCacheConfigurationService(new OptionsWrapper<CacheConfiguration>(new CacheConfiguration()));
            
            // Act & Assert
            Assert.DoesNotThrow(() => typeCache.AddCacheConfig<string>(TimeSpan.FromDays(1), TimeSpan.FromDays(2)));
        }
        
        [TestCase(2, 1)]
        [TestCase(int.MaxValue,123123)]
        public void Should_add_and_get_cacheConfig(int absoluteSeconds, int slidingSeconds)
        {
            // Arrange
            var typeCache = new TypeCacheConfigurationService(new OptionsWrapper<CacheConfiguration>(new CacheConfiguration()));
            
            // Act
            typeCache.AddCacheConfig<string>(TimeSpan.FromSeconds(slidingSeconds), TimeSpan.FromSeconds(absoluteSeconds));
            var result = typeCache.GetCacheConfig<string>();
            
            // Assert
            result.SlidingExpiration.TotalSeconds.Should().Be(slidingSeconds);
            result.AbsoluteExpiration.TotalSeconds.Should().Be(absoluteSeconds);
        }
        
        [Test]
        public void Should_throw_ArgumentOutOfRangeException_on_add_cache_config()
        {
            // Arrange
            var typeCache = new TypeCacheConfigurationService(new OptionsWrapper<CacheConfiguration>(new CacheConfiguration()));
            
            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => typeCache.AddCacheConfig<string>(TimeSpan.FromDays(1), TimeSpan.FromHours(1)));
        }
    }
}