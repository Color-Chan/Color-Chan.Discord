using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Color_Chan.Discord.Caching.Configurations;
using Color_Chan.Discord.Caching.Extensions;
using Color_Chan.Discord.Core.Extensions;
using Color_Chan.Discord.Rest.API.Rest;
using Color_Chan.Discord.Rest.Policies;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Extensions.Http;

namespace Color_Chan.Discord.Rest.Extensions
{
    /// <summary>
    ///     Contains all the extensions methods for <see cref="IServiceCollection" />.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Add the dependencies for Color-Chan.Discord.Rest to the <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" />.</param>
        /// <param name="botToken">
        ///     The bot token of your application.
        ///     This can be found at https://discord.com/developers/applications/APPLICATION_ID/bot
        /// </param>
        /// <param name="defaultCacheConfig">
        ///     The default cache configurations.
        ///     Leave this null to use the default expiration values.
        /// </param>
        /// <param name="redisCacheOptions">
        ///     The cache options for the redis cache.
        ///     Leave this null if you want to use a local cache.
        /// </param>
        /// <param name="discordBaseUriOverwrite">
        ///     The base <see cref="Uri"/> that will be used with the Discord HTTP client.
        ///     Example: http://localhost:3000/api/v9/
        ///     <remarks>
        ///         This can be useful if you want to use something like https://github.com/twilight-rs/http-proxy.
        ///     </remarks>
        /// </param>
        /// <returns>
        ///     The updated <see cref="IServiceCollection" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="botToken" /> is null.</exception>
        public static IServiceCollection AddColorChanDiscordRest(this IServiceCollection services, string botToken,
                                                                 Action<CacheConfiguration>? defaultCacheConfig = null,
                                                                 Action<RedisCacheOptions>? redisCacheOptions = null,
                                                                 Uri? discordBaseUriOverwrite = null)
        {
            if (botToken == null) throw new ArgumentNullException(nameof(botToken));

            // See https://github.com/App-vNext/Polly/wiki/Retry-with-jitter for more info why jitter is used.
            var retryDelay = Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), 5);

            services.AddSingleton<DiscordRateLimitPolicy>();
            services.AddHttpClient("Discord", client =>
            {
                client.BaseAddress = discordBaseUriOverwrite ?? Constants.DiscordApiUrl;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bot", botToken);
            }).AddPolicyHandler((provider, _) => HttpPolicyExtensions.HandleTransientHttpError()
                                                                     .WaitAndRetryAsync(retryDelay)
                                                                     .WrapAsync(provider.GetRequiredService<DiscordRateLimitPolicy>())
            ).AddPolicyHandler(Policy.HandleResult<HttpResponseMessage>(response => response.StatusCode == HttpStatusCode.TooManyRequests)
                                     .WaitAndRetryAsync(1, (_, response, _) => response.Result?.Headers.RetryAfter?.Delta ?? TimeSpan.FromMilliseconds(2500), (_, _, _, _) => Task.CompletedTask));

            // Add all rest classes with Transient live cycle that inherit DiscordRestBase.
            services.Scan(scan =>
            {
                scan.FromAssemblyOf<DiscordRestBase>()
                    .AddClasses(filter => filter.AssignableTo<DiscordRestBase>())
                    .AsImplementedInterfaces()
                    .WithTransientLifetime();
            });

            services.TryAddTransient<IDiscordHttpClient, DiscordHttpClient>();
            services.AddColorChanDiscordCore();
            services.AddColorChanCache(defaultCacheConfig, redisCacheOptions);

            return services;
        }
    }
}