using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL.Clusters.Basic
{
    public enum PowerSource : byte
    {
        UNKNOWN = 0x00,
        MAINS_SINGLE_PHASE = 0x01,
        MAINS_THREE_PHASE = 0x02,
        BATTERY = 0x03,
        DC_SOURCE = 0x04,
        EMERGENCY_MAINS_CONSTANT = 0x05,
        EMERGENCY_MAINS_CHANGEOVER = 0x06
    }
}
