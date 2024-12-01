﻿using Coinbase.AdvancedTrade;

namespace Coinbase.AdvancedTradeTest
{
    public class TestBase
    {
        protected CoinbaseClient? _coinbaseClient;
        private static DateTime _lastApiCallTime;
        protected static bool UseLiveClient { get; set; }
        protected WebSocketManager? _webSocketManager;
        public TestContext? TestContext { get; set; }

        [TestInitialize]
        public virtual void Setup()
        {
            // Coinbase Developer Platform Keys
            var apiKey = Environment.GetEnvironmentVariable("COINBASE_CLOUD_TRADING_API_KEY", EnvironmentVariableTarget.User)
                         ?? throw new InvalidOperationException("API Key not found");
            var apiSecret = Environment.GetEnvironmentVariable("COINBASE_CLOUD_TRADING_API_SECRET", EnvironmentVariableTarget.User)
                           ?? throw new InvalidOperationException("API Secret not found");
            _coinbaseClient = new CoinbaseClient(apiKey, apiSecret);


            // Coinbase Legacy Keys
            //var apiKey = Environment.GetEnvironmentVariable("COINBASE_LEGACY_API_KEY", EnvironmentVariableTarget.User)
            //         ?? throw new InvalidOperationException("API Key not found");
            //var apiSecret = Environment.GetEnvironmentVariable("COINBASE_LEGACY_API_SECRET", EnvironmentVariableTarget.User)
            //           ?? throw new InvalidOperationException("API Secret not found");
            //_coinbaseClient = new CoinbaseClient(apiKey: apiKey, apiSecret: apiSecret, apiKeyType: AdvancedTrade.Enums.ApiKeyType.Legacy);



            _webSocketManager = _coinbaseClient?.WebSocket;
        }

        protected static async Task ExecuteRateLimitedTest(Func<Task> testLogic)
        {
            if (UseLiveClient)
            {
                var timeSinceLastCall = DateTime.UtcNow - _lastApiCallTime;
                var delay = 1000 - (int)timeSinceLastCall.TotalMilliseconds;
                if (delay > 0)
                {
                    await Task.Delay(delay);
                }
                _lastApiCallTime = DateTime.UtcNow;
            }

            try
            {
                await testLogic();
            }
            catch (Exception ex)
            {
                Assert.Fail($"Test failed: {ex.Message}");
            }
        }
    }
}
