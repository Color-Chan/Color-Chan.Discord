using System;
using System.Net.Http.Headers;
using Color_Chan.Discord.Core.Extensions;
using Color_Chan.Discord.Rest.API.Rest;
using Color_Chan.Discord.Rest.Policies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Polly;
using Polly.Contrib.WaitAndRetry;

namespace Color_Chan.Discord.Rest.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Add the dependencies for Color-Chan.Discord.Rest to the <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" />.</param>
        /// <param name="token">The Discord Bot token.</param>
        /// <returns>
        ///     The updated <see cref="IServiceCollection" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="token" /> is null.</exception>
        public static IServiceCollection AddColorChanDiscordRest(this IServiceCollection services, string token)
        {
            if (token == null) throw new ArgumentNullException(nameof(token));

            // See https://github.com/App-vNext/Polly/wiki/Retry-with-jitter for more info why jitter is used.
            var retryDelay = Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), 5);
            var customPolicy = new DiscordRateLimitPolicy(int.MaxValue, int.MaxValue);

            services.AddHttpClient("Discord", client =>
            {
                client.BaseAddress = Constants.DiscordApiUrl;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bot", token);
            }).AddTransientHttpErrorPolicy(policyBuilder => policyBuilder
                .WaitAndRetryAsync(retryDelay)
                .WrapAsync(customPolicy)
            );

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

            return services;
        }
    }
}