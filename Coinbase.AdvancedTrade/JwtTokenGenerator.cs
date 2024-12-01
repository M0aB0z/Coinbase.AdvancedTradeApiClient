using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Jose;
using System.Linq;
using System.Xml.Linq;


/*
 * This class is responsible for generating JSON Web Tokens (JWTs) for authenticating with the Coinbase Developer Platform (CDP) API.
 * cf https://docs.cdp.coinbase.com/coinbase-app/docs/api-key-authentication
 */


/// <summary>
/// Provides functionality to generate JSON Web Tokens (JWTs) for authenticating with the Coinbase Developer Platform (CDP) API.
/// This utility class supports creating tokens with both REST and WebSocket service identifiers.
/// </summary>
public static class JwtTokenGenerator
{
    private static readonly Random random = new Random();

    /// <summary>
    /// Generates a JSON Web Token (JWT) for authenticating with the Coinbase Developer Platform (CDP) API.
    /// </summary>
    /// <param name="appKey">Your application public key like 'organizations/{org_id}/apiKeys/{key_id}'</param>
    /// <param name="cbPrivateKey">Your application private key</param>
    /// <param name="uri">the requested uri</param>
    /// <returns></returns>
    public static string GenerateToken(string appKey, string cbPrivateKey, string uri)
    {
        string base64Key = ParseKey(cbPrivateKey.Replace("\\n", "\n"));

        var privateKeyBytes = Convert.FromBase64String(base64Key); // Assuming PEM is base64 encoded
        using var key = ECDsa.Create();
        key.ImportECPrivateKey(privateKeyBytes, out _);

        var payload = new Dictionary<string, object>
             {
                 { "sub", appKey },
                 { "iss", "coinbase-cloud" },
                 { "nbf", Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds) },
                 { "exp", Convert.ToInt64((DateTime.UtcNow.AddMinutes(1) - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds) },
                 { "uri", uri }
             };

        var extraHeaders = new Dictionary<string, object>
             {
                 { "kid", appKey },
                 // add nonce to prevent replay attacks with a random 10 digit number
                 { "nonce", RandomHex(10) },
                 { "typ", "JWT"}
             };

        return JWT.Encode(payload, key, JwsAlgorithm.ES256, extraHeaders);
    }

    static string ParseKey(string key)
    {
        List<string> keyLines = [.. key.Split('\n', StringSplitOptions.RemoveEmptyEntries)];

        keyLines.RemoveAt(0);
        keyLines.RemoveAt(keyLines.Count - 1);

        return String.Join("", keyLines);
    }

    static string RandomHex(int digits)
    {
        byte[] buffer = new byte[digits / 2];
        random.NextBytes(buffer);
        string result = string.Concat(buffer.Select(x => x.ToString("X2")).ToArray());
        if (digits % 2 == 0)
            return result;
        return result + random.Next(16).ToString("X");
    }
}

