using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC
{
    public enum DeviceStatus : byte
    {
        Offline = 0x00,
        Online = 0x01
    }
}
