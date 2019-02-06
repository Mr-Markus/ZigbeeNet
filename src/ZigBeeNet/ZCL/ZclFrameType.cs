using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL
{
    public enum ZclFrameType
    {
        ENTIRE_PROFILE_COMMAND = 0x00,
        CLUSTER_SPECIFIC_COMMAND = 0x01
    }
}
