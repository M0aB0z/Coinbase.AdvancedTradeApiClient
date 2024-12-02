using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;


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

        // Charger la clé privée
        var privateKeyBytes = Convert.FromBase64String(base64Key); // Assuming PEM is base64 encoded
        using var ecdsa = ECDsa.Create();
        ecdsa.ImportECPrivateKey(privateKeyBytes, out _);

        // Créer le header JWT
        var header = new Dictionary<string, object>
        {
            { "alg", "ES256" },
            { "kid", appKey },
            { "nonce", RandomHex(10) }, // Ajoute une protection contre les attaques de rejeu
            { "typ", "JWT" }
        };

        // Créer le payload JWT
        var payload = new Dictionary<string, object>
        {
            { "sub", appKey },
            { "iss", "coinbase-cloud" },
            { "nbf", Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds) },
            { "exp", Convert.ToInt64((DateTime.UtcNow.AddMinutes(1) - new DateTime(1970, 1, 1)).TotalSeconds) },
            { "uri", uri }
        };

        // Encoder le header et le payload en Base64Url
        string headerBase64Url = Base64UrlEncode(JsonSerializer.SerializeToUtf8Bytes(header));
        string payloadBase64Url = Base64UrlEncode(JsonSerializer.SerializeToUtf8Bytes(payload));

        // Construire la chaîne à signer
        string dataToSign = $"{headerBase64Url}.{payloadBase64Url}";

        // Signer la chaîne
        byte[] dataToSignBytes = Encoding.UTF8.GetBytes(dataToSign);
        byte[] signature = ecdsa.SignData(dataToSignBytes, HashAlgorithmName.SHA256);

        // Encoder la signature en Base64Url
        string signatureBase64Url = Base64UrlEncode(signature);

        // Retourner le JWT complet
        return $"{dataToSign}.{signatureBase64Url}";
    }
    private static string Base64UrlEncode(byte[] input)
        => Convert.ToBase64String(input).TrimEnd('=').Replace('+', '-').Replace('/', '_');

    static string ParseKey(string key)
    {
        List<string> keyLines = [.. key.Split('\n', StringSplitOptions.RemoveEmptyEntries)];

        keyLines.RemoveAt(0);
        keyLines.RemoveAt(keyLines.Count - 1);

        return string.Join("", keyLines);
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

