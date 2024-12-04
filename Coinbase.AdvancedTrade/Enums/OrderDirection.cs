﻿using System.ComponentModel;

namespace Coinbase.AdvancedTrade.Enums;

public enum OrderDirection
{
    /// <summary>
    /// The order is a buy order.
    /// </summary>
    [Description("STOP_DIRECTION_STOP_UP")]
    StopDirectionStopUp = 1,
    /// <summary>
    /// The order is a sell order.
    /// </summary>
    [Description("STOP_DIRECTION_STOP_DOWN")]
    StopDirectionStopDown = 2
}
