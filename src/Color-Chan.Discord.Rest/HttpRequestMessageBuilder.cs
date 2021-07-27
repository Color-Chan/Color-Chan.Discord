using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Web;
using Polly;

namespace Color_Chan.Discord.Rest
{
    public class HttpRequestMessageBuilder
    {
        /// <summary>
        ///     The endpoint that the <see cref="HttpRequestMessage" /> will be using.
        /// </summary>
        private readonly string _endpoint;

        /// <summary>
        ///     A <see cref="Dictionary{TKey,TValue}" /> of <see cref="string" />, <see cref="string" /> holding the query
        ///     parameters.
        /// </summary>
        private readonly Dictionary<string, string> _queryParameters = new();

        /// <summary>
        ///     A <see cref="Dictionary{TKey,TValue}" /> of <see cref="string" />, <see cref="string" /> holding the headers.
        /// </summary>
        private readonly Dictionary<string, string> _headers = new();
        
        /// <summary>
        ///     The JSON content of the <see cref="HttpRequestMessage" />.
        /// </summary>
        private StringContent? _content;

        /// <summary>
        ///     The <see cref="HttpMethod" /> the <see cref="HttpRequestMessage" /> will be using.
        /// </summary>
        private HttpMethod _method;

        /// <summary>
        ///     Initializes a new instance of <see cref="HttpRequestMessageBuilder" />.
        /// </summary>
        /// <param name="endpoint">The endpoint of where the <see cref="HttpRequestMessage" /> will be send to.</param>
        public HttpRequestMessageBuilder(string endpoint)
        {
            _endpoint = endpoint;
            _method = HttpMethod.Get;
        }

        /// <summary>
        ///     Sets the <see cref="HttpMethod" /> of the request.
        /// </summary>
        /// <param name="method">The <see cref="HttpMethod" /> that will be used.</param>
        /// <returns>
        ///     The updated <see cref="HttpRequestMessageBuilder" />.
        /// </returns>
        public HttpRequestMessageBuilder WithMethod(HttpMethod method)
        {
            _method = method;
            return this;
        }

        /// <summary>
        ///     Adds a query parameter to the <see cref="HttpRequestMessageBuilder" />.
        /// </summary>
        /// <param name="name">The name of the query.</param>
        /// <param name="value">The value of the query.</param>
        /// <returns>
        ///     The updated <see cref="HttpRequestMessageBuilder" />.
        /// </returns>
        public HttpRequestMessageBuilder WithQueryParameter(string name, string value)
        {
            _queryParameters.Add(name, value);
            return this;
        }
        
        /// <summary>
        ///     Adds a header to the <see cref="HttpRequestMessageBuilder" />.
        /// </summary>
        /// <param name="name">The name of the header.</param>
        /// <param name="value">The value of the header.</param>
        /// <returns>
        ///     The updated <see cref="HttpRequestMessageBuilder" />.
        /// </returns>
        public HttpRequestMessageBuilder WithHeader(string name, string? value)
        {
            if (value is null)
            {
                return this;
            }
            
            _headers.Add(name, value);
            return this;
        }
        
        /// <summary>
        ///     Adds header parameters to the <see cref="HttpRequestMessageBuilder" />.
        /// </summary>
        /// <param name="headers">The <see cref="IEnumerable{T}" /> containing the headers.</param>
        /// <returns>
        ///     The updated <see cref="HttpRequestMessageBuilder" />.
        /// </returns>
        public HttpRequestMessageBuilder WithHeaders(IEnumerable<KeyValuePair<string, string>>? headers)
        {
            if (headers is null)
                return this;

            foreach (var (key, value) in headers) WithHeader(key, value);

            return this;
        }

        /// <summary>
        ///     Adds query parameters to the <see cref="HttpRequestMessageBuilder" />.
        /// </summary>
        /// <param name="queries">The <see cref="IEnumerable{T}" /> containing the queries.</param>
        /// <returns>
        ///     The updated <see cref="HttpRequestMessageBuilder" />.
        /// </returns>
        public HttpRequestMessageBuilder WithQueryParameters(IEnumerable<KeyValuePair<string, string>>? queries)
        {
            if (queries is null)
                return this;

            foreach (var (key, value) in queries) WithQueryParameter(key, value);

            return this;
        }

        /// <summary>
        ///     Adds a JSON body to the <see cref="HttpRequestMessageBuilder" />.
        /// </summary>
        /// <param name="value">The value that will be added as JSON.</param>
        /// <typeparam name="TValue">The type of <paramref name="value" />.</typeparam>
        /// <returns>
        ///     The updated <see cref="HttpRequestMessageBuilder" />.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when the <see cref="_method" /> is <see cref="HttpMethod.Get" />.</exception>
        public HttpRequestMessageBuilder WithBody<TValue>(TValue value) where TValue : notnull
        {
            if (_method == HttpMethod.Get)
                throw new ArgumentException("Can not add a body to a GET request");

            _content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
            return this;
        }

        /// <summary>
        ///     Builds the <see cref="HttpRequestMessageBuilder" /> into a <see cref="HttpRequestMessage" />.
        /// </summary>
        /// <returns>
        ///     The <see cref="HttpRequestMessage" />.
        /// </returns>
        public HttpRequestMessage Build()
        {
            var queryParameters = HttpUtility.ParseQueryString(string.Empty);
            foreach (var (queryName, queryValue) in _queryParameters) queryParameters.Add(queryName, queryValue);

            var request = new HttpRequestMessage(_method, _endpoint + (queryParameters.Count > 0 ? "?" + queryParameters : string.Empty))
            {
                Content = _content,
            };

            foreach (var (key, value) in _headers)
            {
                request.Headers.Add(key, value);
            }
            
            var context = new Context {{"endpoint", _endpoint}};
            request.SetPolicyExecutionContext(context);

            return request;
        }
    }
}