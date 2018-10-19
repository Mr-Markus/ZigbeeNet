using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.SAPI
{
    public enum ZbDeviceInfo
    {
        DEV_STATE = 0,
        IEEE_ADDR = 1,
        SHORT_ADDR = 2,
        PARENT_SHORT_ADDR = 3,
        PARENT_IEEE_ADDR = 4,
        CHANNEL = 5,
        PAN_ID = 6,
        EXT_PAN_ID = 7
    }
}
