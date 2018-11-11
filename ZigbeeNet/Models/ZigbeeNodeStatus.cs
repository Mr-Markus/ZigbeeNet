using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet
{
    public enum ZigbeeNodeStatus : byte
    {
        Offline = 0x00,
        Online = 0x01
    }
}
