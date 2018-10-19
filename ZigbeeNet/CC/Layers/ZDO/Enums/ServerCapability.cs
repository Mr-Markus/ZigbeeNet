using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.ZDO
{
    public enum ServerCapability
    {
        NOT_SUPPORTED = 0,
        PRIM_TRUST_CENTER = 1,
        BKUP_TRUST_CENTER = 2,
        PRIM_BIND_TABLE = 4,
        BKUP_BIND_TABLE = 8,
        PRIM_DISC_TABLE = 16,
        BKUP_DISC_TABLE = 32,
        NETWORK_MANAGER = 64
    }
}
