using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL.Clusters.Time
{
    /// <summary>
    /// Time Status value enumeration
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public enum TimeStatusBitmap
    {
        // Master
        MASTER = 0x0001,
        // Synchronized
        SYNCHRONIZED = 0x0002,
        // Master Zone DST
        MASTER_ZONE_DST = 0x0004,
        // Superseding
        SUPERSEDING = 0x0008
    }
}
