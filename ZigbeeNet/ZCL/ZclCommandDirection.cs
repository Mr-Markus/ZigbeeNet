using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.ZCL
{
    public enum ZclCommandDirection : byte
    {
        ClientToServer = 0x00,
        ServerToClient = 0x01
    }
}
