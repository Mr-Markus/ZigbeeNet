using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL.Clusters.GreenPower
{
    /// <summary>
    /// Gp Sink Commissioning Mode Options value enumeration
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public enum GpSinkCommissioningModeOptionsBitmap
    {
        // Action
        ACTION = 0x0001,
        // Involve Gpm In Security
        INVOLVE_GPM_IN_SECURITY = 0x0002,
        // Involve Gpm In Pairing
        INVOLVE_GPM_IN_PAIRING = 0x0004,
        // Involve Proxies
        INVOLVE_PROXIES = 0x0008
    }
}
