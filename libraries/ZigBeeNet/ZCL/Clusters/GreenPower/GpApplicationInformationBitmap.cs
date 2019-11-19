using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL.Clusters.GreenPower
{
    /// <summary>
    /// Gp Application Information value enumeration
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public enum GpApplicationInformationBitmap
    {
        // Manufacture ID Present
        MANUFACTURE_ID_PRESENT = 0x0001,
        // Model ID Present
        MODEL_ID_PRESENT = 0x0002,
        // Gpd Commands Present
        GPD_COMMANDS_PRESENT = 0x0004,
        // Cluster List Present
        CLUSTER_LIST_PRESENT = 0x0008
    }
}
