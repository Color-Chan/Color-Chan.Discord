using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Color_Chan.Discord.Caching.Configurations;
using Color_Chan.Discord.Caching.Services;
using FluentAssertions;
using NUnit.Framework;

namespace Color_Chan.Discord.Caching.Tests.Services.Implementations
{
    [Parallelizable]
    public class CacheServiceTestBase<TServiceType> where TServiceType : class, ICacheService
    {
        protected TServiceType CacheService = null!;

        [TestCaseSource(nameof(GetKeyValuePairs))]
        public void CacheValue_should_cache_value(KeyValuePair<string, object> keyValuePair)
        {
            Assert.DoesNotThrow(() => CacheService.CacheValue(keyValuePair.Key, keyValuePair.Value));
        }

        [TestCaseSource(nameof(GetKeyValuePairs))]
        public void CacheValue_with_overwrites_should_cache_value(KeyValuePair<string, object> keyValuePair)
        {
            Assert.DoesNotThrow(() => CacheService.CacheValue(keyValuePair.Key, keyValuePair.Value, TimeSpan.FromSeconds(20), TimeSpan.FromSeconds(40)));
        }

        [TestCaseSource(nameof(GetKeyValuePairs))]
        public void CacheValueAsync_should_cache_value(KeyValuePair<string, object> keyValuePair)
        {
            Assert.DoesNotThrowAsync(() => CacheService.CacheValueAsync(keyValuePair.Key, keyValuePair.Value));
        }

        [TestCaseSource(nameof(GetKeyValuePairs))]
        public void CacheValueAsync_with_overwrites_should_cache_value(KeyValuePair<string, object> keyValuePair)
        {
            Assert.DoesNotThrowAsync(() => CacheService.CacheValueAsync(keyValuePair.Key, keyValuePair.Value, TimeSpan.FromSeconds(20), TimeSpan.FromSeconds(40)));
        }

        [TestCaseSource(nameof(GetKeyValuePairs))]
        public async Task CacheValue_with_absoluteExpiration_should_expire(KeyValuePair<string, object> keyValuePair)
        {
            // Arrange
            Assert.DoesNotThrow(() => CacheService.CacheValue(keyValuePair.Key, keyValuePair.Value, null, DateTimeOffset.Now.AddMilliseconds(200)));

            // Act
            await Task.Delay(100);
            var nonExpiredResult = await CacheService.GetValueAsync<object>(keyValuePair.Key);

            await Task.Delay(125); // let it expire.
            var expiredResult = await CacheService.GetValueAsync<object>(keyValuePair.Key);

            // Assert
            nonExpiredResult.IsSuccessful.Should().BeTrue();
            AssertObjectValue(nonExpiredResult.Entity!, keyValuePair.Value);

            expiredResult.IsSuccessful.Should().BeFalse();
        }

        [TestCaseSource(nameof(GetKeyValuePairs))]
        public async Task CacheValue_with_slidingExpirationOverwrite_should_expire(KeyValuePair<string, object> keyValuePair)
        {
            // Arrange
            Assert.DoesNotThrow(() => CacheService.CacheValue(keyValuePair.Key, keyValuePair.Value, TimeSpan.FromMilliseconds(100), DateTimeOffset.Now.AddDays(1)));

            // Act
            await Task.Delay(75);
            var nonExpiredResult = await CacheService.GetValueAsync<object>(keyValuePair.Key);

            await Task.Delay(125); // let it expire.
            var expiredResult = await CacheService.GetValueAsync<object>(keyValuePair.Key);

            // Assert
            nonExpiredResult.IsSuccessful.Should().BeTrue();
            AssertObjectValue(nonExpiredResult.Entity!, keyValuePair.Value);

            expiredResult.IsSuccessful.Should().BeFalse();
        }

        [TestCaseSource(nameof(GetKeyValuePairs))]
        public async Task CacheValue_with_absoluteExpirationRelativeToNow_should_expire(KeyValuePair<string, object> keyValuePair)
        {
            // Arrange
            Assert.DoesNotThrow(() => CacheService.CacheValue(keyValuePair.Key, keyValuePair.Value, null, TimeSpan.FromMilliseconds(200)));

            // Act
            await Task.Delay(100);
            var nonExpiredResult = await CacheService.GetValueAsync<object>(keyValuePair.Key);

            await Task.Delay(125); // let it expire.
            var expiredResult = await CacheService.GetValueAsync<object>(keyValuePair.Key);

            // Assert
            nonExpiredResult.IsSuccessful.Should().BeTrue();
            AssertObjectValue(nonExpiredResult.Entity!, keyValuePair.Value);

            expiredResult.IsSuccessful.Should().BeFalse();
        }

