using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC
{
    public enum DEV_INFO_TYPE : byte
    {
        CHANNEL = 0x05,
        EXT_PAN_ID = 0x07,
        IEEE_ADDR = 0x01,
        PAN_ID = 0x06,
        PARENT_IEEE_ADDR = 0x04,
        PARENT_SHORT_ADDR = 0x03,
        SHORT_ADDR = 0x02,
        STATE = 0x00
    }
}
