using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.ZDO
{
    public enum DeviceLogicalType
    {
        COORDINATOR = 0,
        ROUTER = 1,
        ENDDEVICE = 2,
        COMPLEX_DESC_AVAIL = 4,
        USER_DESC_AVAIL = 8,
        RESERVED1 = 16,
        RESERVED2 = 32,
        RESERVED3 = 64,
        RESERVED4 = 128
    }
}
