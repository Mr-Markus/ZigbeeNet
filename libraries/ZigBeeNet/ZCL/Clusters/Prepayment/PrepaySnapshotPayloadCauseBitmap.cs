using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL.Clusters.Prepayment
{
    /// <summary>
    /// Prepay Snapshot Payload Cause value enumeration
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public enum PrepaySnapshotPayloadCauseBitmap
    {
        // General
        GENERAL = 0x0001,
        // Change Of Tariff Information
        CHANGE_OF_TARIFF_INFORMATION = 0x0008,
        // Change Of Price Matrix
        CHANGE_OF_PRICE_MATRIX = 0x0010,
        // Manually Triggered From Client
        MANUALLY_TRIGGERED_FROM_CLIENT = 0x0400,
        // Change Of Tenancy
        CHANGE_OF_TENANCY = 0x1000,
        // Change Of Supplier
        CHANGE_OF_SUPPLIER = 0x2000,
        // Change Of Meter Mode
        CHANGE_OF_METER_MODE = 0x4000,
        // Top Up Addition
        TOP_UP_ADDITION = 0x40000,
        // Debt Credit Addition
        DEBT_CREDIT_ADDITION = 0x080000
    }
}