        [TestCaseSource(nameof(GetKeyValuePairs))]
        public async Task CacheValueAsync_with_absoluteExpiration_should_expire(KeyValuePair<string, object> keyValuePair)
        {
            // Arrange
            Assert.DoesNotThrowAsync(() => CacheService.CacheValueAsync(keyValuePair.Key, keyValuePair.Value, null, DateTimeOffset.Now.AddMilliseconds(200)));

            // Act
            await Task.Delay(100);
            var nonExpiredResult = await CacheService.GetValueAsync<object>(keyValuePair.Key);

            await Task.Delay(125); // let it expire.
            var expiredResult = await CacheService.GetValueAsync<object>(keyValuePair.Key);

            // Assert
            nonExpiredResult.IsSuccessful.Should().BeTrue();
            AssertObjectValue(nonExpiredResult.Entity!, keyValuePair.Value);

            expiredResult.IsSuccessful.Should().BeFalse();
        }

        [TestCaseSource(nameof(GetKeyValuePairs))]
        public async Task CacheValueAsync_with_slidingExpirationOverwrite_should_expire(KeyValuePair<string, object> keyValuePair)
        {
            // Arrange
            Assert.DoesNotThrowAsync(() => CacheService.CacheValueAsync(keyValuePair.Key, keyValuePair.Value, TimeSpan.FromMilliseconds(100), DateTimeOffset.Now.AddDays(1)));

            // Act
            await Task.Delay(75);
            var nonExpiredResult = await CacheService.GetValueAsync<object>(keyValuePair.Key);

            await Task.Delay(125); // let it expire.
            var expiredResult = await CacheService.GetValueAsync<object>(keyValuePair.Key);

            // Assert
            nonExpiredResult.IsSuccessful.Should().BeTrue();
            AssertObjectValue(nonExpiredResult.Entity!, keyValuePair.Value);

            expiredResult.IsSuccessful.Should().BeFalse();
        }

        [TestCaseSource(nameof(GetKeyValuePairs))]
        public async Task CacheValueAsync_with_absoluteExpirationRelativeToNow_should_expire(KeyValuePair<string, object> keyValuePair)
        {
            // Arrange
            Assert.DoesNotThrowAsync(() => CacheService.CacheValueAsync(keyValuePair.Key, keyValuePair.Value, null, TimeSpan.FromMilliseconds(200)));

            // Act
            await Task.Delay(100);
            var nonExpiredResult = await CacheService.GetValueAsync<object>(keyValuePair.Key);

            await Task.Delay(125); // let it expire.
            var expiredResult = await CacheService.GetValueAsync<object>(keyValuePair.Key);

            // Assert
            nonExpiredResult.IsSuccessful.Should().BeTrue();
            AssertObjectValue(nonExpiredResult.Entity!, keyValuePair.Value);

            expiredResult.IsSuccessful.Should().BeFalse();
        }

        [TestCaseSource(nameof(GetKeyValuePairs))]
        public async Task CacheValue_should_cache_and_get_value(KeyValuePair<string, object> keyValuePair)
        {
            // Arrange
            CacheValue_should_cache_value(keyValuePair);

            // Act
            var result = await CacheService.GetValueAsync<object>(keyValuePair.Key);

            // Assert
            result.IsSuccessful.Should().BeTrue();
            AssertObjectValue(result.Entity!, keyValuePair.Value);
        }

        [TestCaseSource(nameof(GetKeyValuePairs))]
        public async Task CacheValue_with_overwrites_should_cache_and_get_value(KeyValuePair<string, object> keyValuePair)
        {
            // Arrange
            CacheValue_with_overwrites_should_cache_value(keyValuePair);

            // Act
            var result = await CacheService.GetValueAsync<object>(keyValuePair.Key);

            // Assert
            result.IsSuccessful.Should().BeTrue();
            AssertObjectValue(result.Entity!, keyValuePair.Value);
        }

