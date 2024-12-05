using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Coinbase.AdvancedTrade;

/// <summary>
/// Represents an authenticator for Coinbase API requests.
/// This class is responsible for generating appropriate headers and ensuring authenticated communication with the Coinbase API.
/// </summary>
public sealed class CoinbaseAuthenticator
{
    private readonly HttpClient httpClient;
    private readonly string _apiKey;
    private readonly string _apiSecret;
    private readonly string _oAuth2AccessToken;
    private readonly bool _useOAuth;
    private const string _apiUrl = "https://api.coinbase.com";

    /// <summary>
    /// Initializes a new instance of the <see cref="CoinbaseAuthenticator"/> class using API key and secret.
    /// </summary>
    /// <param name="apiKey">The API key for Coinbase authentication.</param>
    /// <param name="apiSecret">The API secret for Coinbase authentication.</param>
    public CoinbaseAuthenticator(string apiKey, string apiSecret)
    {
        _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey), "API key cannot be null.");
        _apiSecret = apiSecret ?? throw new ArgumentNullException(nameof(apiSecret), "API secret cannot be null.");
        httpClient = new HttpClient
        {
            BaseAddress = new Uri(_apiUrl)
        };
        _useOAuth = false;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CoinbaseAuthenticator"/> class using OAuth2 access token.
    /// </summary>
    /// <param name="oAuth2AccessToken">The OAuth2 access token for Coinbase authentication.</param>
    public CoinbaseAuthenticator(string oAuth2AccessToken)
    {
        _oAuth2AccessToken = oAuth2AccessToken ?? throw new ArgumentNullException(nameof(oAuth2AccessToken), "OAuth2 access token cannot be null.");
        httpClient = new HttpClient
        {
            BaseAddress = new Uri(_apiUrl)
        };
        _useOAuth = true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<JsonElement> GetAsync(string path, CancellationToken cancellationToken = default)
    {
        // Generate headers required for the authenticated request
        var headers = _useOAuth ? CreateOAuth2Headers() : CreateJwtHeaders(HttpMethod.Get, path);

        // Execute the request and return the result
        return await ExecuteRequestAsync(HttpMethod.Get, path, null, headers, cancellationToken);
    }

    /// <summary>
    /// Sends a POST request to the specified path with the provided body object and query parameters.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="bodyObj"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<JsonElement> PostAsync(string path, object bodyObj, CancellationToken cancellationToken = default)
    {
        // Generate headers required for the authenticated request
        var headers = _useOAuth ? CreateOAuth2Headers() : CreateJwtHeaders(HttpMethod.Post, path);

        // Execute the request and return the result
        return await ExecuteRequestAsync(HttpMethod.Post, path, bodyObj, headers, cancellationToken);
    }

    /// <summary>
    /// Generates headers for OAuth2 authentication.
    /// </summary>
    /// <returns>A dictionary of headers with the Authorization header containing the OAuth2 access token.</returns>
    private Dictionary<string, string> CreateOAuth2Headers()
    {
        return new Dictionary<string, string>
        {
            { "Authorization", $"Bearer {_oAuth2AccessToken}" }
        };
    }

    /// <summary>
    /// Generates headers for API key/secret authentication using JWT (JSON Web Token).
    /// </summary>
    /// <param name="method">Wich HTTP method is used ? (GET / POST)</param>
    /// <param name="path">The path of the API endpoint being accessed.</param>
    /// <returns>A dictionary of headers with the Authorization header containing the JWT for authenticating the request using Coinbase Developer Platform (CDP) API keys.</returns>
    private Dictionary<string, string> CreateJwtHeaders(HttpMethod method, string path)
    {
        string jwtToken = JwtTokenGenerator.GenerateToken(_apiKey, _apiSecret, $"{method} {_apiUrl.Substring("https://".Length)}{path.Split('?').First()}");
        return new Dictionary<string, string>
        {
            { "Authorization", $"Bearer {jwtToken}" }
        };
    }

    /// <summary>
    /// Executes an asynchronous REST request using the provided parameters.
    /// </summary>
    /// <param name="method">The HTTP method (GET, POST, PUT, etc.).</param>
    /// <param name="path">The request path.</param>
    /// <param name="bodyObj">The request body object.</param>
    /// <param name="headers">Headers to be added to the request.</param>
    /// <param name="cancellationToken">Http request cancellation token</param>
    /// <returns>A Task representing the asynchronous operation, which upon completion returns a dictionary representation of the response content, or null if the content is empty or only consists of white-space characters.</returns>
    private async Task<JsonElement> ExecuteRequestAsync(HttpMethod method, string path, object bodyObj, Dictionary<string, string> headers, CancellationToken cancellationToken)
    {
        try
        {
            using (var requestMessage = new HttpRequestMessage(method, path))
            {
                //requestMessage.Headers.Authorization =
                //    new AuthenticationHeaderValue("Bearer", your_token);

                // Add headers to the request
                foreach (var header in headers)
                    requestMessage.Headers.Add(header.Key, header.Value);

                if (bodyObj != null)
                    requestMessage.Content = new StringContent(JsonSerializer.Serialize(bodyObj), Encoding.UTF8, "application/json");

                var response = await httpClient.SendAsync(requestMessage, cancellationToken);
                var responseContent = await response.Content.ReadAsStringAsync();

                // Check if the response content is empty or just white-space
                if (string.IsNullOrWhiteSpace(responseContent))
                    return default;

                // Deserialize the content into a dictionary
                return JsonDocument.Parse(responseContent).RootElement;
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An error occurred while executing the request.", ex);
        }
    }
}