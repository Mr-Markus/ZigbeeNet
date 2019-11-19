using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL.Clusters.Prepayment
{
    /// <summary>
    /// Result Type value enumeration
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public enum ResultTypeEnum
    {
        // Accepted
        ACCEPTED = 0x0000,
        // Rejected Invalid Top Up
        REJECTED_INVALID_TOP_UP = 0x0001,
        // Rejected Duplicate Top Up
        REJECTED_DUPLICATE_TOP_UP = 0x0002,
        // Rejected Error
        REJECTED_ERROR = 0x0003,
        // Rejected Max Credit Reached
        REJECTED_MAX_CREDIT_REACHED = 0x0004,
        // Rejected Keypad Lock
        REJECTED_KEYPAD_LOCK = 0x0005,
        // Rejected Top Up Value Too Large
        REJECTED_TOP_UP_VALUE_TOO_LARGE = 0x0006,
        // Accepted Supply Enabled
        ACCEPTED_SUPPLY_ENABLED = 0x0010,
        // Accepted Supply Disabled
        ACCEPTED_SUPPLY_DISABLED = 0x0011,
        // Accepted Supply Armed
        ACCEPTED_SUPPLY_ARMED = 0x0012
    }
}
