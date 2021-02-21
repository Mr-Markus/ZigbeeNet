using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL.Clusters.MeterIdentification
{
    /// <summary>
    /// Data Quality value enumeration
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public enum DataQualityEnum
    {
        // All Data Certified
        ALL_DATA_CERTIFIED = 0x0000,
        // Only Instantaneous Power not Certified
        ONLY_INSTANTANEOUS_POWER_NOT_CERTIFIED = 0x0001,
        // Only Cumulated Consumption not Certified
        ONLY_CUMULATED_CONSUMPTION_NOT_CERTIFIED = 0x0002,
        // Not Certified data
        NOT_CERTIFIED_DATA = 0x0003
    }
}
