namespace Coinbase.AdvancedTradeApiClient.Models.WebSocket
{
    /// <summary>
    /// Candle with extra property ProductId
    /// </summary>
    public class WebSocketCandle : Candle
    {
        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// Returns a string representation of the candle.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{ProductId} {base.ToString()}";
    }
}