        [TestCaseSource(nameof(GetKeyValuePairs))]
        public async Task CacheValueAsync_should_cache_and_get_value(KeyValuePair<string, object> keyValuePair)
        {
            // Arrange
            CacheValueAsync_should_cache_value(keyValuePair);

            // Act
            var result = await CacheService.GetValueAsync<object>(keyValuePair.Key);

            // Assert
            result.IsSuccessful.Should().BeTrue();
            AssertObjectValue(result.Entity!, keyValuePair.Value);
        }

        [TestCaseSource(nameof(GetKeyValuePairs))]
        public async Task CacheValueAsync_with_overwrites_should_cache_and_get_value(KeyValuePair<string, object> keyValuePair)
        {
            // Arrange
            CacheValueAsync_with_overwrites_should_cache_value(keyValuePair);

            // Act
            var result = await CacheService.GetValueAsync<object>(keyValuePair.Key);

            // Assert
            result.IsSuccessful.Should().BeTrue();
            AssertObjectValue(result.Entity!, keyValuePair.Value);
        }

        [TestCaseSource(nameof(GetKeyValuePairs))]
        public async Task CacheValueAsync_should_cache_and_get_and_remove_value(KeyValuePair<string, object> keyValuePair)
        {
            // Arrange
            await CacheValueAsync_should_cache_and_get_value(keyValuePair);

            // Act & Assert
            Assert.DoesNotThrowAsync(() => CacheService.RemoveValueAsync(keyValuePair.Key));
        }

        [Test]
        public async Task GetValueAsync_should_not_get_value()
        {
            // Act
            var result = await CacheService.GetValueAsync<object>("this key does not exist");

            // Assert
            result.IsSuccessful.Should().BeFalse();
        }

        [Test]
        public async Task GetOrCreateValueAsync_not_should_get_cached_value()
        {
            // Arrange
            const int value = 420;

            async Task<object> GetValueAsync()
            {
                await Task.Delay(1);
                return value;
            }

            // Act
            var result = await CacheService.GetOrCreateValueAsync("this key does not exist", GetValueAsync);

            // Assert
            result.IsSuccessful.Should().BeTrue();
            AssertObjectValue(result.Entity!, value);
        }

        [TestCaseSource(nameof(GetKeyValuePairs))]
        public async Task GetOrCreateValueAsync_should_get_cached_value(KeyValuePair<string, object> keyValuePair)
        {
            // Arrange
            await CacheValueAsync_should_cache_and_get_value(keyValuePair);

            Task<object> GetValueAsync()
            {
                throw new Exception("Value was not cached");
            }

            // Act
            var result = await CacheService.GetOrCreateValueAsync(keyValuePair.Key, GetValueAsync);

            // Assert
            result.IsSuccessful.Should().BeTrue();
            AssertObjectValue(result.Entity!, keyValuePair.Value);
        }

        private static void AssertObjectValue(object value, object expectedValue)
        {
            switch (value)
            {
                case int intValue:
                    intValue.Should().BeOfType(expectedValue.GetType());
                    intValue.Should().Be((int)expectedValue);
                    break;
                case string stringValue:
                    stringValue.Should().BeOfType(expectedValue.GetType());
                    stringValue.Should().Be((string)expectedValue);
                    break;
                case CacheConfiguration cacheConfigValue:
                    cacheConfigValue.Should().BeOfType(expectedValue.GetType());
                    cacheConfigValue.Should().Be((CacheConfiguration)expectedValue);
                    break;
                case DateTime dateTimeValue:
                    dateTimeValue.Should().Be((DateTime)expectedValue);
                    break;
                case TimeSpan timeSpanValue:
                    timeSpanValue.Should().Be((TimeSpan)expectedValue);
                    break;
                case DateTimeOffset dateTimeOffsetValue:
                    dateTimeOffsetValue.Should().Be((DateTimeOffset)expectedValue);
                    break;
            }
        }

        protected static IEnumerable<KeyValuePair<string, object>> GetKeyValuePairs()
        {
            var keyValueParis = new List<KeyValuePair<string, object>>
            {
                new(Guid.NewGuid().ToString(), int.MaxValue),
                new(Guid.NewGuid().ToString(), "test string value"),
                new(Guid.NewGuid().ToString(), new CacheConfiguration()),
                new(Guid.NewGuid().ToString(), DateTime.UtcNow),
                new(Guid.NewGuid().ToString(), TimeSpan.FromDays(1)),
                new(Guid.NewGuid().ToString(), DateTimeOffset.UtcNow)
            };

            foreach (var keyValuePair in keyValueParis)
            {
                yield return keyValuePair;
            }
        }
    }
}