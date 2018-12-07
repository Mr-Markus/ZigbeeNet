using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL.Protocol
{
    public enum ZclCommandDirection : byte
    {
        CLIENT_TO_SERVER = 0x00,
        SERVER_TO_CLIENT = 0x01
    }
}
