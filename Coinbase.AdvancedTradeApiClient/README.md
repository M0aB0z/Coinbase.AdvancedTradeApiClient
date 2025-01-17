# Coinbase API Wrapper for Advanced Trade

This project provides a C# wrapper for the [Coinbase Advanced Trade API](https://docs.cdp.coinbase.com/advanced-trade/docs/welcome/), facilitating interactions with various advanced trading functionalities on Coinbase.

The wrapper supports Coinbase Developer Platform (CDP) Keys and  OAuth2 access tokens. The new Coinbase Developer Platform (CDP) Keys utilize JWT for authentication. The legacy API keys option has been restored but is deprecated and will be removed in a future release following Coinbase's update and the removal of the ability to create and edit legacy API keys effective June 10, 2024. However, Coinbase will continue to allow existing legacy keys to work for some time. For more details on Coinbase Developer Platform (CDP) Keys, see [Coinbase's documentation](https://docs.cloud.coinbase.com/advanced-trade-api/docs/rest-api-auth#cloud-api-trading-keys).

For Coinbase Developer Platform (CDP) Keys, the `key_name` and `key_secret` are expected to follow this structure as per Coinbase:

- key: `"organizations/{org_id}/apiKeys/{key_id}"`
- secret: `"-----BEGIN EC PRIVATE KEY-----\nYOUR PRIVATE KEY\n-----END EC PRIVATE KEY-----\n"`

## Overview
The wrapper is organized into various namespaces, each serving a distinct purpose, covering the spectrum from data models to actual API interactions.

## Namespaces

### `Coinbase.AdvancedTradeApiClient`
The root namespace and foundation for the entire API wrapper.

### `Coinbase.AdvancedTradeApiClient.ExchangeManagers`
Central hub for managers responsible for orchestrating different types of operations with the Coinbase Advanced Trade API including WebSocket connections.

### `Coinbase.AdvancedTradeApiClient.Interfaces`
Blueprints that outline the structure and contract for the managers, ensuring a cohesive and standardized approach to API interactions.

### `Coinbase.AdvancedTradeApiClient.Models`
Collection of models capturing the essence of various entities and data structures pivotal for a seamless interaction with the Coinbase API.

## Classes

### `CoinbaseClient`
Your gateway to the vast functionalities of the Coinbase API, supporting the new Coinbase Developer Platform (CDP) Keys.
- **Constructor**: Initializes managers for accounts, products, orders, fees, and WebSocket connections using the provided API credentials. It also includes an optional parameter to specify the WebSocket buffer size, with a default value of 5 MB.

### `CoinbaseOauth2Client`
A new client that allows interaction with the Coinbase API using OAuth2 access tokens.
- **Constructor**: Initializes managers for accounts, products, orders, and fees using the provided OAuth2 access token.
- **Note**: OAuth2 does not support WebSocket connections.

### `CoinbasePublicClient`
Access public API endpoints without the need for authentication.
- **Methods**:
  - `ListPublicProductsAsync`, `GetPublicProductAsync`, `GetPublicProductBookAsync`, `GetPublicMarketTradesAsync`, `GetPublicProductCandlesAsync`, and more.

### `CoinbaseAuthenticator`
A sentinel that ensures every API request is authenticated.
- **Constructors**:
  - Accepts either API key and secret or an OAuth2 access token.
- **Methods**:
  - `SendAuthenticatedRequest`: Directs an authenticated request to the destined path.
  - `SendAuthenticatedRequestAsync`: Asynchronously channels an authenticated request to the designated path.

## Interfaces

### `IAccountsManager`
- **Methods**:
  - `ListAccountsAsync`: Unveils a list of accounts.
  - `GetAccountAsync`: Zeros in on a specific account using its UUID.

### `IApiKeyManager`
- **Methods**:
  - `GetApiKeyDetailsAsync`: Asynchronously retrieves the details of the API key.

### `IFeesManager`
- **Methods**:
  - `GetTransactionsSummaryAsync`: Condenses transactions within a chosen date range into a summary.

### `IProductsManager`
Entrusted with product-centric functionalities.
- **Methods**:
  - `ListProductsAsync`, `GetProductCandlesAsync`, and more.

### `IOrdersManager`
A maestro orchestrating order-related functionalities.
- **Methods**:
  - `ListOrdersAsync`, `CreateMarketOrderAsync`, `CreateSORLimitIOCOrderAsync`, and more.

### `IPublicManager`
Interface for managing public endpoints of Coinbase Advanced Trade API.
- **Methods**:
  - `GetCoinbaseServerTimeAsync`: Asynchronously retrieves the current server time from Coinbase.
  - `ListPublicProductsAsync`: Asynchronously retrieves a list of public products from Coinbase.
  - `GetPublicProductAsync`: Asynchronously retrieves details for a specific public product by product ID.
  - `GetPublicProductBookAsync`: Asynchronously retrieves the order book for a specific public product.
  - `GetPublicMarketTradesAsync`: Asynchronously retrieves market trades for a specific public product.
  - `GetPublicProductCandlesAsync`: Asynchronously retrieves candlestick data for a specific public product.

### `WebSocketManager`
Specialized in managing WebSocket communications for real-time updates from the Coinbase Advanced Trade API.
- **Methods**:
  - `ConnectAsync`, `SubscribeAsync`, and more.
- **Events**:
  - Alerts for diverse message types, from `CandleMessageReceived` to `UserMessageReceived`.
- **Properties**:
  - `WebSocketState`: Get the current connection state.
  - `Subscriptions`: Get the list of active subscriptions.

# Changelog

## v2.0.11 - 2025-JAN-17
-