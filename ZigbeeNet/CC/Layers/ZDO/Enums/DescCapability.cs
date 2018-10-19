using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.ZDO
{
    public enum DescCapability
    {
        EXT_LIST_NOT_SUPPORTED = 0,
        EXT_ACTIVE_EP_LIST_AVAIL = 1,
        EXT_SIMPLE_DESC_LIST_AVAIL = 2,
        RESERVED1 = 4,
        RESERVED2 = 8,
        RESERVED3 = 16,
        RESERVED4 = 32,
        RESERVED5 = 64,
        RESERVED6 = 128
    }
}
