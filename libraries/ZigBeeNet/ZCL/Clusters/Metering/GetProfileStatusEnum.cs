using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL.Clusters.Metering
{
    /// <summary>
    /// Get Profile Status value enumeration
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public enum GetProfileStatusEnum
    {
        // Success
        SUCCESS = 0x0000,
        // Undefined Interval Channel Requested
        UNDEFINED_INTERVAL_CHANNEL_REQUESTED = 0x0001,
        // Interval Channel Not Supported
        INTERVAL_CHANNEL_NOT_SUPPORTED = 0x0002,
        // Invalid End Time
        INVALID_END_TIME = 0x0003,
        // More Periods Requested Than Can Be Returned
        MORE_PERIODS_REQUESTED_THAN_CAN_BE_RETURNED = 0x0004,
        // No Intervals Available For The Requested Time
        NO_INTERVALS_AVAILABLE_FOR_THE_REQUESTED_TIME = 0x0005
    }
}
