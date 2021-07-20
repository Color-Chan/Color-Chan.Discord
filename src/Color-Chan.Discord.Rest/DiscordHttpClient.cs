using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Color_Chan.Discord.Core.Common.API.DataModels.Errors;
using Color_Chan.Discord.Core.Results;
using Color_Chan.Discord.Rest.Results;
using Microsoft.Extensions.Options;

namespace Color_Chan.Discord.Rest
{
    public class DiscordHttpClient : IDiscordHttpClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOptions<JsonSerializerOptions> _serializerOptions;

        /// <summary>
        ///     Initializes a new instance of <see cref="DiscordHttpClient" />.
        /// </summary>
        /// <param name="httpClientFactory">The <see cref="IHttpClientFactory" /> that will generate <see cref="HttpClient" />s.</param>
        /// <param name="serializerOptions">The options that will be used for serialization.</param>
        public DiscordHttpClient(IHttpClientFactory httpClientFactory, IOptions<JsonSerializerOptions> serializerOptions)
        {
            _httpClientFactory = httpClientFactory;
            _serializerOptions = serializerOptions;
        }

        /// <inheritdoc />
        public async Task<Result<TEntity>> GetAsync<TEntity>(string endpoint, IEnumerable<KeyValuePair<string, string>>? queries = null, CancellationToken ct = default) where TEntity : notnull
        {
            var requestBuilder = new HttpRequestMessageBuilder(endpoint)
                .WithQueryParameters(queries)
                .WithMethod(HttpMethod.Get);

            return await SendRequestAsync<TEntity>(requestBuilder, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<Result<TEntity>> PostAsync<TEntity, TBody>(string endpoint, TBody body, CancellationToken ct = default) where TEntity : notnull where TBody : notnull
        {
            var requestBuilder = new HttpRequestMessageBuilder(endpoint)
                .WithMethod(HttpMethod.Post)
                .WithBody(body);

            return await SendRequestAsync<TEntity>(requestBuilder, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<Result> PostAsync<TBody>(string endpoint, TBody body, CancellationToken ct = default) where TBody : notnull
        {
            var requestBuilder = new HttpRequestMessageBuilder(endpoint)
                .WithMethod(HttpMethod.Post)
                .WithBody(body);

            return await SendRequestAsync(requestBuilder, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<Result<TEntity>> PatchAsync<TEntity, TBody>(string endpoint, TBody body, CancellationToken ct = default) where TEntity : notnull where TBody : notnull
        {
            var requestBuilder = new HttpRequestMessageBuilder(endpoint)
                .WithMethod(HttpMethod.Patch)
                .WithBody(body);

            return await SendRequestAsync<TEntity>(requestBuilder, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<Result> PatchAsync<TBody>(string endpoint, TBody body, CancellationToken ct = default) where TBody : notnull
        {
            var requestBuilder = new HttpRequestMessageBuilder(endpoint)
                .WithMethod(HttpMethod.Patch)
                .WithBody(body);

            return await SendRequestAsync(requestBuilder, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<Result<TEntity>> DeleteAsync<TEntity>(string endpoint, IEnumerable<KeyValuePair<string, string>>? queries = null, CancellationToken ct = default) where TEntity : notnull
        {
            var requestBuilder = new HttpRequestMessageBuilder(endpoint)
                .WithQueryParameters(queries)
                .WithMethod(HttpMethod.Delete);

            return await SendRequestAsync<TEntity>(requestBuilder, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<Result> DeleteAsync(string endpoint, IEnumerable<KeyValuePair<string, string>>? queries = null, CancellationToken ct = default)
        {
            var requestBuilder = new HttpRequestMessageBuilder(endpoint)
                .WithQueryParameters(queries)
                .WithMethod(HttpMethod.Delete);

            return await SendRequestAsync(requestBuilder, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<Result<TEntity>> PutAsync<TEntity, TBody>(string endpoint, TBody body, CancellationToken ct = default) where TEntity : notnull where TBody : notnull
        {
            var requestBuilder = new HttpRequestMessageBuilder(endpoint)
                .WithMethod(HttpMethod.Put)
                .WithBody(body);

            return await SendRequestAsync<TEntity>(requestBuilder, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<Result> PutAsync<TBody>(string endpoint, TBody body, CancellationToken ct = default) where TBody : notnull
        {
            var requestBuilder = new HttpRequestMessageBuilder(endpoint)
                .WithMethod(HttpMethod.Put)
                .WithBody(body);

            return await SendRequestAsync(requestBuilder, ct).ConfigureAwait(false);
        }

        /// <summary>
        ///     Send a request with the discord http client.
        /// </summary>
        /// <param name="requestBuilder">The <see cref="HttpRequestMessageBuilder" /> containing the details for the request.</param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <typeparam name="TEntity">The entity type that will be used for deserialization.</typeparam>
        /// <returns>
        ///     The <see cref="Result" /> with the deserialized <see cref="TEntity" />.
        /// </returns>
        private async Task<Result<TEntity>> SendRequestAsync<TEntity>(HttpRequestMessageBuilder requestBuilder, CancellationToken ct) where TEntity : notnull
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("Discord");
                using var request = requestBuilder.Build();
                using var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct).ConfigureAwait(false);

                return await DeserializeResponseAsync<TEntity>(response, ct).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                return e;
            }
        }

        /// <summary>
        ///     Send a request with the discord http client.
        /// </summary>
        /// <param name="requestBuilder">The <see cref="HttpRequestMessageBuilder" /> containing the details for the request.</param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     The <see cref="Result" /> of the request.
        /// </returns>
        private async Task<Result> SendRequestAsync(HttpRequestMessageBuilder requestBuilder, CancellationToken ct)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("Discord");
                using var request = requestBuilder.Build();
                using var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct).ConfigureAwait(false);

                return await GetResponseAsync(response, ct).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                return e;
            }
        }

        /// <summary>
        ///     Deserializes a json response.
        /// </summary>
        /// <param name="response">The <see cref="HttpResponseMessage" /> response.</param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <typeparam name="TEntity">The entity type that will be used for deserialization.</typeparam>
        /// <returns>
        ///     The <see cref="Result" /> with the deserialized <see cref="TEntity" />.
        /// </returns>
        /// <exception cref="NoNullAllowedException">Thrown when <typeparamref name="TEntity" /> is null.</exception>
        private async Task<Result<TEntity>> DeserializeResponseAsync<TEntity>(HttpResponseMessage response, CancellationToken ct = default) where TEntity : notnull
        {
            if (response.IsSuccessStatusCode)
            {
                var entity = await JsonSerializer.DeserializeAsync<TEntity>(await response.Content.ReadAsStreamAsync(ct).ConfigureAwait(false), _serializerOptions.Value, ct).ConfigureAwait(false);
                return entity is not null ? Result<TEntity>.FromSuccess(entity) : throw new NoNullAllowedException("TEntity can not be null");
            }

            if (response.Content.Headers.ContentLength is not > 0) return new HttpErrorResult(response.StatusCode, response.ReasonPhrase ?? "A HTTP error occured");

            var jsonError = await JsonSerializer.DeserializeAsync<DiscordJsonErrorData>(await response.Content.ReadAsStreamAsync(ct).ConfigureAwait(false), _serializerOptions.Value, ct)
                .ConfigureAwait(false);

            return jsonError is null
                ? new HttpErrorResult(response.StatusCode, response.ReasonPhrase ?? "A JSON error occured")
                : new DiscordHttpErrorResult(response.StatusCode, $"Code {jsonError.ErrorType} : {jsonError.Message}");
        }

        /// <summary>
        ///     Get the <see cref="Result" /> from an <see cref="HttpResponseMessage" />.
        /// </summary>
        /// <param name="response">The <see cref="HttpResponseMessage" /> from the request.</param>
        /// <param name="ct">The <see cref="CancellationToken" />.</param>
        /// <returns>
        ///     The <see cref="Result" />.
        /// </returns>
        private async Task<Result> GetResponseAsync(HttpResponseMessage response, CancellationToken ct = default)
        {
            if (response.IsSuccessStatusCode) Result.FromSuccess();

            if (response.Content.Headers.ContentLength is not > 0) return new HttpErrorResult(response.StatusCode, response.ReasonPhrase ?? "A HTTP error occured");

            var jsonError = await JsonSerializer.DeserializeAsync<DiscordJsonErrorData>(await response.Content.ReadAsStreamAsync(ct).ConfigureAwait(false), _serializerOptions.Value, ct)
                .ConfigureAwait(false);

            return jsonError is null
                ? new HttpErrorResult(response.StatusCode, response.ReasonPhrase ?? "A JSON error occured")
                : new DiscordHttpErrorResult(response.StatusCode, $"Code {jsonError.ErrorType} : {jsonError.Message}");
        }
    }
}