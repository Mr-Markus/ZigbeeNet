using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL.Clusters.ColorControl
{
    public enum EnhancedColorMode
    {
        CURRENTHUE_AND_CURRENTSATURATION = 0x0000,
        CURRENTX_AND_CURRENTY = 0x0001,
        ENHANCEDCURRENTHUE_AND_CURRENTSATURATION = 0x0002
    }
}
