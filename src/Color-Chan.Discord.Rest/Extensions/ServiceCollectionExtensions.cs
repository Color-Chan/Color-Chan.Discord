using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Color_Chan.Discord.Core.Extensions;
using Color_Chan.Discord.Rest.API.Rest;
using Color_Chan.Discord.Rest.Policies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Polly;
using Polly.Contrib.WaitAndRetry;

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
        /// <returns>
        ///     The updated <see cref="IServiceCollection" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="botToken" /> is null.</exception>
        public static IServiceCollection AddColorChanDiscordRest(this IServiceCollection services, string botToken)
        {
            if (botToken == null) throw new ArgumentNullException(nameof(botToken));

            // See https://github.com/App-vNext/Polly/wiki/Retry-with-jitter for more info why jitter is used.
            var retryDelay = Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), 5);
            var customPolicy = new DiscordRateLimitPolicy(int.MaxValue, int.MaxValue);

            services.AddHttpClient("Discord", client =>
            {
                client.BaseAddress = Constants.DiscordApiUrl;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bot", botToken);
            }).AddTransientHttpErrorPolicy(policyBuilder => policyBuilder
                                                            .WaitAndRetryAsync(retryDelay)
                                                            .WrapAsync(customPolicy)
            ).AddPolicyHandler(Policy.HandleResult<HttpResponseMessage>(response => response.StatusCode == HttpStatusCode.TooManyRequests)
                                     .WaitAndRetryAsync(1, SleepDurationProvider, (_, _, _, _) => Task.CompletedTask));

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

        private static TimeSpan SleepDurationProvider(int retryCount, DelegateResult<HttpResponseMessage> response, Context context)
        {
            return response.Result?.Headers.RetryAfter?.Delta ?? TimeSpan.FromSeconds(1);
        }
    }
}