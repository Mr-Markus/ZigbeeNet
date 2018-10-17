using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.AF
{
    public enum Options
    {
        PREPROCESS = 4,
        LIMIT_CONCENTRATOR = 8,
        ACK_REQUEST = 16,
        DISCV_ROUTE = 32,
        EN_SECURITY = 64,
        SKIP_ROUTING = 128
    }
}
