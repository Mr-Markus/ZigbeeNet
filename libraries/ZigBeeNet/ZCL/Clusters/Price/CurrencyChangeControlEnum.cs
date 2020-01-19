using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL.Clusters.Price
{
    /// <summary>
    /// Currency Change Control value enumeration
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public enum CurrencyChangeControlEnum
    {
        // Clear Billing Info
        CLEAR_BILLING_INFO = 0x0001,
        // Convert Billing Info Using New Currency
        CONVERT_BILLING_INFO_USING_NEW_CURRENCY = 0x0002,
        // Clear Old Consumption Data
        CLEAR_OLD_CONSUMPTION_DATA = 0x0004,
        // Convert Old Consumption Data Using New Currency
        CONVERT_OLD_CONSUMPTION_DATA_USING_NEW_CURRENCY = 0x0008
    }
}
