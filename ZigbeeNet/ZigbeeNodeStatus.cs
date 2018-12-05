using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet
{
    public enum ZigBeeNodeStatus : byte
    {
        Offline = 0x00,
        Online = 0x01
    }
}
